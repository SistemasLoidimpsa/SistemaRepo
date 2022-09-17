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
    public partial class ActualizacionEmpleado : System.Web.UI.Page
    {
        
        EmpleadoN empleadoN;
        string script;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (Session["usuario"] != null)
                {
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        if (item.rol == 1 || item.rol == 4)
                        {
                            pnlAdministracion.Visible = true;
                            ddlRol.Enabled = true;
                        }
                        else if (item.rol == 2)
                        {
                            pnlAdministracion.Visible = true;
                            ddlRol.Enabled = false;
                        }
                        else if (item.rol == 3 || item.rol == 5)
                        {
                            pnlAdministracion.Visible = false;
                            ddlRol.Enabled = false;
                            pnlDatosClientes.Visible = true;
                            DatosEmpleados(item.identificacion);
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
               gvEmpleados.DataSource = null;
               gvEmpleados.DataBind();
                if (!string.IsNullOrWhiteSpace(txtEmpleadoBuscar.Text.Trim()))
                {
                    pnlClientes.Visible = true;
                    var lista = new EmpleadoN().BuscarEmpleado(ConfigurationManager.AppSettings["cnnSQL"], txtEmpleadoBuscar.Text.Trim());
                   gvEmpleados.DataSource = lista;
                   gvEmpleados.DataBind();
                   gvEmpleados.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gv = gvEmpleados.SelectedRow;




            if (DatosEmpleados(Server.HtmlEncode(gv.Cells[1].Text)))
            {

                txtIdentificacion.Text = Server.HtmlEncode(gv.Cells[1].Text);
                _rutUser.Value = Server.HtmlEncode(gv.Cells[1].Text);
                txtIdentificacion.Enabled = false;
                btnGuardar.Visible = true;
                pnlDatosClientes.Visible = true;

            }
            else
            {
                script = "alert('Empleado no registrado');";
                ScriptManager.RegisterStartupScript(this, this.GetType()
                                                  , "script", script, true);
                return;
            }
        }

        private bool DatosEmpleados(string identificacion)
        {
            bool save = false;
            try
            {
                empleadoN = new EmpleadoN();
                var datos = empleadoN.DatosEmpleado(identificacion);
                if (datos.numeroIdentificacion != null)
                {

                    
                    ddlTipoIdentificacion.SelectedValue = datos.tipoIdentificacion.ToString();
                    txtIdentificacion.Text = datos.numeroIdentificacion;
                    txtprimerApellido.Text = datos.primerApellido;
                    txtSegundoApellido.Text = datos.segundoApellido;
                    txtPrimerNombre.Text = datos.primerNombre;
                    txtSegundoNombre.Text = datos.segundoNombre;
                    ddlGenero.SelectedValue = datos.sexoID.ToString();
                    txtTelefono.Text = datos.telefono;
                    txtMail.Text = datos.correo;
                    txtCelular.Text = datos.celular;
                    txtFechaNacimiento.Text = datos.fecha_nacimiento.ToString("yyyy-MM-dd");
                    ddlRol.SelectedValue = datos.id_rol;
                    save = true;

                }
            }
            catch (Exception ex)
            {

            }
            return save;
        }

        protected void gvEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

       

        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            empleadoN = new EmpleadoN();

            if (!String.IsNullOrEmpty(txtFechaNacimiento.Text))
            {
            

                    if (empleadoN.ActualizarDatosEmpleados(Convert.ToInt32(ddlTipoIdentificacion.SelectedValue), txtIdentificacion.Text, txtPrimerNombre.Text,
                                                                              txtSegundoNombre.Text, txtprimerApellido.Text, txtSegundoApellido.Text, Convert.ToInt32(ddlGenero.SelectedValue),
                                                                              txtTelefono.Text, txtMail.Text, txtCelular.Text, 
                                                                              Convert.ToDateTime(txtFechaNacimiento.Text),
                                                                              ddlRol.SelectedValue,
                                                                           
                                                                              Session["cod_usu"].ToString()))
                    {
                        script = "alert('Se actualizó el Empleado');";
                        ScriptManager.RegisterStartupScript(uppActualizacionEmpleado, uppActualizacionEmpleado.GetType(), "script", script, true);
                    }
                    else
                    {
                        script = "alert('Ocurrio un error');";
                        ScriptManager.RegisterStartupScript(uppActualizacionEmpleado, uppActualizacionEmpleado.GetType(), "script", script, true);
                    }
                }
               
            
            else
            {
                script = "alert('Debe ingresar la fecha de nacimiento.');";
                ScriptManager.RegisterStartupScript(uppActualizacionEmpleado, uppActualizacionEmpleado.GetType(), "script", script, true);
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
