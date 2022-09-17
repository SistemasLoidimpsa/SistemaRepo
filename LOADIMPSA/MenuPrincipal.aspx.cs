using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Drawing;
using Entidades;
using Negocio;


namespace LOADIMPSA
{

    public partial class MenuPrincipal : System.Web.UI.Page
    {
        ParametrosCorporativosN parametrosCorporativosN;
        ClienteN clienteN;
        TrackingsN trackingsN;
        EmpleadoN empleadoN;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {

                    ParametrosCorporativosN parametrosCorporativosN = new ParametrosCorporativosN();
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        Session["cod_usu"] = item.cod_usu;
                        Session["identificacion"] = item.identificacion;

                        List<ParametrosCorporativosE> parametroDireccionMiami = new List<ParametrosCorporativosE>();
                        List<ParametrosCorporativosE> parametroDireccionGuayqquil = new List<ParametrosCorporativosE>();
                        parametroDireccionMiami = parametrosCorporativosN.BuscaParametrosCorporativos("DIRECCIONBODEGAMIAMI");
                        parametroDireccionGuayqquil = parametrosCorporativosN.BuscaParametrosCorporativos("DIRECCIONBODEGAGUAYAQUIL");
                        foreach (var items in parametroDireccionMiami)
                        {
                            txtDireccionMiami.Text = items.valorchar.ToString();
                        }


                        if (item.rol == 1 || item.rol == 2 || item.rol == 4 || item.rol == 6)
                        {
                            var inicio_anio = Convert.ToString(DateTime.Now.Year);
                            foreach (var items in parametroDireccionGuayqquil)
                            {
                                txtDireccionEcuador.Text = items.valorchar.ToString();
                            }
                            btnActualizarDireccion.Visible = false;
                            txtDireccionEcuador.Enabled = false;
                            CargarListado(inicio_anio);
                            CharClientesAnual.Visible = true;
                        }
                        else
                        {
                            txtDireccionEcuador.Text = item.direccionEntrega;
                            btnActualizarDireccion.Visible = true; 
                            txtDireccionEcuador.Enabled = true;
                            pnlCharCantidadClientes.Visible = false;
                        }


                    }
                }
            }
        }

        string script;
        public void CargarListado(string strAnio)
        {
            try
            { // Para clientes

                trackingsN = new TrackingsN();
                var list = trackingsN.ReportePromDiasTrack(ConfigurationManager.AppSettings["cnnSQL"], strAnio);




                CharClientesAnual.DataSource = list;


                CharClientesAnual.Series["ReportePromedio"].XValueMember = "Mes";
                CharClientesAnual.Series["ReportePromedio"].YValueMembers = "promeDias";
                CharClientesAnual.Series["ReportePromedio"].IsValueShownAsLabel = true;
                CharClientesAnual.ChartAreas["CantidadDias"].AxisX.MajorGrid.Enabled = false;
                CharClientesAnual.ChartAreas["CantidadDias"].AxisY.MajorGrid.Enabled = false;
                CharClientesAnual.Series["ReportePromedio"].SmartLabelStyle.Enabled = true;

                //Para Peso



               
            }
            catch (Exception ex)
            {

            }

        }
        protected void btnActualizarDireccion_Click(object sender, EventArgs e)
        {
            clienteN = new ClienteN();

            if (clienteN.ActualizarDireccion(Session["identificacion"].ToString(), txtDireccionEcuador.Text))
            {
                script = "alert('La dirección de entrega se actualizó.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
            }
            else
            {
                script = "alert('ocurrio un error.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
            }

        }

        protected void CharClientesAnual_Load(object sender, EventArgs e)
        {

        }
    }
}