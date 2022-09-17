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
    public partial class ConsultaCanje : System.Web.UI.Page

    {
        ClienteN clienteN;
        /*TransportistasN transportistasN;
        EmpleadoN empleadoN;
        TrackingsN trackingsN;
        string script;*/
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

                        }
                        else if (item.rol == 2 || item.rol == 6 || item.rol == 8 || item.rol == 9)
                        {
                         
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
                 
                    var lista = new ClienteN().BuscarCanje(ConfigurationManager.AppSettings["cnnSQL"], txtCliente.Text.Trim());
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
            DatosClientes(Server.HtmlDecode(gv.Cells[1].Text), Server.HtmlDecode(gv.Cells[3].Text));
            pnlDatosCliente.Visible = true;


        }

        private bool DatosClientes(string identificacion, string codigoCanje)
        {
            bool save = false;
            try
            {
                clienteN = new ClienteN();
                var datos = clienteN.DatosClienteCanje(identificacion, codigoCanje);
                if (datos.numeroidentificacion != null)
                {
                    lblNombresCom.Text = datos.nombresCompletos;
                    lBlCed.Text = datos.numeroidentificacion.ToString();
                    lblCodCliente.Text = datos.codigoCanje.ToString();
                    lblIdCanje.Text = datos.idCangeo.ToString();

                    lblFechaCanje.Text = datos.fechaCanjeo;
                    lblNombreProducto.Text = datos.nombreProducto.ToString();
                    lblPuntosUsados.Text = datos.puntosUsados.ToString();
                    lblPuntosacumulados.Text = datos.puntosAcumulado.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return save;
        }
   

     
   



    }
}

