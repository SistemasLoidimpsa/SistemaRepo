using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace LOADIMPSA
{
    public partial class ActualizacionDatos : System.Web.UI.Page
    {
        GeneralesN generalesN;
        ClienteN clienteN;
        string script;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProvincias();
                CargarCantones(ddlProvincia.SelectedValue);
                if (Session["usuario"] != null)
                {
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        if (item.rol == 1 )
                        {
                            pnlAdministracion.Visible = true;
                            ddlRol.Enabled = true;
                        }
                        else if (item.rol == 2 || item.rol == 6 || item.rol == 4 || item.rol == 8 || item.rol == 9)
                        {
                            pnlAdministracion.Visible = true;
                        
                            ddlRol.Enabled = false;
                        }
                        else if (item.rol == 3 || item.rol == 5)
                        {
                            ddlRol.Visible = false;
                            pnlAdministracion.Visible = false;
                            ddlRol.Enabled = false;

                            pnlDatosClientes.Visible = true;
                            DatosClientes(item.identificacion);
                        }
                        Session["cod_usu"] = item.cod_usu;
                    }

                }

            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                gvClientes.DataSource = null;
                gvClientes.DataBind();
                if (!string.IsNullOrWhiteSpace(txtClienteBuscar.Text.Trim()))
                {
                    pnlClientes.Visible = true;
                    var lista = new ClienteN().BuscarCliente(ConfigurationManager.AppSettings["cnnSQL"], txtClienteBuscar.Text.Trim());
                    gvClientes.DataSource = lista;
                    gvClientes.DataBind();
                    gvClientes.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gv = gvClientes.SelectedRow;




            if (DatosClientes(Server.HtmlEncode(gv.Cells[1].Text)))
            {

                txtIdentificacion.Text = Server.HtmlEncode(gv.Cells[1].Text);
                _rutUser.Value = Server.HtmlEncode(gv.Cells[1].Text);
                txtIdentificacion.Enabled = false;
                btnGuardar.Visible = true;
                pnlDatosClientes.Visible = true;
                ddlRol.Items[0].Enabled = false;
                ddlRol.Items[1].Enabled = false;

            }
            else
            {
                script = "alert('Cliente no registrado');";
                ScriptManager.RegisterStartupScript(this, this.GetType()
                                                  , "script", script, true);
                return;
            }
        }

        private bool DatosClientes(string identificacion)
        {
            bool save = false;
            try
            {
                clienteN = new ClienteN();
                var datos = clienteN.DatosClientes(identificacion);
                if (datos.numeroidentificacion != null)
                {

                    CargarProvincias();
                    CargarCantones(null);
                    ddlTipoIdentificacion.SelectedValue = datos.tipoIdentificacion.ToString();
                    txtIdentificacion.Text = datos.numeroidentificacion;
                    txtprimerApellido.Text = datos.primerApellido;
                    txtSegundoApellido.Text = datos.segundoApellido;
                    txtPrimerNombre.Text = datos.primerNombre;
                    txtSegundoNombre.Text = datos.segundoNombre;
                    ddlGenero.SelectedValue = datos.sexoId.ToString();
                    txtTelefono.Text = datos.telefono;
                    txtMail.Text = datos.correo;
                    txtCelular.Text = datos.celular;
                    ddlProvincia.SelectedValue = datos.provinciaID;
                    ddlProvincia_SelectedIndexChanged(null, null);
                    ddlCantones.SelectedValue = datos.cantonID;
                    txtDireccion.Text = datos.direccionEntrega;
                    txtFechaNacimiento.Text = datos.fechaNacimiento.ToString("yyyy-MM-dd");
                    ddlRol.SelectedValue = datos.id_rol;
                    save = true;

                }
            }
            catch (Exception ex)
            {

            }
            return save;
        }

        protected void gvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void CargarProvincias()
        {
            generalesN = new GeneralesN();
            var provincias = generalesN.Provincias();
            ddlProvincia.Items.Clear();
            foreach (var item in provincias)
            {
                ddlProvincia.Items.Add(new ListItem(item.Value, item.Key));
            }
            ddlProvincia.Items.Add(new ListItem("SIN PROVINCIA", "NA"));
        }
        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCantones(ddlProvincia.SelectedValue);
        }

        private void CargarCantones(string id_prov)
        {
            generalesN = new GeneralesN();
            var cantones = generalesN.Cantones(id_prov);
            ddlCantones.Items.Clear();

            foreach (var item in cantones)
            {
                ddlCantones.Items.Add(new ListItem(item.Value, item.Key));
            }
            ddlCantones.Items.Add(new ListItem("SIN CANTON", "NA"));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            clienteN = new ClienteN();

            if (!String.IsNullOrEmpty(txtFechaNacimiento.Text))
            {
                if (!String.IsNullOrEmpty(txtCodigoClienteVIP.Text) || ddlRol.SelectedValue == "3")
                {

                    if (clienteN.ActualizarDatos(Convert.ToInt32(ddlTipoIdentificacion.SelectedValue), txtIdentificacion.Text, txtPrimerNombre.Text,
                                                                              txtSegundoNombre.Text, txtprimerApellido.Text, txtSegundoApellido.Text, Convert.ToInt32(ddlGenero.SelectedValue),
                                                                              txtTelefono.Text, txtMail.Text, txtCelular.Text, ddlProvincia.SelectedValue, ddlProvincia.SelectedItem.Text,
                                                                              ddlCantones.SelectedValue, ddlCantones.SelectedItem.Text, txtDireccion.Text,
                                                                              Convert.ToDateTime(txtFechaNacimiento.Text),
                                                                              ddlRol.SelectedValue,
                                                                             ddlRol.SelectedValue == "3" ? "" : txtCodigoClienteVIP.Text,
                                                                              Session["cod_usu"].ToString()))
                    {
                        script = "alert('Se actualizó el Cliente');";
                        ScriptManager.RegisterStartupScript(uppActualizacion, uppActualizacion.GetType(), "script", script, true);
                    }
                    else
                    {
                        script = "alert('Ocurrio un error');";
                        ScriptManager.RegisterStartupScript(uppActualizacion, uppActualizacion.GetType(), "script", script, true);
                    }
                }
                else
                {
                    script = "alert('Debe Registrar el Código VIP del cliente.');";
                    ScriptManager.RegisterStartupScript(uppActualizacion, uppActualizacion.GetType(), "script", script, true);
                }
            }
            else
            {
                script = "alert('Debe ingresar la fecha de nacimiento.');";
                ScriptManager.RegisterStartupScript(uppActualizacion, uppActualizacion.GetType(), "script", script, true);
            }



        }

        protected void ddlProvincia_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRol.SelectedValue == "5")
            {
                txtCodigoClienteVIP.Visible = true;
                txtCodigoClienteVIP.Text = string.Empty;
            }
            else
            {
                txtCodigoClienteVIP.Visible = false;
                txtCodigoClienteVIP.Text = string.Empty;
            }
        }
    }
}