using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace LOADIMPSA
{
    public partial class AdministracionEnvios : System.Web.UI.Page
    {
        string script;
        double sumatoria;
        TrackingsN trackingsN;
        EmpleadoN empleadoN;
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnExcel);


            if (!IsPostBack)
            { // cargar Fechas en Textbox


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
                    var mes = DateTime.Now.Month - 1;
                    var dia = DateTime.Now.Day;
                    DateTime fechaInicio;

                    List<int> list = new List<int>() { 29, 30, 31 };
                    if (mes == 2 && list.Contains(dia))
                    {
                        fechaInicio = new DateTime(DateTime.Now.Year, (mes), 28);
                    }
                    if (mes == 4 && list.Contains(dia))
                    {
                        fechaInicio = new DateTime(DateTime.Now.Year, (mes), dia - 1);
                    }
                    else
                    {
                        fechaInicio = new DateTime(DateTime.Now.Year, (mes), dia);
                    }


                    txtFechaIngreso.Text = fechaInicio.ToString("yyyy-MM-dd");
                    DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    txtFechaRecbidoMiami.Text = fechaFin.ToString("yyyy-MM-dd");
                }

                CargarEmpleados();
                dt.Columns.AddRange(new DataColumn[6] { new DataColumn("numeroOrdenInterno", typeof(string)),
                                                            new DataColumn("cedulaCliente", typeof(string)),
                                                            new DataColumn("NombreCompleto", typeof(string)),
                                                            new DataColumn("peso", typeof(string)),
                                                            new DataColumn("precio", typeof(string)),
                                                            new DataColumn("estado", typeof(string))});
                Session["enviosGestion"] = dt;
                if (Session["usuario"] != null)
                {
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        Session["cod_usu"] = item.cod_usu;
                        if (item.rol == 1 || item.rol == 4)
                        {
                            if (item.rol == 4)
                            {
                                dtgListadoTrackingFiltro.Columns[14].Visible = false;
                                dtgListadoTrackingFiltro.Columns[13].Visible = false;
                            }
                            
                            CargarListado("%", "%", "%", Convert.ToDateTime(txtFechaIngreso.Text), Convert.ToDateTime(txtFechaRecbidoMiami.Text), "%","%");
                            btnReversarEstado.Visible = true;



                        }
                        else if (item.rol == 2 || item.rol == 6)
                        {
                            dtgListadoTrackingFiltro.Columns[14].Visible = false;
                            dtgListadoTrackingFiltro.Columns[13].Visible = false;
                            ddlEjecutivos.SelectedValue = item.identificacion;
                            //ddlEjecutivos.Enabled = false;
                            CargarListado("%", "%", "%", Convert.ToDateTime(txtFechaIngreso.Text), Convert.ToDateTime(txtFechaRecbidoMiami.Text), ddlEjecutivos.SelectedValue, ddlCategoria.SelectedValue);
                            btnReversarEstado.Visible = false;
                            btnReversarGuias.Visible = false;
                            dtgListadoTrackingFiltro.Columns[12].Visible = false;


                        }
                    }
                }
            }
        }


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

        
        private void CargarListado(string strEstadoTrackings, string strNroOrden, string strNroTracking
                                   , DateTime? datFechaIngreso, DateTime? datFechaRecibidoMiami, string strEjecutivo, string categoria)
        {



            trackingsN = new TrackingsN();
            var list = trackingsN.ListadoTrackingsFiltro(ConfigurationManager.AppSettings["cnnSQL"], strEstadoTrackings, strNroOrden, strNroTracking
                , datFechaIngreso, datFechaRecibidoMiami, strEjecutivo, categoria);
            
            
            
            dtgListadoTrackingFiltro.DataSource = list;
            sumatoria =sumarListado(list);
            dtgListadoTrackingFiltro.DataBind();

        }

        private double sumarListado(List<InformacionTrackingE> list)
        {

            double suma = list.Sum(item => Convert.ToDouble(item.peso));

            return suma;

        }

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


                CargarListado(ddlEstado.SelectedValue
                    , String.IsNullOrEmpty(txtNroOrdenInterna.Text) ? "%" : txtNroOrdenInterna.Text
                    , String.IsNullOrEmpty(txtNroTracking.Text) ? "%" : txtNroTracking.Text
                    , datFechaIngreso
                    , datFechaRecibidoMiami
                    , ddlEjecutivos.SelectedValue, ddlCategoria.SelectedValue);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }
        private void ExportGridToExcel()
        {
            StringBuilder builder = new StringBuilder();
            string strFileName = "ReportePanel_" + DateTime.Now.ToShortDateString() + ".csv";
            builder.Append("No. Orden;ID Tracking;Cliente;Nombre Completo;Peso;Precio;Estado;Observaciones;Descripcion;Categoria;Bodega;Fecha Recibido Miami;Fecha Registro" + Environment.NewLine);
            foreach (GridViewRow row in dtgListadoTrackingFiltro.Rows)
            {
                string noOrden = HttpUtility.HtmlDecode( row.Cells[1].Text);
                string traking = HttpUtility.HtmlDecode("'"+row.Cells[2].Text.ToString());
                string clienteId = HttpUtility.HtmlDecode(row.Cells[3].Text);
                string nombreCliente = HttpUtility.HtmlDecode(row.Cells[4].Text);
                string peso = HttpUtility.HtmlDecode(row.Cells[7].Text);
                string precio = HttpUtility.HtmlDecode(row.Cells[8].Text);
                string estado = HttpUtility.HtmlDecode(row.Cells[9].Text);
                string obs = HttpUtility.HtmlDecode(row.Cells[10].Text).Replace(";", ",").Replace("&", ",").Replace("\n", " ");
                string descp = HttpUtility.HtmlDecode(row.Cells[11].Text).Replace(";", ",").Replace("&", ",").Replace("\n", " ");
                string categoria = HttpUtility.HtmlDecode(row.Cells[12].Text);
                string fechaMiami = HttpUtility.HtmlDecode(row.Cells[13].Text);
                string fechaRegistro = HttpUtility.HtmlDecode( row.Cells[14].Text);
                string bodega = HttpUtility.HtmlDecode(row.Cells[16].Text);

                builder.Append(noOrden + ";" + traking + ";" + clienteId + ";" + nombreCliente + ";" + peso + ";" + precio + ";" + estado + ";" + obs + ";" + descp + ";" + categoria + ";" + bodega + ";" + fechaMiami + ";" + fechaRegistro + Environment.NewLine);
            }
            Response.Clear();
            Response.ContentEncoding = Encoding.Default;
            Response.Charset = "utf-8";
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
           
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            Response.Write(builder.ToString());
            Response.End();


        }

        protected void dtgListadoTrackingFiltro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                
                
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    if (e.Row.Cells[9].Text == "RECIBIDO EN MIAMI")
                    {
                        (e.Row.FindControl("chkEstado") as CheckBox).Enabled = false;
                        (e.Row.FindControl("chkEstado") as CheckBox).BackColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("chkEstado") as CheckBox).ToolTip = "El envio esta a espera de que el cliente autorice para volar.";
                    }
                    else if (e.Row.Cells[9].Text == "BODEGA LOIDIMPSA")
                    {
                        (e.Row.FindControl("chkEstado") as CheckBox).Enabled = false;
                        (e.Row.FindControl("chkEstado") as CheckBox).BackColor = System.Drawing.Color.Beige;
                        (e.Row.FindControl("chkEstado") as CheckBox).ToolTip = "El envio ya se encuentra en la Bodega Loidimpsa.";
                    }
                    else if (e.Row.Cells[9].Text == "FINALIZADO")
                    {
                        (e.Row.FindControl("chkEstado") as CheckBox).Enabled = false;
                        (e.Row.FindControl("chkEstado") as CheckBox).BackColor = System.Drawing.Color.Aquamarine;
                        (e.Row.FindControl("chkEstado") as CheckBox).ToolTip = "El envio ya se encuentra finalizado y entregado al Cliente.";
                        dtgListadoTrackingFiltro.Columns[13].Visible = false;
                    }

                    else
                    {
                        (e.Row.FindControl("chkEstado") as CheckBox).Enabled = true;
                        (e.Row.FindControl("chkEstado") as CheckBox).BackColor = System.Drawing.Color.LightGreen;
                        (e.Row.FindControl("chkEstado") as CheckBox).ToolTip = "El envio se puede cambiar de estado.";
                    }

                    if (e.Row.Cells[12].Text == "C")
                    {
                        e.Row.Cells[12].ControlStyle.Font.Bold = true;
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Blue;
                    }
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    //e.Row.Cells[0].Text = "Total: ";
                    e.Row.Cells[7].Text = "Total: " +  sumatoria;
                }

            }
            catch (Exception ex)
            {

            }
        }


        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            DataTable dp = Session["enviosGestion"] as DataTable;
            dtgListadoEnviosGestionar.DataSource = dp;
            dtgListadoEnviosGestionar.DataBind();
            btnCambiarEstado.Visible = true;
            btnReversarEstado.Visible = false;
            pnlGestion.Visible = true;
            txtObservaciones.Text = string.Empty;
            pnlHistorial.Visible = false;
            lblTituloV.Text = "GESTIÓN DE GUÍAS";
            mdlPopup.Show();
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();
            int contador = 0;
            foreach (GridViewRow gr in dtgListadoEnviosGestionar.Rows)
            {
                var respuesta = trackingsN.ActualizaEnvioAdministrador(Convert.ToInt32(gr.Cells[0].Text), gr.Cells[5].Text, txtObservaciones.Text, Session["cod_usu"].ToString());
                if (respuesta == true)
                {
                    contador++;
                }
            }
            DataTable dt = Session["enviosGestion"] as DataTable;
            dt.Clear();
            //dt.Columns.AddRange(new DataColumn[6] { new DataColumn("numeroOrdenInterno", typeof(string)),
            //                                                new DataColumn("cedulaCliente", typeof(string)),
            //                                                new DataColumn("NombreCompleto", typeof(string)),
            //                                                new DataColumn("peso", typeof(string)),
            //                                                new DataColumn("precio", typeof(string)),
            //                                                new DataColumn("estado", typeof(string))});
            //Session["enviosGestion"] = dt;

            script = "alert('El Empleado a cambiado de estado a: " + contador + " envios.');";
            ScriptManager.RegisterStartupScript(updPnlCustomerDetail, updPnlCustomerDetail.GetType(), "script", script, true);
            btnBuscar_Click(null, null);
        }





        protected void chkEstado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {


                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                int index = row.RowIndex;
                CheckBox cb1 = (CheckBox)dtgListadoTrackingFiltro.Rows[index].FindControl("chkEstado");
                if (cb1.Checked == true)
                {
                    DataTable dp = Session["enviosGestion"] as DataTable;
                    DataRow dr1 = dp.NewRow();
                    dr1[0] = Server.HtmlDecode(dtgListadoTrackingFiltro.Rows[index].Cells[1].Text);
                    dr1[1] = Server.HtmlDecode(dtgListadoTrackingFiltro.Rows[index].Cells[3].Text);
                    dr1[2] = Server.HtmlDecode(dtgListadoTrackingFiltro.Rows[index].Cells[4].Text);
                    dr1[3] = Server.HtmlDecode(dtgListadoTrackingFiltro.Rows[index].Cells[7].Text);
                    dr1[4] = Server.HtmlDecode(dtgListadoTrackingFiltro.Rows[index].Cells[8].Text);
                    dr1[5] = Server.HtmlDecode(dtgListadoTrackingFiltro.Rows[index].Cells[9].Text);
                    dp.Rows.Add(dr1);

                    Session["enviosGestion"] = dp;
                }
                else if (cb1.Checked == false)
                {
                    DataTable dp = Session["enviosGestion"] as DataTable;
                    for (int i = dp.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dp.Rows[i];
                        if (((dr["numeroOrdenInterno"]).ToString() == Server.HtmlDecode(dtgListadoTrackingFiltro.Rows[index].Cells[1].Text)))
                        {
                            dr.Delete();
                        }
                    }
                    //dtgPagosSolicitudes.DataSource = dp;
                    //dtgPagosSolicitudes.DataBind();
                    Session["enviosGestion"] = dp;
                }
            }
            catch (Exception ex)
            {

            }
        }


        protected void btnHistorial_Click(object sender, ImageClickEventArgs e)
        {
            mdlPopup.Show();
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lblTituloV.Text = "HISTORIAL DE ORDEN / TRANSPORTISTA";
            Label13.Text = commandArgs[0].ToString() + " / " + commandArgs[1];
            string rutaFisica = commandArgs[2].ToString();
            hddImgFactura.Value = rutaFisica;
            var estado = commandArgs[3].ToString();
            if (estado == "AUTORIZADO PARA VOLAR" || estado == "EN TRANSITO Y PROCESO DE ADUANA")
            {
                btnVerFactura.Visible = true;
                if (String.IsNullOrEmpty(rutaFisica))
                {
                    btnVerFactura.BackColor = Color.FromArgb(101, 108, 122);
                    btnVerFactura.Enabled = false;
                }
                else
                {
                    btnVerFactura.BackColor = Color.FromArgb(29, 66, 137);
                    btnVerFactura.Enabled = true;

                }



            }

            CargarHistorialTracking(Convert.ToInt32(commandArgs[0].ToString()));
            btnCambiarEstado.Visible = false;
            pnlGestion.Visible = false;
            pnlHistorial.Visible = true;
        }

        protected void btnVerFactura_Click(object sender, EventArgs e)
        {


            string[] subs = hddImgFactura.Value.Split('/');
            string nameFile = subs[4];
            string inicRuta = subs[0];
            string foldeLocal = subs[1];
            string cedula = subs[2];
            string idOrden = subs[3];
            string folderLocalFull = inicRuta + "/" + foldeLocal + "/" + cedula + "/";
            string folderLocalFull2 = inicRuta + "/" + foldeLocal + "/" + cedula + "/" + idOrden + "/";
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string folder = path + "/tempFactura/";
            string fullFilePath = "/tempFactura/" + nameFile;
            string fullFile = path + "/tempFactura/" + nameFile;

            //Crear la carpeta tempFactura




            // Copiar en la Carpeta TempFactura

            if (File.Exists(hddImgFactura.Value))
            {
                if (!File.Exists(fullFile))
                {
                    File.Copy(hddImgFactura.Value, folder + "/" + nameFile);
                }
                if (File.Exists(fullFile))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Open", "window.open('" + fullFilePath + "');", true);
                }
                else
                {
                    script = "alert('No se pudo copiar el archivo.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                }

            }
            else
            {
                script = "alert('No se encuentra el archivo en Pago.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
            }



        }
        public void CargarHistorialTracking(int idOrden)
        {
            trackingsN = new TrackingsN();
            var lista = trackingsN.HistorialTracking(ConfigurationManager.AppSettings["cnnSQL"], idOrden);
            dtgHistorialTracking.DataSource = lista;
            dtgHistorialTracking.DataBind();
        }

        protected void btnReversarGuias_Click(object sender, EventArgs e)
        {
            DataTable dp = Session["enviosGestion"] as DataTable;
            dtgListadoEnviosGestionar.DataSource = dp;
            dtgListadoEnviosGestionar.DataBind();
            btnCambiarEstado.Visible = false;
            btnReversarEstado.Visible = true;
            pnlGestion.Visible = true;
            txtObservaciones.Text = string.Empty;
            pnlHistorial.Visible = false;
            lblTituloV.Text = "REVERSO DE GUÍAS";
            mdlPopup.Show();
        }

        protected void btnReversarEstado_Click(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();
            int contador = 0;
            foreach (GridViewRow gr in dtgListadoEnviosGestionar.Rows)
            {
                var respuesta = trackingsN.ReversaEnvioAdministrador(Convert.ToInt32(gr.Cells[0].Text), gr.Cells[5].Text, txtObservaciones.Text, Session["cod_usu"].ToString());
                if (respuesta == true)
                {
                    contador++;
                }
            }
            DataTable dt = Session["enviosGestion"] as DataTable;
            dt.Clear();
            //dt.Columns.AddRange(new DataColumn[6] { new DataColumn("numeroOrdenInterno", typeof(string)),
            //                                                new DataColumn("cedulaCliente", typeof(string)),
            //                                                new DataColumn("NombreCompleto", typeof(string)),
            //                                                new DataColumn("peso", typeof(string)),
            //                                                new DataColumn("precio", typeof(string)),
            //                                                new DataColumn("estado", typeof(string))});
            //Session["enviosGestion"] = dt;

            script = "alert('El Empleado a reversado de estado a: " + contador + " envios.');";
            ScriptManager.RegisterStartupScript(updPnlCustomerDetail, updPnlCustomerDetail.GetType(), "script", script, true);
            btnBuscar_Click(null, null);
        }



        protected void btnBorrarGuia_Click(object sender, ImageClickEventArgs e)
        {
            string confirmvalue = Request.Form["confirm_value"];
            var respuesta = confirmvalue.Split(',');
            if (respuesta.LastOrDefault() == "Yes")
            {
                string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
                trackingsN = new TrackingsN();
                var respuestas = trackingsN.EliminaEnvioAdministrador(Convert.ToInt32(commandArgs[0].ToString()), "ELIMINADO", "Orden eliminada por el Administrador", Session["cod_usu"].ToString());
                if (respuestas == true)
                {
                    script = "alert('La orden:" + commandArgs[0].ToString() + " se a eliminado correctamente.');";
                    ScriptManager.RegisterStartupScript(updPnlCustomerDetail, updPnlCustomerDetail.GetType(), "script", script, true);
                    btnBuscar_Click(null, null);
                }
            }
        }
    }
}