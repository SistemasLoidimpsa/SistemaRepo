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
    public partial class AdministraciónCotizaciones : System.Web.UI.Page
    {
        string script;
        CotizacionN cotizacionN;
        EmpleadoN empleadoN;
        ClienteN clienteN;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    CargarEmpleados();
                    CargarEmpleadosAdministrador();
                    CargarEmpleadosreporte();
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        Session["cod_usu"] = item.cod_usu;
                        Session["identificacion"] = item.identificacion;
                        Session["rol"] = item.rol;

                        // cargar Fechas en Textbox
                        DateTime fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        txtFechaInicio.Text = fechaInicio.ToString("yyyy-MM-dd");
                        DateTime fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        txtFechaFin.Text = fechaFin.ToString("yyyy-MM-dd");

                        // cargar Fechas en Textbox Reporte

                        txtFechaInicioReporte.Text = fechaInicio.ToString("yyyy-MM-dd");
                        txtFechaFinReporte.Text = fechaFin.ToString("yyyy-MM-dd");


                        if (item.rol == 1)
                        {
                            CargarVentas(ddlEjecutivosAdministrador.SelectedValue,
                                Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text));
                            CargarVentasReporte(ddlEjecutovosReporte.SelectedValue,
                               Convert.ToDateTime(txtFechaInicioReporte.Text), Convert.ToDateTime(txtFechaFinReporte.Text)
                               , ddlEstado.SelectedValue);
                            tpnlRegistrarVentas.Visible = true;
                            tpnlAdministrador.Visible = true;
                            tpnlReporteVentas.Visible = true;
                        }
                        else if (item.rol == 4 || item.rol == 9)
                        {
                            lblEmpleados.Visible = false;
                            ddlEjecutivos.Visible = false;
                            ddlEjecutivos.SelectedValue = item.identificacion; 
                            tpnlRegistrarVentas.Visible = true;
                            tpnlAdministrador.Visible = false;
                            tpnlReporteVentas.Visible = true;
                        }
                        else if (item.rol == 2 || item.rol == 6)
                        {
                            lblEmpleados.Visible = false;
                            ddlEjecutivos.Visible = false;
                            ddlEjecutivos.SelectedValue = item.identificacion;
                            ddlEjecutivosAdministrador.SelectedValue = item.identificacion;
                            ddlEjecutivosAdministrador.Enabled = false;
                            ddlEjecutovosReporte.SelectedValue = item.identificacion;
                            ddlEjecutovosReporte.Enabled = false;
                            tpnlRegistrarVentas.Visible = true;
                            tpnlAdministrador.Visible = false;
                            tpnlReporteVentas.Visible = false;
                        }
                        else
                        {
                            Response.Redirect("MenuPrincipal.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }


        #region Buscar Cliente

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
            pnlRegistroVenta.Visible = true;
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
            catch (Exception)
            {

            }
            return save;
        }



        #endregion

        #region Registro Ventas
        protected void ddlTipoVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoVenta.SelectedValue != "*")
            {
                if (ddlTipoVenta.SelectedValue == "Categoria C")
                {
                    ddlPorcentaje.SelectedValue = "10";
                    CalcularValorGananciaComision();
                }
                else
                {
                    ddlPorcentaje.Enabled = true;
                    lblValorComision.Text = string.Empty;
                    CalcularValorGananciaComision();
                }
            }
            else
            {
                script = "alert('Seleccione un tipo de Venta');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
            }
        }

        public void CalcularValorGananciaComision()
        {
            decimal valorGanar;
            valorGanar = Math.Round((Convert.ToDecimal(ddlPorcentaje.SelectedValue) / 100), 2) * Convert.ToDecimal(txtValorVenta.Text);
            lblValorComision.Text = valorGanar.ToString();
        }

        protected void ddlPorcentaje_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcularValorGananciaComision();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            cotizacionN = new CotizacionN();
            if (!String.IsNullOrEmpty(txtFechaVentaEfectiva.Text))
            {
                if (!String.IsNullOrEmpty(txtDescripcionVenta.Text))
                {
                    if (!String.IsNullOrEmpty(txtValorVentaSinIva.Text))
                    {

                        if (ddlRegistroVenta.SelectedValue != "*")
                        {
                            if (ddlTipoVenta.SelectedValue != "*")
                            {
                                if (!String.IsNullOrEmpty(txtValorVenta.Text))
                                {
                                    var res = cotizacionN.InsertaRegistroVenta(Convert.ToDateTime(txtFechaVentaEfectiva.Text), ddlTipoCliente.SelectedValue,
                                            ddlRegistroVenta.SelectedValue, txtDocumento.Text, Convert.ToDecimal(txtValorVenta.Text), ddlTipoVenta.SelectedValue,
                                            Convert.ToInt32(ddlPorcentaje.SelectedValue), Convert.ToDecimal(lblValorComision.Text),
                                            Session["cod_usu"].ToString(),
                                             Session["rol"].ToString() == "1" ? ddlEjecutivos.SelectedValue : Session["identificacion"].ToString()
                                             , lBlCed.Text, txtDescripcionVenta.Text, Convert.ToDecimal(txtValorVentaSinIva.Text));

                                    if (res)
                                    {
                                        script = "alert('Se ha Registrado la Venta Correctamente con el numero de Documento: " + txtDocumento.Text + ".');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                        txtDocumento.Text = string.Empty;
                                        txtValorVenta.Text = string.Empty;
                                        ddlRegistroVenta.SelectedValue = "*";
                                        ddlTipoVenta.SelectedValue = "*";
                                        lblValorComision.Text = string.Empty;
                                        txtFechaVentaEfectiva.Text = string.Empty;
                                        ddlPorcentaje.Enabled = true;
                                        pnlRegistroVenta.Visible = false;
                                        pnlDatosEstudiante.Visible = false;
                                        txtCliente.Text = string.Empty;
                                        pnlClientes.Visible = false;
                                    }
                                    else
                                    {

                                        script = "alert('Ingrese el valor de la Venta.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                    }
                                }
                                else
                                {
                                    script = "alert('Ingrese el valor de la Venta.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                }
                            }
                            else
                            {
                                script = "alert('Seleccione un tipo de Venta.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            }
                        }
                        else
                        {
                            script = "alert('Seleccione un documento de registro de Venta.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            txtDocumento.Focus();
                        }
                    }
                    else
                    {
                        script = "alert('Ingrese el valor Total facturado sin IVA.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                        txtValorVentaSinIva.Focus();
                    }
                }
                else
                {
                    script = "alert('Ingrese la descripción de la Venta.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtDescripcionVenta.Focus();
                }
            }
            else
            {
                script = "alert('Escoja una fecha de Venta Efectiva.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
            }
        }


        private void CargarEmpleados()
        {
            empleadoN = new EmpleadoN();
            ddlEjecutivos.Items.Clear();
            var docentes = empleadoN.Empleados();
            foreach (var item in docentes)
            {
                ddlEjecutivos.Items.Add(new ListItem(item.Value, item.Key));
            }
        }

        private void CargarEmpleadosAdministrador()
        {
            empleadoN = new EmpleadoN();
            ddlEjecutivosAdministrador.Items.Clear();
            var items = empleadoN.Empleados();
            ddlEjecutivosAdministrador.Items.Add(new ListItem("Todos", "%"));
            foreach (var item in items)
            {
                ddlEjecutivosAdministrador.Items.Add(new ListItem(item.Value, item.Key));
            }
        }

        private void CargarEmpleadosreporte()
        {
            empleadoN = new EmpleadoN();
            ddlEjecutovosReporte.Items.Clear();
            var docentes = empleadoN.Empleados();
            foreach (var item in docentes)
            {
                ddlEjecutovosReporte.Items.Add(new ListItem(item.Value, item.Key));
            }
        }
        #endregion

        #region Administracion Ventas


        public void CargarVentas(string strEjecutivo, DateTime strFechaInicio, DateTime strFechaFin)
        {
            cotizacionN = new CotizacionN();

            var items = cotizacionN.ListadoClientes(ConfigurationManager.AppSettings["cnnSQL"],
                    strEjecutivo, strFechaInicio, strFechaFin);

            if (items.Count > 0)
            {
                dtgListadoVenta.DataSource = items;
                dtgListadoVenta.DataBind();
            }
            else
            {
                dtgListadoVenta.DataSource = null;
                dtgListadoVenta.DataBind();
            }

        }

        protected void ddlEjecutivosAdministrador_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVentas(ddlEjecutivosAdministrador.SelectedValue, Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text));
        }

        protected void txtFechaInicio_TextChanged(object sender, EventArgs e)
        {
            CargarVentas(ddlEjecutivosAdministrador.SelectedValue, Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text));
        }

        protected void txtFechaFin_TextChanged(object sender, EventArgs e)
        {
            CargarVentas(ddlEjecutivosAdministrador.SelectedValue, Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text));
        }
        #endregion

        #region Listado Ventas
        protected void btnVer_Click(object sender, ImageClickEventArgs e)
        {
            lblErrores.Visible = false;
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lblTituloV.Text = "ID VENTA: " + commandArgs[0].ToString();
            hddIDVENTA.Value = commandArgs[0].ToString();
            lbltitulo2.Text = commandArgs[1].ToString();
            lblTipoDocumento.Text = commandArgs[2].ToString();
            lblNoDocumento.Text = commandArgs[3].ToString();
            lblValorVenta.Text = commandArgs[4].ToString();
            txtPorcentaje.Text = commandArgs[5].ToString();
            txtPorcentaje.Enabled = false;
            lblValorComisionT.Text = commandArgs[6].ToString();
            lblTipoCliente.Text = commandArgs[7].ToString();
            lblTipoVenta.Text = commandArgs[8].ToString();
            lblFechaVentaEfectiva.Text = commandArgs[9].ToString();
            lblFechaRegistro.Text = commandArgs[10].ToString();
            btnAprobar.Visible = false;
            btnNegar.Visible = false;
            mdlPopup.Show();
        }

        protected void btnAprobar_Click(object sender, ImageClickEventArgs e)
        {
            lblErrores.Visible = false;
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lblTituloV.Text = "ID VENTA: " + commandArgs[0].ToString();
            hddIDVENTA.Value = commandArgs[0].ToString();
            lbltitulo2.Text = commandArgs[1].ToString();
            lblTipoDocumento.Text = commandArgs[2].ToString();
            lblNoDocumento.Text = commandArgs[3].ToString();
            lblValorVenta.Text = commandArgs[4].ToString();
            txtPorcentaje.Text = commandArgs[5].ToString();
            txtPorcentaje.Enabled = true;
            lblValorComisionT.Text = commandArgs[6].ToString();
            lblTipoCliente.Text = commandArgs[7].ToString();
            lblTipoVenta.Text = commandArgs[8].ToString();
            lblFechaVentaEfectiva.Text = commandArgs[9].ToString();
            lblFechaRegistro.Text = commandArgs[10].ToString();
            btnAprobar.Visible = true;
            btnNegar.Visible = false;
            mdlPopup.Show();
        }

        protected void btnAprobar_Click1(object sender, EventArgs e)
        {
            cotizacionN = new CotizacionN();
            decimal valorGanar;
            valorGanar = Math.Round((Convert.ToDecimal(txtPorcentaje.Text) / 100), 2) * 60;
            var res = cotizacionN.ActualizarEstadoVenta(hddIDVENTA.Value, Convert.ToInt32(txtPorcentaje.Text), valorGanar, "APROBADO");
            CargarVentas(ddlEjecutivosAdministrador.SelectedValue,
                               Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text));
        }

        protected void btnNegar_Click(object sender, EventArgs e)
        {
            cotizacionN = new CotizacionN();
            decimal valorGanar;
            valorGanar = Math.Round((Convert.ToDecimal(txtPorcentaje.Text) / 100), 2) * 60;
            var res = cotizacionN.ActualizarEstadoVenta(hddIDVENTA.Value, Convert.ToInt32(txtPorcentaje.Text), valorGanar, "NEGADO");
            CargarVentas(ddlEjecutivosAdministrador.SelectedValue,
                               Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text));
        }



        protected void btnRechazar_Click(object sender, ImageClickEventArgs e)
        {
            lblErrores.Visible = false;
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lblTituloV.Text = "ID VENTA: " + commandArgs[0].ToString();
            hddIDVENTA.Value = commandArgs[0].ToString();
            lbltitulo2.Text = commandArgs[1].ToString();
            lblTipoDocumento.Text = commandArgs[2].ToString();
            lblNoDocumento.Text = commandArgs[3].ToString();
            lblValorVenta.Text = commandArgs[4].ToString();
            txtPorcentaje.Text = commandArgs[5].ToString();
            txtPorcentaje.Enabled = true;
            lblValorComisionT.Text = commandArgs[6].ToString();
            lblTipoCliente.Text = commandArgs[7].ToString();
            lblTipoVenta.Text = commandArgs[8].ToString();
            lblFechaVentaEfectiva.Text = commandArgs[9].ToString();
            lblFechaRegistro.Text = commandArgs[10].ToString();
            btnAprobar.Visible = false;
            btnNegar.Visible = true;
            mdlPopup.Show();
        }

        decimal total;
        protected void dtgListadoVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    total += Convert.ToDecimal(e.Row.Cells[8].Text.Replace("$ ", "").Replace("$", ""));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[7].Text = "Total Valor Comisiones: ";
                    e.Row.Cells[8].Text = total.ToString("c");
                }
            }

            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Reporte

        public void CargarVentasReporte(string strEjecutivo, DateTime strFechaInicio, DateTime strFechaFin, string strEstado)
        {
            cotizacionN = new CotizacionN();

            var items = cotizacionN.ListadoVentasReporte(ConfigurationManager.AppSettings["cnnSQL"],
                    strEjecutivo, strFechaInicio, strFechaFin, strEstado);

            if (items.Count > 0)
            {
                dtgReporteVentas.DataSource = items;
                dtgReporteVentas.DataBind();
            }
            else
            {
                dtgReporteVentas.DataSource = null;
                dtgReporteVentas.DataBind();
            }

        }
        protected void txtFechaInicioReporte_TextChanged(object sender, EventArgs e)
        {
            CargarVentasReporte(ddlEjecutovosReporte.SelectedValue,
                             Convert.ToDateTime(txtFechaInicioReporte.Text), Convert.ToDateTime(txtFechaFinReporte.Text)
                             , ddlEstado.SelectedValue);
        }

        protected void txtFechaFinReporte_TextChanged(object sender, EventArgs e)
        {
            CargarVentasReporte(ddlEjecutovosReporte.SelectedValue,
                             Convert.ToDateTime(txtFechaInicioReporte.Text), Convert.ToDateTime(txtFechaFinReporte.Text)
                             , ddlEstado.SelectedValue);
        }

        protected void ddlEjecutovosReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVentasReporte(ddlEjecutovosReporte.SelectedValue,
                             Convert.ToDateTime(txtFechaInicioReporte.Text), Convert.ToDateTime(txtFechaFinReporte.Text)
                             , ddlEstado.SelectedValue);
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarVentasReporte(ddlEjecutovosReporte.SelectedValue,
                             Convert.ToDateTime(txtFechaInicioReporte.Text), Convert.ToDateTime(txtFechaFinReporte.Text)
                             , ddlEstado.SelectedValue);
        }

        decimal totalReporte;
        protected void dtgReporteVentas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    totalReporte += Convert.ToDecimal(e.Row.Cells[8].Text.Replace("$ ", "").Replace("$", ""));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[7].Text = "Total Valor Comisiones: ";
                    e.Row.Cells[8].Text = totalReporte.ToString("c");
                }
            }

            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Reporte en Excel

        protected void ExportToExcel()
        {
            try
            {
                string fileName = "ReporteVentas_" + ddlEstado.SelectedItem.Text + "+.xls";
                Session["fileName"] = fileName;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[13] { new DataColumn("idVenta", typeof(string)),
                                                         new DataColumn("NombreCompleto", typeof(string)),
                                                         new DataColumn("tipoCliente", typeof(string)),
                                                         new DataColumn("tipoDocumento", typeof(string)),
                                                         new DataColumn("nroDocumento", typeof(string)),
                                                         new DataColumn("valorVenta", typeof(string)),
                                                         new DataColumn("tipoVenta", typeof(string)),
                                                         new DataColumn("porcentaje", typeof(string)),
                                                         new DataColumn("valorComision", typeof(string)),
                                                         new DataColumn("estado", typeof(string)),
                                                         new DataColumn("FechaVentaEfectiva", typeof(string)),
                                                         new DataColumn("fechaRegistroVenta", typeof(string)),
                                                         new DataColumn("identificacionEjecutivo", typeof(string))});
                dt.Columns.Add();
                foreach (GridViewRow gvr in dtgReporteVentas.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["idVenta"] = gvr.Cells[0].Text;
                    dr["NombreCompleto"] = gvr.Cells[1].Text;
                    dr["tipoCliente"] = gvr.Cells[2].Text;
                    dr["tipoDocumento"] = gvr.Cells[3].Text;
                    dr["nroDocumento"] = gvr.Cells[4].Text;
                    dr["valorVenta"] = gvr.Cells[5].Text;
                    dr["tipoVenta"] = gvr.Cells[6].Text;
                    dr["porcentaje"] = gvr.Cells[7].Text;
                    dr["valorComision"] = gvr.Cells[8].Text;
                    dr["estado"] = gvr.Cells[9].Text;
                    dr["FechaVentaEfectiva"] = gvr.Cells[10].Text;
                    dr["fechaRegistroVenta"] = gvr.Cells[11].Text;
                    dr["identificacionEjecutivo"] = gvr.Cells[12].Text;
                    dt.Rows.Add(dr);
                }

                dt.Columns.RemoveAt(13);
                System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
                dg.AllowPaging = false;
                dg.DataSource = dt;
                dg.DataBind();
                Response.Clear();
                Response.Write(@"<style> TD { mso-number-format:\@; } </style>");
                Response.Buffer = true;
                Response.Charset = "UTF-8";
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Session["fileName"].ToString());
                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
                dg.RenderControl(htmlTextWriter);
                Response.Write(stringWriter.ToString());
                Response.End();
            }

            catch (Exception ex)
            {

            }
        }

        protected void btnGenerarListado_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        #endregion
    }
}