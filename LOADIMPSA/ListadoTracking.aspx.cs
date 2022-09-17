using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Configuration;

namespace LOADIMPSA
{
    public partial class ListadoTracking1 : System.Web.UI.Page
    {
        ClienteN clienteN;
        TransportistasN transportistasN;
        EmpleadoN empleadoN;
        TrackingsN trackingsN;
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
                        if (item.rol == 1 || item.rol == 4 || item.privilegiado == 1)
                        {
                          
                        }
                        else if ( item.rol == 2 || item.rol == 6 || item.rol == 9 && item.privilegiado == 0 )
                        {
                            dtgListadoTracking.Columns[8].Visible = false;
                        }
                        else if (item.rol == 3)
                        {
                            pnlBusquedaClientes.Visible = false;
                            DatosClientes(item.identificacion);
                            pnlDatosCliente.Visible = true;
                            pnlListadoTracking.Visible = true;
                            CargaTrackings(item.identificacion);
                            dtgListadoTracking.Columns[8].Visible = false;
                        }
                        else
                        {
                            Response.Redirect("Home.aspx");
                        }
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
                if (!string.IsNullOrWhiteSpace(txtCliente.Text.Trim()))
                {
                    pnlClientes.Visible = true;
                    pnlHistorial.Visible = false;
                    pnlVerOrden.Visible = false;
                    var lista = new ClienteN().BuscarCliente(ConfigurationManager.AppSettings["cnnSQL"], txtCliente.Text.Trim());
                    gvClientes.DataSource = lista;
                    gvClientes.DataBind();
                    gvClientes.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {

            }
        }



        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gv = gvClientes.SelectedRow;
            DatosClientes(Server.HtmlDecode(gv.Cells[1].Text));
            pnlDatosCliente.Visible = true;
            pnlListadoTracking.Visible = true;
            CargaTrackings(Server.HtmlDecode(gv.Cells[1].Text));
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
                    lblNombresCom.Text = datos.primerNombre + " " + datos.segundoNombre + " " + datos.primerApellido + " " + datos.segundoApellido;
                    lBlCed.Text = datos.numeroidentificacion.ToString();
                    lblCodCliente.Text = datos.idCasillero.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return save;
        }
        public void CargaTrackings(string strCliente)
        {
            trackingsN = new TrackingsN();
            var lista = trackingsN.ListadoTrackings(ConfigurationManager.AppSettings["cnnSQL"], strCliente);
            dtgListadoTracking.DataSource = lista;
            dtgListadoTracking.DataBind();
        }

        public void CargarHistorialTracking(int idOrden)
        {
            trackingsN = new TrackingsN();
            var lista = trackingsN.HistorialTracking(ConfigurationManager.AppSettings["cnnSQL"], idOrden);
            dtgHistorialTracking.DataSource = lista;
            dtgHistorialTracking.DataBind();
        }
        protected void ddlseparar_SelectedIndexChanged(object sender, EventArgs e)
        {
            mdlPopup.Show();
        }

        protected void ddlTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransportista.SelectedValue == "-1")
            {
                txtOtroTransportista.Visible = true;
            }
            else
            {
                txtOtroTransportista.Visible = false;
                txtOtroTransportista.Text = string.Empty;
            }
        }
        private void CargarTransportistas()
        {
            ddlTransportista.Items.Clear();
            transportistasN = new TransportistasN();

            var carreras = transportistasN.Transportistas();
            foreach (var item in carreras)
            {
                ddlTransportista.Items.Add(new ListItem(item.Key, item.Value.ToString()));
            }
            ddlTransportista.Items.Add(new ListItem("Nuevo", "-1"));

        }
        protected void btnHistorial_Click(object sender, ImageClickEventArgs e)
        {

            mdlPopup.Show();
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lbltitulo2.Text = commandArgs[0].ToString() + " / " + commandArgs[1];
            CargarHistorialTracking(Convert.ToInt32(commandArgs[0].ToString()));
            pnlHistorial.Visible = true;
            pnlVerOrden.Visible = false;
        }

        public void LimpiarCajas()
        {   txtJustifica.Text = string.Empty;
            txtOrdenInterno.Text = string.Empty;
            txtTracking.Text = string.Empty;
            txtPeso.Text = string.Empty;
            DateTime fechaHoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            txtFechaRecibidoMiami.Text = fechaHoy.ToString("yyyy-MM-dd");
            txtObservaciones.Text = string.Empty;
            txtOtroTransportista.Text = string.Empty;
            CargarTransportistas();
          
        }
        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCedula.Text))

            {
                if (!String.IsNullOrEmpty(txtOrdenInterno.Text))

                {
                    if (Convert.ToInt32(txtOrdenInterno.Text) <= 1999999999)
                    {
                        if (!String.IsNullOrEmpty(txtTracking.Text))
                        {
                            if (!String.IsNullOrEmpty(txtPeso.Text))
                            {
                                if (!String.IsNullOrEmpty(txtFechaRecibidoMiami.Text))
                                {
                                    if (!String.IsNullOrEmpty(txtDescripcion.Text))
                                    {
                                        if (!String.IsNullOrEmpty(txtJustifica.Text))
                                        {
                                            if (!String.IsNullOrEmpty(txtPrecio.Text))
                                            {



                                                empleadoN = new EmpleadoN();
                                            if (empleadoN.ActualizaOrdenInterna(Convert.ToInt32(txtOrdenInterno.Text), txtTracking.Text,
                                                txtCedula.Text, Convert.ToDecimal(txtPeso.Text), Convert.ToDateTime(txtFechaRecibidoMiami.Text), ddlCategoriaC.SelectedValue, txtJustifica.Text, 
                                                txtDescripcion.Text,
                                                txtObservaciones.Text, Session["cod_usu"].ToString(), Convert.ToInt32(ddlsepararPaquete.SelectedValue), Convert.ToDecimal(txtPrecio.Text))
                                                )
                                            {
                                                script = "alert('La Orden: " + txtOrdenInterno.Text + " del cliente: " + lblNombresCom.Text + " se ha actualizado correctamente.');";
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                LimpiarCajas();
                                                CargaTrackings(lBlCed.Text.ToString());
                                            }
                                            else
                                            {
                                                script = "alert('La Orden: " + txtOrdenInterno.Text + " No se ha actulizado, revise la información.');";
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                            }
                                            }
                                            else
                                            {
                                                script = "alert('Es precio es obligatorio.');";
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                txtPrecio.Focus();
                                                mdlPopup.Show();
                                            }
                                        }
                                        else
                                        {
                                            script = "alert('La Justificación de la actualizacion es obligatoria.');";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                            txtJustifica.Focus();
                                            mdlPopup.Show();
                                        }
                                    }
                                    else
                                    {
                                        script = "alert('La descripción del Envio es obligatoria.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                        txtDescripcion.Focus();
                                        mdlPopup.Show();
                                    }
                                }
                                else
                                {

                                    script = "alert('La Fecha de Ingreso a la Bodega en Miami es obligatoria');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                    txtFechaRecibidoMiami.Focus();
                                    mdlPopup.Show();
                                }
                            }
                            else
                            {

                                script = "alert('El peso de la Orden es obligatorio.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                txtPeso.Focus();
                                mdlPopup.Show();
                            }

                        }
                        else
                        {
                            script = "alert('El numero de Tracking es obligatorio.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            txtTracking.Focus();
                            mdlPopup.Show();
                        }
                    }
                    else
                    {
                        script = "alert('El numero es demasiado grande, no soporta mas de 10 digitos');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                        txtTracking.Focus();
                        mdlPopup.Show();
                    }
                }
                else
                {
                    script = "alert('El numero de Orden es obligatorio.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtOrdenInterno.Focus();
                    mdlPopup.Show();

                }


            }
            else
            {
                script = "alert('El numero de cédula es obligatorio.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                txtCedula.Focus();
                mdlPopup.Show();
            }

        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarCajas();
            mdlPopup.Show();
            CargarTransportistas();
            ddlTransportista_SelectedIndexChanged(null, null);
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lbltitulo2.Text = commandArgs[0].ToString();
            txtCedula.Text = commandArgs[7].ToString();
            txtOrdenInterno.Text = commandArgs[0].ToString();
            txtOrdenInterno.Enabled = false;
            txtTracking.Text = commandArgs[2].ToString();
            txtPeso.Text = commandArgs[4].ToString();
           
            txtFechaRecibidoMiami.Text = Convert.ToDateTime(commandArgs[5]).ToString("yyyy-MM-dd");
            txtDescripcion.Text = commandArgs[6].ToString();
            txtObservaciones.Text = commandArgs[8].ToString();
            ddlsepararPaquete.SelectedValue = commandArgs[9].ToString();
            ddlCategoriaC.SelectedValue = commandArgs[10].ToString();
            txtPrecio.Text= commandArgs[11].ToString();

            pnlHistorial.Visible = false;
            pnlVerOrden.Visible = true;
            var variable = Convert.ToInt32(commandArgs[3].ToString());
            if (variable <= 50)
            {
                ddlTransportista.Visible = true;
                ddlTransportista.SelectedValue = commandArgs[3].ToString();
                ddlTransportista.Enabled = false;
                ddlTransportista_SelectedIndexChanged(null, null);
            }
            else
            {
                ddlTransportista.Visible = false;
                txtOtroTransportista.Visible = true;
                txtOtroTransportista.Enabled = false;
                txtOtroTransportista.Text = commandArgs[1].ToString();

            }
         
        }



    }
}

