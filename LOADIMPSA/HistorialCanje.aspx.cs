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
    public partial class HistorialCanje : System.Web.UI.Page
    {

        ClienteN clienteN;
        TransportistasN transportistasN;
        EmpleadoN empleadoN;
        TrackingsN trackingsN;
        UsuarioN usuN;
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
                        if (item.rol == 1 )
                        {
                            Button1.Visible = true;




                        }
                        else if (item.rol == 2 || item.rol == 6 || item.rol == 4 || item.rol == 9)
                        {
                            Button1.Visible = false;
                        }
                        else if (item.rol == 3 || item.rol == 5 || item.rol == 7)
                        {
                            Button1.Visible = false;
                            pnlBusquedaClientesCanjeo.Visible = false;
                            pnlClientes.Visible = false;
                        
                            hddIdentificacion.Value = item.identificacion;
                            DatosClientesHistorial(hddIdentificacion.Value, item.cod_usu);
                         
                        }

                        else
                        {
                            Response.Redirect("Home.aspx");
                        }
                    }
                }
            }
        }

        private bool DatosClientes(string identificacion)
        {
            bool save = false;
            pnlListadoHistorial.Visible = true;
            try
            {
                clienteN = new ClienteN();
                var datos = clienteN.DatosClientes(identificacion);
              
            }
            catch (Exception ex)
            {

            }
            return save;
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
            pnlListadoHistorial.Visible = true;
      
            GridViewRow gv = gvClientes.SelectedRow;
            hddIdentificacion.Value = Server.HtmlDecode(gv.Cells[1].Text);
            DatosClientesHistorial(Server.HtmlDecode(gv.Cells[1].Text), Server.HtmlDecode(gv.Cells[3].Text));
            


        }
        protected void pagoConfir_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            mdlPopup.Show();
        }
        protected void dtgHistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }
        protected void btnDebiCredi(object sender, EventArgs e)
        {
            mdlPopup.Show();
          
            pnlRevisar.Visible = true;
           
            btnConfirm.Visible = true;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtObservacion2.Text))
            {
                if (!String.IsNullOrEmpty(txtValor.Text))
                {
                    trackingsN = new TrackingsN();
                bool? res = trackingsN.ActualizaDebiCredi(ddlAccion.SelectedValue, Convert.ToInt32(txtValor.Text), Session["cod_usu"].ToString(), txtObservacion2.Text, hddIdentificacion.Value);

                if (res == true)
                {
                    Session["confirm"] = "alert('Se ha realizado el respectivo: " + ddlAccion.SelectedValue + ".');";
                    Response.Redirect("HistorialCanje.aspx");
                }
                }
                else
                {
                    lblErrores.Text = "El valor es obligatorio.";
                    lblErrores.Visible = true;
                    txtValor.Focus();
                    mdlPopup.Show();
                }
            }
            else
            {
                lblErrores.Text = "La observación es obligatoria para verificar el Pago.";
                lblErrores.Visible = true;
                txtObservacion2.Focus();
                mdlPopup.Show();
            }
        }
        private void DatosClientesHistorial(string identificacion, string cod_usu)
        {
            
            try
            {
                clienteN = new ClienteN();
                usuN = new UsuarioN();
                var datos = clienteN.DatosClienteCanjeHistorial(ConfigurationManager.AppSettings["cnnSQL"], identificacion);
                gvClientesHist.DataSource = datos;
                gvClientesHist.DataBind();

                pnlListadoHistorial.Visible = true;
               
                List<ListaPuntos> puntos = usuN.ListaPuntos(cod_usu);
                if (puntos.Count == 0)
                {
                    lblPuntos.Text = "PUNTOS TOTALES:  0 pts";
                }

                else
                {
                    lblPuntos.Text = "PUNTOS TOTALES:  + " + puntos[0].puntosObtenidos + " pts";
                }

            }
            catch (Exception ex)
            {

            }
           
        }







    }
}

