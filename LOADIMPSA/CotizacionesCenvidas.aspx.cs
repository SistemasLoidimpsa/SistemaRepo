using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
namespace LOADIMPSA
{
    public partial class CotizacionesCenvidas : System.Web.UI.Page
    {
        ClienteN clienteN;
        TrackingsN trackingsN;
        EmpleadoN empleadoN;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var inicio_mes = DateTime.Now.Month;
                if (inicio_mes == 1)
                {
                    DateTime fechaInicio = new DateTime(DateTime.Now.Year - 1, 12, DateTime.Now.Day);
                    txtFechaIngreso.Text = fechaInicio.ToString("yyyy-MM-dd");
                    DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    txtFechaRecbidoMiami.Text = fechaFin.ToString("yyyy-MM-dd");

                }
                else
                {

                    DateTime fechaInicio = new DateTime(DateTime.Now.Year, (DateTime.Now.Month - 1), DateTime.Now.Day);
                    txtFechaIngreso.Text = fechaInicio.ToString("yyyy-MM-dd");
                    DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    txtFechaRecbidoMiami.Text = fechaFin.ToString("yyyy-MM-dd");
                }

                CargarEmpleados();

                if (Session["confirm"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", Session["confirm"].ToString(), true);
                    Session["confirm"] = null;
                }

                if (Session["usuario"] != null)
                {
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        // cargar Fechas en Textbox
                        Session["rol"] = item.rol;
                        Session["cod_usu"] = item.cod_usu;

                        if (item.rol == 1 || item.rol == 4)
                        {
                            if (item.rol == 4)
                            {
                                
                                dtgEnvios.Columns[11].Visible = false;
                                dtgEnvios.Columns[12].Visible = false;
                            }
                            CargarListado(Convert.ToDateTime(txtFechaIngreso.Text), Convert.ToDateTime(txtFechaRecbidoMiami.Text), ddlEjecutivos.SelectedValue);




                        }
                        else if (item.rol == 2 || item.rol == 6)
                        {
              
                            dtgEnvios.Columns[11].Visible = false;
                            dtgEnvios.Columns[12].Visible = false;
                            ddlEjecutivos.SelectedValue = item.identificacion;
                            //ddlEjecutivos.Enabled = false;
                            CargarListado( Convert.ToDateTime(txtFechaIngreso.Text), Convert.ToDateTime(txtFechaRecbidoMiami.Text), ddlEjecutivos.SelectedValue);
                          


                        }

                        
                    }

                }
                else
                {
                    Response.Redirect("MenuPrincipal.aspx");
                }

            }
        }

        public void CargarListado(DateTime? strFechaInicio, DateTime? strFechaFin, string strUsuario)
        {
            try
            {
             
                var datos = new ReportesN().ConsultaCotizacionN(ConfigurationManager.AppSettings["cnnSQL"], strFechaInicio, strFechaFin, strUsuario);
                dtgEnvios.DataSource = datos;
                dtgEnvios.DataBind();
            }
            catch (Exception ex)
            {

            }

        }

        protected void dtgEnviosDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void dtgEnvios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            trackingsN = new TrackingsN();

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                  
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime? datFechaIngreso;
            DateTime? datFechaRecibidoMiami;

            if (String.IsNullOrEmpty(txtFechaIngreso.Text) || String.IsNullOrEmpty(txtFechaRecbidoMiami.Text))
            {
                script = "alert('Se debe ingresar las fechas de Inicio y Fecha Fin.');";
                ScriptManager.RegisterStartupScript(updPnlCustomerDetail, updPnlCustomerDetail.GetType(), "script", script, true);
            }
            else
            {
                if (String.IsNullOrEmpty(txtFechaIngreso.Text))
                    datFechaIngreso = null;
                else
                    datFechaIngreso = Convert.ToDateTime(txtFechaIngreso.Text);

                if (String.IsNullOrEmpty(txtFechaRecbidoMiami.Text))
                    datFechaRecibidoMiami = null;
                else
                    datFechaRecibidoMiami = Convert.ToDateTime(txtFechaRecbidoMiami.Text);


                CargarListado(datFechaIngreso, datFechaRecibidoMiami, ddlEjecutivos.SelectedValue);
                pnlListado.Visible = true;
            }
        }


        string script;


        private void CargarEmpleados()
        {
            empleadoN = new EmpleadoN();
            ddlEjecutivos.Items.Clear();
            var docentes = empleadoN.Empleados();
            ddlEjecutivos.Items.Add(new ListItem("Todos", "%"));
            foreach (var item in docentes)
            {
                ddlEjecutivos.Items.Add(new ListItem(item.Value, item.Key));
            }
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();
            if (!String.IsNullOrEmpty(txtObservacion.Text))
            {
                if (!String.IsNullOrEmpty(txtNumFacturaContf.Text))
                {
                    bool? res = trackingsN.ActualizaCheckOut(Convert.ToInt32(lbltitulo2.Text), txtObservacion.Text, Session["cod_usu"].ToString(), ddlMetodoPagoConfir.SelectedValue, txtNumFacturaContf.Text, "","");

                    if (res == true)
                    {
                        Session["confirm"] = "alert('Se finaliza correctamente la importación: " + lbltitulo2.Text + ".');";
                        Response.Redirect("BodegaLoidimpsa.aspx");
                    }
                }
                else
                {
                    lblErrores.Text = "La factura es obligatoria para finalizar la importación.";
                    lblErrores.Visible = true;
                    txtNumFacturaContf.Focus();
                    mdlPopup.Show();
                }

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

        protected void btnRegistroCheckOut_ClickFile(object sender, ImageClickEventArgs e)
        {
              string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
                Session["strCliente"] =  commandArgs[0].ToString();
                string s = "window.open('ReporteC.aspx', '_new_tab');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", s, true);


           


            //   Process.Start(fullFilePath);


            //  string filesPath = Server.MapPath(@"~/PAGOS/" + cedula+"/"+ nameFile);
            // Get list of files from the path.
            /*
                        Process process = new Process();
                                process.StartInfo.UseShellExecute = true;
                                ProcessStartInfo psStartInfo = new ProcessStartInfo();
                                psStartInfo.FileName = filesPath;
                                Process ps = Process.Start(psStartInfo);

                         //   ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('File Not found');", true);
              */
        }

        protected void pagoConfir_SelectedIndexChanged(object sender, EventArgs e)
        {
            mdlPopup.Show();
        }
    }
}