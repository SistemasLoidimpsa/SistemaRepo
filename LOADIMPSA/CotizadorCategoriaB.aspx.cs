using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace LOADIMPSA
{
    public partial class CotizadorCategoriaB : System.Web.UI.Page
    {

        ClienteN clienteN;
        TransportistasN transportistasN;
        EmpleadoN empleadoN;
        TrackingsN trackingsN;
        CotizacionN cotizacionN;
        ParametrosCorporativosN parametrosCorporativosN;
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
                        if (item.rol == 1)
                        {
                            Session["registro"] = item.identificacion;
                        }
                        else if (item.rol == 4 || item.rol == 2 || item.rol == 6 || item.rol == 9)
                        {
                            Session["registro"] = item.identificacion;
                        }
                        else if (item.rol == 3 || item.rol == 5 || item.rol == 7)
                        {
                            pnlBusquedaClientes.Visible = false;
                            DatosClientes(item.identificacion);
                            pnlDatosCliente.Visible = true;
                            pnlCotizador.Visible = true;
                            Session["registro"] = item.identificacion;
                        }
                        else
                        {
                            Response.Redirect("MenuPrincipal.aspx");
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
            pnlCotizador.Visible = true;
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

        protected void txtPeso_TextChanged(object sender, EventArgs e)
        {
            ParametrosCorporativosN parametrosCorporativosN = new ParametrosCorporativosN();

            if (!String.IsNullOrEmpty(txtPeso.Text))
            {
                if (Convert.ToDouble(txtPeso.Text) > 8.5)
                {
                    script = "alert('El peso en Categoría B no puede exceder las 8.5 libras.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtPeso.Text = string.Empty;
                    txtPeso.Focus();
                }
                else
                {
                    List<ParametrosCorporativosE> parametroTaza = new List<ParametrosCorporativosE>();
                    parametroTaza = parametrosCorporativosN.BuscaParametrosCorporativos("TAZA");
                    double taza = 0;
                    foreach (var items in parametroTaza)
                    {
                        taza = Convert.ToDouble(items.valordecimal);
                    }
                    
                    double respuesta = Math.Round((Convert.ToDouble(txtPeso.Text) * taza), 2);
                    lblServicioCorrier.Text = (respuesta).ToString();
                    double sumatoria = respuesta;
                    lblSumatoria.Text = sumatoria.ToString();
                }
            }


        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            CotizacionN cotizacionN = new CotizacionN();

            var respuesta = cotizacionN.InsertaCotizacionB("","",lBlCed.Text, "B", txtDescripcionMercaderia.Text, Convert.ToDecimal(txtFactura.Text),
                Convert.ToDecimal(txtPeso.Text), Convert.ToDecimal(lblServicioCorrier.Text),
                Convert.ToDecimal(lblSumatoria.Text), Session["registro"].ToString(), 0, 0, 0, 0);

            if (respuesta.id_codigoContizacion > 0)
            {
                script = "alert('La cotización del Cliente: " + lblNombresCom.Text + " se ha registrado correctamente.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                Session["strCliente"] = respuesta.id_codigoContizacion;
                string s = "window.open('ReporteCotizaciones.aspx', '_new_tab');";

                ScriptManager.RegisterStartupScript(uppListadoTracking, uppListadoTracking.GetType(), "script", s, true);

                LimpiarCajas();
              
            }
            else
            {
                script = "alert('Ocurrio un error comuniquese con el administrador.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
            }
        }


        public void LimpiarCajas()
        {
            txtDescripcionMercaderia.Text = string.Empty;
            txtFactura.Text = string.Empty;
            txtPeso.Text = string.Empty;
            lblServicioCorrier.Text = string.Empty;
            lblSumatoria.Text = string.Empty;
            pnlCotizador.Visible = false;
            pnlDatosCliente.Visible = false;
            pnlClientes.Visible = false;
            txtCliente.Text = string.Empty;
        }

        protected void txtFactura_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtFactura.Text))
            {
                
                if (Convert.ToDouble(txtFactura.Text) > 400)
                {
                    script = "alert('El valor FOB debe ser menor o igual a $ 400.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtFactura.Text = string.Empty;
                    txtFactura.Focus();
                }
            }
        }
    }
}