using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
using System.Configuration;

namespace LOADIMPSA
{
    public partial class CotizacionLcl : System.Web.UI.Page
    {
        ClienteN clienteN;
        TransportistasN transportistasN;
        EmpleadoN empleadoN;
        TrackingsN trackingsN;
        ParametrosCorporativosN parametrosCorporativosN;
        string script;


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
                if (Session["usuario"] != null)
                {
                    ParametrosCorporativosN parametrosCorporativosN = new ParametrosCorporativosN();
                    List<ParametrosCorporativosE> parametroNacionalizacion = new List<ParametrosCorporativosE>();

                    parametroNacionalizacion = parametrosCorporativosN.BuscaParametrosCorporativos("GASTOSPOT");
                    foreach (var items in parametroNacionalizacion)
                    {
                        lblgaSpot.Text = Convert.ToString(Math.Round(Convert.ToDouble(items.valordecimal.ToString()), 2));

                        ParametrosCorporativosN parametrosCorporativosFee = new ParametrosCorporativosN();
                        List<ParametrosCorporativosE> parametroClfee = new List<ParametrosCorporativosE>();
                        parametroClfee = parametrosCorporativosFee.BuscaParametrosCorporativos("CLFEE");

                        foreach (var items2 in parametroClfee)
                        {
                            hddFee.Value = items2.valorint.ToString();
                        }


                        List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                        foreach (var item in usuE)
                        {
                            if (item.rol == 1)
                            {
                                Session["registro"] = item.identificacion;
                                Session["rol"] = item.rol;
                            }
                            else if (item.rol == 4)
                            {
                                Session["registro"] = item.identificacion;
                                Session["rol"] = item.rol;
                            }
                            else if (item.rol == 2 || item.rol == 6)
                            {
                                Session["registro"] = item.identificacion;
                                Session["rol"] = item.rol;
                            }
                            else if (item.rol == 7)
                            {
                                pnlBusquedaClientes.Visible = false;
                                DatosClientes(item.identificacion);
                                pnlDatosCliente.Visible = true;
                                pnlCotizador.Visible = true;
                                Session["registro"] = item.identificacion;
                                Session["rol"] = item.rol;
                            }
                            else
                            {
                                Response.Redirect("MenuPrincipal.aspx");
                            }
                            ddlCliente_SelectedIndexChanged(null, null);
                            PanelIngreso.Visible = false;

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
                    Session["rol"] = datos.id_rol.ToString();


                }
            }
            catch (Exception ex)
            {

            }
            return save;
        }
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlstrCliente.SelectedValue == "SI")
            {
                panelCompletoC.Visible = true;
                pnlBusquedaClientes.Visible = true;
                panelCompletoNC.Visible = false;
                pnlCotizador.Visible = false;
                pnlClientes.Visible = false;
                pnlDatosCliente.Visible = false;

            }
            else if (ddlstrCliente.SelectedValue == "NO")
            {
                panelCompletoC.Visible = false;
                pnlBusquedaClientes.Visible = false;
                panelCompletoNC.Visible = true;
                pnlCotizador.Visible = true;
                txtCliente.Text = string.Empty;
                pnlClientes.Visible = false;
            }


        }
        protected void txtFlete_TextChanged(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(txtFleteInter.Text))
            {

                // Calculo de Flete Collect FEE

                double collFee = Math.Round((Convert.ToDouble(txtFleteInter.Text) * (Convert.ToDouble(hddFee.Value) / 100)), 2);
                lblcolFee.Text = collFee.ToString();


                decimal tazaIva = 0;

                ParametrosCorporativosN parametrosCorporativosN = new ParametrosCorporativosN();
                List<ParametrosCorporativosE> parametroIVA = new List<ParametrosCorporativosE>();
                parametroIVA = parametrosCorporativosN.BuscaParametrosCorporativos("IVA");
                foreach (var items in parametroIVA)
                {
                    tazaIva = Convert.ToDecimal(items.valordecimal);
                }

                decimal iva = Math.Round(Convert.ToDecimal(Convert.ToDecimal(lblcolFee.Text) + Convert.ToDecimal(lblgaSpot.Text)) * Convert.ToDecimal(tazaIva), 2);
                lblIva.Text = iva.ToString();

                // Sumatoria Impuestos SENAE

                double sumatoriaImpuestos;

                sumatoriaImpuestos = Convert.ToDouble(lblgaSpot.Text) + Convert.ToDouble(lblcolFee.Text) + Convert.ToDouble(iva);

                lblTotalGastos.Text = sumatoriaImpuestos.ToString();

                // Sumatoria Total Importacion

                double sumatoriaTotalImportacion;
                sumatoriaTotalImportacion = Convert.ToDouble(txtFleteInter.Text) + sumatoriaImpuestos;
                lblTotalCotiza.Text = sumatoriaTotalImportacion.ToString();

            }
        }
        public void LimpiarCajas()
        {
            txtNombres.Text = string.Empty;
            txtIDE.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtImport.Text = string.Empty;
            txtNombres.Text = string.Empty;

            txtDias.Text = string.Empty;
            txtFechaExp.Text = string.Empty;
            txtDescripcionMercaderia.Text = string.Empty;

            txtPuertoO.Text = string.Empty;
            txtPuertoD.Text = string.Empty;
            txtMc.Text = string.Empty;

            txtPeso.Text = string.Empty;
            txtFleteInter.Text = string.Empty;
            lblgaSpot.Text = string.Empty;
            lblcolFee.Text = string.Empty;
            lblIva.Text = string.Empty;
            lblTotalGastos.Text = string.Empty;
            lblTotalCotiza.Text = string.Empty;
            txtCliente.Text = string.Empty;
            pnlClientes.Visible = false;
            pnlDatosCliente.Visible = false;
            pnlCotizador.Visible = false;

        }

        public void CargarListado(DateTime? strFechaInicio, DateTime? strFechaFin, string strUsuario)
        {
            try
            {

                var datos = new ReportesN().ConsultaCotizacionLcl(ConfigurationManager.AppSettings["cnnSQL"], strFechaInicio, strFechaFin, strUsuario);
                dtgEnvios.DataSource = datos;
                dtgEnvios.DataBind();
            }
            catch (Exception ex)
            {

            }

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

        protected void btnRegistroCheckOut_ClickFile(object sender, ImageClickEventArgs e)
        {
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            Session["strCliente"] = commandArgs[0].ToString();
            string s = "window.open('ReporteLcl.aspx', '_new_tab');";
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

        protected void btnBuscar1_Click(object sender, EventArgs e)
        {
            DateTime? datFechaIngreso;
            DateTime? datFechaRecibidoMiami;

            if (String.IsNullOrEmpty(txtFechaIngreso.Text) || String.IsNullOrEmpty(txtFechaRecbidoMiami.Text))
            {
                script = "alert('Se debe ingresar las fechas de Inicio y Fecha Fin.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
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
        protected void radio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //do whatever you want by calling the name of the radio id
            //example

            if (panelId.SelectedItem.Value == "0")
            {

                PanelIngreso.Visible = true;
                PanelConsulta.Visible = false;
                panelCompletoNC.Visible = false;


            }
            else if (panelId.SelectedItem.Value == "1")
            {
                LimpiarCajas();
                panelCompletoNC.Visible = false;

                PanelIngreso.Visible = false;
                PanelConsulta.Visible = true;
                pnlFiltros.Visible = true;



            }

        }


        protected void btnRegistrar_Click(object sender, EventArgs e)
        {







            if (ddlstrCliente.SelectedValue == "SI")
            {
                if (!String.IsNullOrEmpty(txtImport.Text))

                {
                    if (!String.IsNullOrEmpty(txtDias.Text))
                    {
                        if (!String.IsNullOrEmpty(txtFechaExp.Text))
                        {
                            if (!String.IsNullOrEmpty(txtPeso.Text))
                            {
                                if (!String.IsNullOrEmpty(txtPuertoD.Text))
                                {
                                    if (!String.IsNullOrEmpty(txtPuertoO.Text))
                                    {
                                        if (!String.IsNullOrEmpty(txtMc.Text))
                                        {
                                            if (!String.IsNullOrEmpty(txtPeso.Text))
                                            {
                                                if (!String.IsNullOrEmpty(txtDescripcionMercaderia.Text))
                                                {
                                                    if (!String.IsNullOrEmpty(txtFleteInter.Text))
                                                    {
                                                        if (!String.IsNullOrEmpty(txtDescripcion.Text))
                                                        {
                                                            CotizacionN cotizacionN = new CotizacionN();

                                                            var respuesta = cotizacionN.InsertaCotizacionClc("", "", lBlCed.Text, "A", txtDescripcionMercaderia.Text, txtImport.Text,
                                                                Convert.ToDecimal(txtPeso.Text), txtPuertoO.Text, txtPuertoD.Text,
                                                                 Session["registro"].ToString(), Convert.ToInt32(txtDias.Text),
                                                                Convert.ToDecimal(txtMc.Text), Convert.ToDecimal(txtFleteInter.Text),
                                                                Convert.ToDecimal(lblgaSpot.Text), Convert.ToDecimal(lblcolFee.Text), Convert.ToDecimal(lblIva.Text), Convert.ToDecimal(lblTotalGastos.Text), Convert.ToDecimal(lblTotalCotiza.Text), Convert.ToDateTime(txtFechaExp.Text), txtDescripcion.Text);

                                                            if (respuesta.id_codigoContizacion > 0)
                                                            {
                                                                script = "alert('La cotización del Cliente: " + lBlCed.Text + " se ha registrado correctamente.');";
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                                Session["strCliente"] = respuesta.id_codigoContizacion;
                                                                string s = "window.open('ReporteLcl.aspx', '_new_tab');";

                                                                ScriptManager.RegisterStartupScript(uppListadoCotiza, uppListadoCotiza.GetType(), "script", s, true);
                                                            }
                                                            else
                                                            {
                                                                script = "alert('Ocurrio un error comuniquese con el administrador.');";
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            script = "alert('La observacion  es obligatoria.');";
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                            txtDescripcion.Focus();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        script = "alert('El flete Internacional es obligatoria.');";
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                        txtFleteInter.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                    script = "alert('La descripción del Envio es obligatoria.');";
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                    txtDescripcionMercaderia.Focus();
                                                }
                                            }
                                            else
                                            {
                                                script = "alert('El peso es obligatorio.');";
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                txtPeso.Focus();
                                            }
                                        }
                                        else
                                        {

                                            script = "alert('Los metros Cubicos es obligatorio');";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                            txtMc.Focus();
                                        }
                                    }
                                    else
                                    {

                                        script = "alert('El puerto Origen es obligatorio.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                        txtPuertoO.Focus();
                                    }

                                }
                                else
                                {
                                    script = "alert('El puerto destino es obligatorio.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                    txtPuertoO.Focus();
                                }
                            }
                            else
                            {
                                script = "alert('El peso es obligatorio');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                txtPeso.Focus();
                            }
                        }
                        else
                        {
                            script = "alert('Fecha de Expiración es obligatorio.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            txtFechaExp.Focus();

                        }

                    }
                    else
                    {
                        script = "alert('El No Dias  es obligatorio.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                        txtDias.Focus();

                    }
                }
                else
                {
                    script = "alert('El importador  es obligatorio.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtImport.Focus();

                }
            }
            else if (ddlstrCliente.SelectedValue == "NO")
            {
                if (!String.IsNullOrEmpty(txtImport.Text))

                {
                    if (!String.IsNullOrEmpty(txtDias.Text))
                    {
                        if (!String.IsNullOrEmpty(txtFechaExp.Text))
                        {
                            if (!String.IsNullOrEmpty(txtPeso.Text))
                            {
                                if (!String.IsNullOrEmpty(txtPuertoD.Text))
                                {
                                    if (!String.IsNullOrEmpty(txtPuertoO.Text))
                                    {
                                        if (!String.IsNullOrEmpty(txtMc.Text))
                                        {
                                            if (!String.IsNullOrEmpty(txtPeso.Text))
                                            {
                                                if (!String.IsNullOrEmpty(txtDescripcionMercaderia.Text))
                                                {
                                                    if (!String.IsNullOrEmpty(txtFleteInter.Text))
                                                    {
                                                        if (!String.IsNullOrEmpty(txtDescripcion.Text))
                                                        {

                                                            CotizacionN cotizacionN = new CotizacionN();

                                                            var respuesta = cotizacionN.InsertaCotizacionClc(txtNombres.Text, txtCorreo.Text, txtIDE.Text, "A", txtDescripcionMercaderia.Text, txtImport.Text,
                                                               Convert.ToDecimal(txtPeso.Text), txtPuertoO.Text, txtPuertoD.Text,
                                                                Session["registro"].ToString(), Convert.ToInt32(txtDias.Text),
                                                               Convert.ToDecimal(txtMc.Text), Convert.ToDecimal(txtFleteInter.Text),
                                                               Convert.ToDecimal(lblgaSpot.Text), Convert.ToDecimal(lblcolFee.Text), Convert.ToDecimal(lblIva.Text), Convert.ToDecimal(lblTotalGastos.Text), Convert.ToDecimal(lblTotalCotiza.Text), Convert.ToDateTime(txtFechaExp.Text), txtDescripcion.Text);


                                                            if (respuesta.id_codigoContizacion > 0)
                                                            {
                                                                script = "alert('La cotización del Cliente: " + lblNombresCom.Text + " se ha registrado correctamente.');";
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                                Session["strCliente"] = respuesta.id_codigoContizacion;
                                                                string s = "window.open('ReporteLcl.aspx', '_new_tab');";

                                                                ScriptManager.RegisterStartupScript(uppListadoCotiza, uppListadoCotiza.GetType(), "script", s, true);
                                                            }
                                                            else
                                                            {
                                                                script = "alert('Ocurrio un error comuniquese con el administrador.');";
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            script = "alert('La observacion  es obligatoria.');";
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                            txtDescripcion.Focus();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        script = "alert('El flete Internacional es obligatoria.');";
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                        txtFleteInter.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                    script = "alert('La descripción del Envio es obligatoria.');";
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                    txtDescripcionMercaderia.Focus();
                                                }
                                            }
                                            else
                                            {
                                                script = "alert('El peso es obligatorio.');";
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                                txtPeso.Focus();
                                            }
                                        }
                                        else
                                        {

                                            script = "alert('Los metros Cubicos es obligatorio');";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                            txtMc.Focus();
                                        }
                                    }
                                    else
                                    {

                                        script = "alert('El puerto Origen es obligatorio.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                        txtPuertoO.Focus();
                                    }

                                }
                                else
                                {
                                    script = "alert('El puerto destino es obligatorio.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                    txtPuertoO.Focus();
                                }
                            }
                            else
                            {
                                script = "alert('El peso es obligatorio');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                txtPeso.Focus();
                            }
                        }
                        else
                        {
                            script = "alert('Fecha de Expiración es obligatorio.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            txtFechaExp.Focus();

                        }

                    }
                    else
                    {
                        script = "alert('El No Dias  es obligatorio.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                        txtDias.Focus();

                    }
                }
                else
                {
                    script = "alert('El importador  es obligatorio.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtImport.Focus();

                }
            }



        }


    }
}