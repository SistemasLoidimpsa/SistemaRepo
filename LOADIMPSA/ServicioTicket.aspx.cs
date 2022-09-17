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
    public partial class ServicioTicket : System.Web.UI.Page {

        TrackingsN trackingsN;
        ClienteN clienteN;
        TransportistasN transportistasN;
        EmpleadoN empleadoN;
        string script;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    DateTime fechaHoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    PanelIngreso.Visible = false;
                    PanelConsulta.Visible = false;
                    // cargar Fechas en Textbox
                    DateTime fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    txtFechaInicio.Text = fechaInicio.ToString("yyyy-MM-dd");
                    DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    txtFechaFin.Text = fechaFin.ToString("yyyy-MM-dd");
                    ddlEstadoOrden.SelectedValue = "INGRESADA";

                    CargarTransportistas();
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        Session["cod_usu"] = item.cod_usu;

                    }
                }
                else
                {
                    Response.Redirect("ServicioTicket.aspx");
                }
            }
        }

        //Cargar panel
        protected void radio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //do whatever you want by calling the name of the radio id
            //example

            if (panelId.SelectedItem.Value == "0"){
              
                PanelIngreso.Visible = true;
                PanelConsulta.Visible = false;


            }
            else if (panelId.SelectedItem.Value == "1")
            {
                LimpiarCajas();
                PanelIngreso.Visible = false;
                PanelConsulta.Visible = true;
                btnBuscar_ClickC(null, null);

            }
        
}

        #region Cargar Datos

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                gvClientes.DataSource = null;
                gvClientes.DataBind();
                if (!string.IsNullOrWhiteSpace(txtCliente.Text.Trim()))
                {
                    pnlClientes.Visible = true;
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
            pnlDatosEstudiante.Visible = true;
            pnlClientesTracking.Visible = true;
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


        #endregion

        protected void btnIngresarOrden_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtContenido.Text))

            {
                    if (!String.IsNullOrEmpty(txtTracking.Text))
                    {
                       

                            if (!String.IsNullOrEmpty(txtDescripcion.Text))
                            {

                                string strTransportista = ddlTransportista.SelectedValue == "-1" ? txtOtroTransportista.Text : ddlTransportista.SelectedItem.Text;

                                empleadoN = new EmpleadoN();
                                if (empleadoN.InsertaOrdenTicket( txtTracking.Text,
                                    lBlCed.Text, Convert.ToInt32(ddlTransportista.SelectedValue),
                                    strTransportista, txtDescripcion.Text,  txtContenido.Text, "INGRESADA",
                                    Session["cod_usu"].ToString()))
                                {
                                    script = "alert('El ticket" +" del cliente: " + lblNombresCom.Text + " se ha registrado correctamente.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                    LimpiarCajas();
                                }
                                else
                                {
                                    script = "alert('El ticket ya se ha registrado, revise la información.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                }
                            }
                            else
                            {
                                script = "alert('La descripción del problema es obligatoria.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                txtDescripcion.Focus();
                            }
                        

                
                   

                }
                else
                {
                    script = "alert('El numero de Tracking es obligatorio.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtTracking.Focus();
                }
            
        }
        else
        {
            script = "alert('El contenido no se ha ingresado.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
        txtContenido.Focus();

        } 
    }


    public void LimpiarCajas()
    {
     txtDescripcion.Text = string.Empty;
            txtTracking.Text = string.Empty;
      
        txtContenido.Text = string.Empty;
        txtOtroTransportista.Text = string.Empty;
        CargarTransportistas();
        pnlClientesTracking.Visible = false;
        pnlDatosEstudiante.Visible = false;
        pnlClientes.Visible = false;
    }

        public void LimpiarCajasConsulta()
        {
            ddlEstadoTicket.SelectedIndex = 0;
            txtObservacion.Text = string.Empty;
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

        protected void txtOtroTransportista_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtOrdenInterno_TextChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mdlPopup.Show();
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

       
        //Seccion de Consulta


        public void CargarListado(DateTime datFechaIngreso, DateTime datFechaFin, string strOrdenEstado, string strOrdenTicket)
        {
            try
            {
                trackingsN = new TrackingsN();
                var list = trackingsN.ListadoTicketCliente(ConfigurationManager.AppSettings["cnnSQL"], datFechaIngreso, datFechaFin, strOrdenEstado, strOrdenTicket);
                dtgEnvios.DataSource = list;
                dtgEnvios.DataBind();
            }
            catch (Exception ex)
            {

            }

        }

        protected void dtgTicketDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void dtgTicket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            trackingsN = new TrackingsN();

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView dtgDetalle = (e.Row.FindControl("dtgTicketDetalle") as GridView);
                    String idTicket = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "idTicket"));
                    var dat = new TrackingsN().ListadoTicketDetalle(ConfigurationManager.AppSettings["cnnSQL"], idTicket);
                    dtgDetalle.DataSource = dat;
                    dtgDetalle.DataBind();
                    dtgDetalle.Visible = true;

                    if (e.Row.Cells[9].Text == "RESUELTO")
                    {
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).Enabled = false;
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).ImageUrl = "~/images/checki.png";
                         (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).ToolTip = "EL envio se encuentra finalizado.";

                    }
                    else
                    {
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).Enabled = true;
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).ToolTip = "Click para Resolver el Ticket.";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //protected void txtfechaRegistro_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CargarListado(Convert.ToDateTime(txtfechaRegistro.Text));
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

     
        protected void btnBuscar_ClickC(object sender, EventArgs e)
        {
            pnlListado.Visible = true;
            if (!String.IsNullOrEmpty(txtFechaInicio.Text))
            {
                if (!String.IsNullOrEmpty(txtFechaFin.Text))
                {

                    CargarListado(Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text), ddlEstadoOrden.SelectedValue, String.IsNullOrEmpty(txtOrdenTicket.Text) ? "%" : txtOrdenTicket.Text);
                }
                else
                {
                    script = "alert('La fecha fin es obligatoria para buscar.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                }
            }
            else
            {
                script = "alert('La fecha inicio es obligatoria para buscar.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

            }
        }

        protected void btnBorrarTicket_Click(object sender, ImageClickEventArgs e)
        {
            string confirmvalue = Request.Form["confirm_value"];
            var respuesta = confirmvalue.Split(',');
            if (respuesta.LastOrDefault() == "Yes")
            {
                string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
                trackingsN = new TrackingsN();
                var respuestas = trackingsN.EliminaTicketResuelto(commandArgs[0].ToString(), "ELIMINADO", "Orden eliminada por el Administrador", Session["cod_usu"].ToString());
                if (respuestas == true)
                {
                    script = "alert('La orden: #" + commandArgs[0].ToString() + " se a eliminado correctamente.');";
                    ScriptManager.RegisterStartupScript(updPnlCustomerDetail, updPnlCustomerDetail.GetType(), "script", script, true);
                    btnBuscar_Click(null, null);
                    btnBuscar_ClickC(null, null);
                }
            }
        }

        protected void btnCheckOut_ClickR(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();
            if (!String.IsNullOrEmpty(txtObservacion.Text))
            {
                bool? res = trackingsN.ActualizaResueltoTicket(lbltitulo2.Text, ddlEstadoTicket.SelectedValue, txtObservacion.Text, Session["cod_usu"].ToString());

                if (res == true)
                {   if (ddlEstadoTicket.SelectedValue == "PROCESO")
                    {
                        script = "alert('Se encuentra en Proceso el Servicio del Ticket : " + lbltitulo2.Text + " .');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                        btnBuscar_Click(null, null);
                        btnBuscar_ClickC(null, null);
                    }
                    else {

                        script = "alert('Se finaliza correctamente el Servicio del Ticket : " + lbltitulo2.Text + " .');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                        btnBuscar_Click(null, null);
                        btnBuscar_ClickC(null, null); 
                    }



                }
                btnBuscar_ClickC(null, null);

            }
            else
            {
                lblErrores.Text = "La observación es obligatoria para finalizar la importación.";
                lblErrores.Visible = true;
                txtObservacion.Focus();
                mdlPopup.Show();
            }

        }

        protected void btnRegistroCheckOut_Click(object sender, ImageClickEventArgs e)
        {
            mdlPopup.Show();
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lbltitulo2.Text = commandArgs[1].ToString();

            lblNombreCliente.Text = commandArgs[0].ToString();
        }
    }
}