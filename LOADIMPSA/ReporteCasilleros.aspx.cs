using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Linq;

using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace LOADIMPSA
{
    public partial class ReporteCasilleros : System.Web.UI.Page
    {
        ClienteN clienteN;
        TrackingsN trackingsN;
        EmpleadoN empleadoN;
        string script;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlCharCantidadClientes.Visible = false;
                if (Session["usuario"] != null)

                {
                 
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {   
                       
                        if (item.rol == 1)

                        {
                           
                            //   ObtenerDatos();
                        }
                        else if (item.rol == 4)
                        {
                           // ObtenerDatos();


                        }
                        else if (item.rol == 2)
                        {
                          
                           
                        }
                        else
                        {
                           
                        }
                    }
                }
                else
                {
                    Response.Redirect("ReporteCasilleros.aspx");
                }
            }
        }
        public void CargarListado(string strAnio)
        {
            try
            { // Para clientes
               
                clienteN = new ClienteN();
                var list = clienteN.ReporteCasillero(ConfigurationManager.AppSettings["cnnSQL"], strAnio);

               

              
                CharClientesAnual.DataSource = list;
              
                
                CharClientesAnual.Series["ReporteCasilleros"].XValueMember = "Mes";
                CharClientesAnual.Series["ReporteCasilleros"].YValueMembers = "cantidadMes";
                CharClientesAnual.Series["ReporteCasilleros"].IsValueShownAsLabel = true;
                CharClientesAnual.ChartAreas["CantidadClientes"].AxisX.MajorGrid.Enabled = false;
                CharClientesAnual.ChartAreas["CantidadClientes"].AxisY.MajorGrid.Enabled = false;
                CharClientesAnual.Series["ReporteCasilleros"].SmartLabelStyle.Enabled = true;

                //Para Peso



                trackingsN = new TrackingsN();
                var list2 = trackingsN.ReportePesoAnual(ConfigurationManager.AppSettings["cnnSQL"], strAnio);

              
                Title title2 = new Title();
                title2.Font = new Font("Arial", 14, FontStyle.Bold);
                title2.ForeColor = System.Drawing.Color.FromArgb(20, 6, 102);
                title2.Text = "PESO TOTAL MENSUAL";

               // var al = list2.Select(x => new { mes = x.Mes, cantidadMes = x.cantidadMes }).ToList();

                ChartPesoAnual.Titles.Add(title2);
                //       ChartPesoAnual.DataSource = list2;
                ChartPesoAnual.DataBindCrossTable(list2, "peso", "Mes", "cantidadMes", "Label=cantidadMes");


                //ChartPesoAnual.Series["ReportePeso"].Points.DataBind(al, "mes", "cantidadMes", "");
                ChartPesoAnual.Series["Peso-Saliente"].SmartLabelStyle.Enabled = false;
                ChartPesoAnual.Series["Peso-Entrante"].SmartLabelStyle.Enabled = false;
                ChartPesoAnual.Series["Peso-Saliente"].CustomProperties = "DrawingStyle=LightToDark,  MaxPixelPointWidth=40";
                ChartPesoAnual.Series["Peso-Entrante"].CustomProperties = "DrawingStyle=LightToDark,   MaxPixelPointWidth=40";
                ChartPesoAnual.Series["Peso-Saliente"].LabelAngle = -90;
                ChartPesoAnual.Series["Peso-Entrante"].LabelAngle = -90;
                ChartPesoAnual.ChartAreas["CantidadPeso"].AxisX.MajorGrid.Enabled = false;
                ChartPesoAnual.ChartAreas["CantidadPeso"].AxisY.LabelStyle.Format = "0.0";
                ChartPesoAnual.ChartAreas["CantidadPeso"].AxisY.MajorGrid.Enabled = false;


                //Para Categoria


                clienteN = new ClienteN();
                var list3 = clienteN.ReporteCategoriaRed(ConfigurationManager.AppSettings["cnnSQL"], strAnio);

               


                Title title3 = new Title();
                title3.Font = new Font("Arial", 14, FontStyle.Bold);
                title3.ForeColor = System.Drawing.Color.FromArgb(20, 6, 102);
                title3.Text = "CANTIDAD DE CLIENTES POR RED SOCIAL";

                var al3 = list2.Select(x => new { mes = x.Mes, cantidadMes = x.cantidadMes }).ToList();

                CharClientesCategoria.Titles.Add(title3);
                //       ChartPesoAnual.DataSource = list2;
                CharClientesCategoria.DataBindCrossTable(list3, "campoMarketing", "Mes", "cantidadMes", "Label=cantidadMes");


                //ChartPesoAnual.Series["ReportePeso"].Points.DataBind(al, "mes", "cantidadMes", "");
                CharClientesCategoria.Series[0].ChartType = SeriesChartType.Line;
                CharClientesCategoria.Series[1].ChartType = SeriesChartType.Line;
                CharClientesCategoria.Series[2].ChartType = SeriesChartType.Line;
                CharClientesCategoria.Series[3].ChartType = SeriesChartType.Line;
                CharClientesCategoria.Series[4].ChartType = SeriesChartType.Line;
                CharClientesCategoria.Series[5].ChartType = SeriesChartType.Line;

                CharClientesCategoria.Series[0].BorderWidth = 6;
                CharClientesCategoria.Series[1].BorderWidth = 6;
                CharClientesCategoria.Series[2].BorderWidth = 6;
                CharClientesCategoria.Series[3].BorderWidth = 6;
                CharClientesCategoria.Series[4].BorderWidth = 6;
                CharClientesCategoria.Series[5].BorderWidth = 6;

                CharClientesCategoria.Series[0].SmartLabelStyle.Enabled = false;
                CharClientesCategoria.Series[1].SmartLabelStyle.Enabled = false;
                CharClientesCategoria.Series[2].SmartLabelStyle.Enabled = false;
                CharClientesCategoria.Series[3].SmartLabelStyle.Enabled = false;
                CharClientesCategoria.Series[4].SmartLabelStyle.Enabled = false;
                CharClientesCategoria.Series[5].SmartLabelStyle.Enabled = false;

                CharClientesCategoria.ChartAreas["ClientesCategoria"].AxisX.MajorGrid.Enabled = false;
                CharClientesCategoria.ChartAreas["ClientesCategoria"].AxisY.LabelStyle.Format = "0.0";
                CharClientesCategoria.ChartAreas["ClientesCategoria"].AxisY.MajorGrid.Enabled = false;



            }
            catch (Exception ex)
            {

            }

        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //   pnlListado.Visible = true;
            try
            {
                
                CharClientesAnual.DataSource = null;
                CharClientesAnual.DataBind();
                ChartPesoAnual.DataSource = null;
                ChartPesoAnual.DataBind();
                ChartPesoAnual.DataSource = null;
                CharClientesCategoria.DataBind();
                CharClientesCategoria.DataSource = null;
                pnlCharCantidadClientes.Visible = true;
                if ( (ddlstrAnio.SelectedValue != "%"))
                {


                    CargarListado(ddlstrAnio.SelectedValue);
                   CharClientesAnual.Visible = true;
                    ChartPesoAnual.Visible = true;
                    CharClientesCategoria.Visible = true;

                }
                else
                {
                    script = "alert('La debe seleccionar el año para buscar.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

                }
            }
            catch (Exception)
            {
                script = "alert('La porfavor de en buscar.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

            }
        }

        protected void CharClientesAnual_Load(object sender, EventArgs e)
        {

        }
    }
    }
