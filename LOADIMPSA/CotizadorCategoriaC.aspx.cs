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
    public partial class CotizadorCategoriaC : System.Web.UI.Page
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

                    parametroNacionalizacion = parametrosCorporativosN.BuscaParametrosCorporativos("NACIONALIZACION");
                    foreach (var items in parametroNacionalizacion)
                    {
                        lblNacionalizacion.Text = items.valorint.ToString();
                    }


                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        if (item.rol == 1)
                        {
                            Session["registro"] = item.identificacion;
                            Session["rol"] = item.rol;
                        }
                        else if (item.rol == 4 || item.rol == 9)
                        {
                            Session["registro"] = item.identificacion;
                            Session["rol"] = item.rol;
                        }
                        else if (item.rol == 2 || item.rol == 6 )
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

      

        protected void btnRegistroCheckOut_ClickFile(object sender, ImageClickEventArgs e)
        {
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            Session["strCliente"] = commandArgs[0].ToString();
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
        public void LimpiarCajas()
        {
            txtCliente.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtIDE.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtDescripcionMercaderia.Text = string.Empty;

            txtFactura.Text = string.Empty;
            txtPeso.Text = string.Empty;
           
            pnlClientes.Visible = false;
            pnlDatosCliente.Visible = false;
            pnlCotizador.Visible = false;

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
        protected void txtPeso_TextChanged(object sender, EventArgs e)
        {
            ParametrosCorporativosN parametrosCorporativosN = new ParametrosCorporativosN();
            if (!String.IsNullOrEmpty(txtPeso.Text))
            {
                if (Convert.ToDouble(txtPeso.Text) > 220)
                {
                    script = "alert('El peso en Categoría C no puede exceder los 100 Kilos, validar como consumo.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtPeso.Text = string.Empty;
                    txtPeso.Focus();
                }
                else
                {
                    List<ParametrosCorporativosE> parametroTazaFlete = new List<ParametrosCorporativosE>();
                    parametroTazaFlete = parametrosCorporativosN.BuscaParametrosCorporativos("TAZAFLETEINTERNACIONAL");
                    double taza = 0;
                    foreach (var items in parametroTazaFlete)
                    {
                        taza = Convert.ToDouble(items.valordecimal);
                    }

                    // Calculo de Flete Internacional

                    double fleteInternacional = Math.Round((Convert.ToDouble(txtPeso.Text) * taza), 2);
                    lblFleteInternacional.Text = fleteInternacional.ToString();

                    List<ParametrosCorporativosE> parametroDomicilio = new List<ParametrosCorporativosE>();
                    parametroDomicilio = parametrosCorporativosN.BuscaParametrosCorporativos("VALORDOMICILIO");
                    decimal pagoServicioDomicilio = 0;
                    foreach (var items in parametroDomicilio)
                    {
                        pagoServicioDomicilio = Convert.ToDecimal(items.valordecimal);
                    }

                    if (Session["rol"].ToString() == "5")
                    {
                        List<ParametrosCorporativosE> parametroDomicilioVip = new List<ParametrosCorporativosE>();
                        parametroDomicilioVip = parametrosCorporativosN.BuscaParametrosCorporativos("VALORDOMICILIOVIP");
                        foreach (var items in parametroDomicilioVip)
                        {
                            pagoServicioDomicilio = Convert.ToDecimal(items.valordecimal);
                        }
                    }

                    // Costo por Libra
                    List<ParametrosCorporativosE> parametroValorLibra = new List<ParametrosCorporativosE>();
                    parametroValorLibra = parametrosCorporativosN.BuscaParametrosCorporativos("COSTOLIBRA");
                    decimal valorLibra = 0;
                    foreach (var items in parametroValorLibra)
                    {
                        valorLibra = Convert.ToDecimal(items.valordecimal);
                    }

                    if (Session["rol"].ToString() == "5")
                    {
                        List<ParametrosCorporativosE> parametroValorLibraVip = new List<ParametrosCorporativosE>();
                        parametroValorLibraVip = parametrosCorporativosN.BuscaParametrosCorporativos("COSTOLIBREVIP");
                        foreach (var items in parametroValorLibraVip)
                        {
                            valorLibra = Convert.ToDecimal(items.valordecimal);
                        }
                    }

                    // Calculo de Envio a Domicilio
                    if (Math.Round(Convert.ToDouble(txtPeso.Text), 2) > 2.2)
                    {
                        decimal librasextra = Convert.ToDecimal(txtPeso.Text) - Convert.ToDecimal(2.2);
                        decimal pagoextraEnvio = librasextra * Convert.ToDecimal(valorLibra);
                        pagoServicioDomicilio = Math.Round(Convert.ToDecimal(5 + pagoextraEnvio), 2);
                       // lblServicioDomicilio.Text = pagoServicioDomicilio.ToString();
                    }
                    else
                    {
                       // lblServicioDomicilio.Text = pagoServicioDomicilio.ToString();
                    }
                    //+ Convert.ToDecimal(pagoServicioDomicilio)
                    decimal sumatoriaTotalCourier = (Convert.ToDecimal(lblNacionalizacion.Text) +
                        Convert.ToDecimal(fleteInternacional) );

                    lblTotalServicioCorrier.Text = sumatoriaTotalCourier.ToString();


                    // Calculo de Base Imponible
                    decimal baseImponible;
                    decimal seguro;
                    double porcentajeUno = 0.01;
                    if (!String.IsNullOrEmpty(txtFactura.Text))
                    {
                        seguro = (decimal)Math.Round((Convert.ToDouble(txtFactura.Text) + Convert.ToDouble(fleteInternacional)) * porcentajeUno, 2);
                        baseImponible = (decimal)Math.Round((Convert.ToDecimal(txtFactura.Text) + Convert.ToDecimal(fleteInternacional) + seguro), 2);
                        lblBaseImponible.Text = baseImponible.ToString();



                        // Calculo delArancel
                        if (!String.IsNullOrEmpty(txtPorcentajeOperacion.Text))
                        {
                            decimal arancel = Math.Round(baseImponible * (Convert.ToDecimal(txtPorcentajeOperacion.Text) / 100), 2);
                            lblArancel.Text = arancel.ToString();
                            decimal tazafonfimfa = 0;
                            List<ParametrosCorporativosE> parametroFOd = new List<ParametrosCorporativosE>();
                            parametroFOd = parametrosCorporativosN.BuscaParametrosCorporativos("TAZAFODIMFA");
                            foreach (var items in parametroFOd)
                            {
                                tazafonfimfa = Convert.ToDecimal(items.valordecimal);
                            }
                            decimal fondinfa = Math.Round(Convert.ToDecimal(lblBaseImponible.Text) * Convert.ToDecimal(tazafonfimfa), 2);
                            lblFondimfa.Text = fondinfa.ToString();

                            decimal calculoIva = baseImponible + arancel + fondinfa;
                            decimal tazaIva = 0;
                            List<ParametrosCorporativosE> parametroIVA = new List<ParametrosCorporativosE>();
                            parametroIVA = parametrosCorporativosN.BuscaParametrosCorporativos("IVA");
                            foreach (var items in parametroIVA)
                            {
                                tazaIva = Convert.ToDecimal(items.valordecimal);
                            }

                            decimal iva = Math.Round(Convert.ToDecimal(calculoIva) * Convert.ToDecimal(tazaIva), 2);
                            lblIVA.Text = iva.ToString();

                            // Sumatoria Impuestos SENAE

                            decimal sumatoriaImpuestos;

                            sumatoriaImpuestos = arancel + fondinfa + iva;
                            lblTotalImpuestosSenae.Text = sumatoriaImpuestos.ToString();

                            // Sumatoria Total Importacion

                            decimal sumatoriaTotalImportacion;
                            sumatoriaTotalImportacion = sumatoriaTotalCourier + sumatoriaImpuestos;
                            lblSumatoria.Text = sumatoriaTotalImportacion.ToString();
                        }
                    }
                }
            }






        }

        protected void txtPorcentajeOperacion_TextChanged(object sender, EventArgs e)
        {
            //calculo Arancel
            //calculo FODIMFA
            ParametrosCorporativosN parametrosCorporativosN = new ParametrosCorporativosN();
            if (!String.IsNullOrEmpty(txtPorcentajeOperacion.Text))
            {
                if (!String.IsNullOrEmpty(lblBaseImponible.Text))
                {

                    decimal arancel = Math.Round(Convert.ToDecimal(lblBaseImponible.Text) * (Convert.ToDecimal(txtPorcentajeOperacion.Text) / 100), 2);
                    lblArancel.Text = arancel.ToString();
                    decimal tazafonfimfa = 0;
                    List<ParametrosCorporativosE> parametroFOd = new List<ParametrosCorporativosE>();
                    parametroFOd = parametrosCorporativosN.BuscaParametrosCorporativos("TAZAFODIMFA");
                    foreach (var items in parametroFOd)
                    {
                        tazafonfimfa = Convert.ToDecimal(items.valordecimal);
                    }
                    decimal fondinfa = Math.Round(Convert.ToDecimal(lblBaseImponible.Text) * Convert.ToDecimal(tazafonfimfa), 2);
                    lblFondimfa.Text = fondinfa.ToString();

                    decimal calculoIva = Convert.ToDecimal(lblBaseImponible.Text) + arancel + fondinfa;
                    decimal tazaIva = 0;
                    List<ParametrosCorporativosE> parametroIVA = new List<ParametrosCorporativosE>();
                    parametroIVA = parametrosCorporativosN.BuscaParametrosCorporativos("IVA");
                    foreach (var items in parametroIVA)
                    {
                        tazaIva = Convert.ToDecimal(items.valordecimal);
                    }
                    decimal iva = Math.Round(Convert.ToDecimal(calculoIva) * Convert.ToDecimal(tazaIva), 2);
                    lblIVA.Text = iva.ToString();

                    decimal sumatoriaImpuestos;
                    sumatoriaImpuestos = arancel + fondinfa + iva;
                    lblTotalImpuestosSenae.Text = sumatoriaImpuestos.ToString();

                    // Sumatoria Total Importacion
                    decimal sumatoriaTotalImportacion;
                    sumatoriaTotalImportacion = Convert.ToDecimal(lblTotalServicioCorrier.Text) + sumatoriaImpuestos;
                    lblSumatoria.Text = sumatoriaTotalImportacion.ToString();

                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (ddlstrCliente.SelectedValue == "SI")
            {

                CotizacionN cotizacionN = new CotizacionN();

                var respuesta = cotizacionN.InsertaCotizacionB("","",lBlCed.Text, "C", txtDescripcionMercaderia.Text, Convert.ToDecimal(txtFactura.Text),
                    Convert.ToDecimal(txtPeso.Text), Convert.ToDecimal(lblTotalServicioCorrier.Text),
                    Convert.ToDecimal(lblSumatoria.Text), Session["registro"].ToString(), Convert.ToDecimal(lblNacionalizacion.Text),
                    Convert.ToDecimal(lblFleteInternacional.Text), Convert.ToDecimal(lblTotalImpuestosSenae.Text),
                    Convert.ToDecimal(4));

                if (respuesta.id_codigoContizacion > 0)
                {
                    script = "alert('La cotización del Cliente: " + lblNombresCom.Text + " se ha registrado correctamente.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    Session["strCliente"] = respuesta.id_codigoContizacion;
                    string s = "window.open('ReporteC.aspx', '_new_tab');";

                    ScriptManager.RegisterStartupScript(uppListadoTracking, uppListadoTracking.GetType(), "script", s, true);
                }
                else
                {
                    script = "alert('Ocurrio un error comuniquese con el administrador.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                }
            }
            else if (ddlstrCliente.SelectedValue == "NO")
            {
                if (!String.IsNullOrEmpty(txtNombres.Text))

                {
                    if (!String.IsNullOrEmpty(txtIDE.Text))
                    {
                        if (!String.IsNullOrEmpty(txtCorreo.Text))
                        {
                            CotizacionN cotizacionN = new CotizacionN();

                            var respuesta = cotizacionN.InsertaCotizacionB(txtNombres.Text,txtCorreo.Text,txtIDE.Text, "C", txtDescripcionMercaderia.Text, Convert.ToDecimal(txtFactura.Text),
                                Convert.ToDecimal(txtPeso.Text), Convert.ToDecimal(lblTotalServicioCorrier.Text),
                                Convert.ToDecimal(lblSumatoria.Text), Session["registro"].ToString(), Convert.ToDecimal(lblNacionalizacion.Text),
                                Convert.ToDecimal(lblFleteInternacional.Text), Convert.ToDecimal(lblTotalImpuestosSenae.Text),
                                Convert.ToDecimal(4));

                            if (respuesta.id_codigoContizacion > 0)
                            {
                                script = "alert('La cotización del Cliente: " + lblNombresCom.Text + " se ha registrado correctamente.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                Session["strCliente"] = respuesta.id_codigoContizacion;
                                string s = "window.open('ReporteC.aspx', '_new_tab');";

                                ScriptManager.RegisterStartupScript(uppListadoTracking, uppListadoTracking.GetType(), "script", s, true);
                            }
                            else
                            {
                                script = "alert('Ocurrio un error comuniquese con el administrador.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            }
                        }
                        else
                        {
                            script = "alert('El correo  es obligatorio.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            txtCorreo.Focus();

                        }

                    }
                    else
                    {
                        script = "alert('La identificacion  es obligatoria.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                        txtIDE.Focus();

                    }
                }
                else
                {
                    script = "alert('El nombre  es obligatorio.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtNombres.Focus();

                }
            }
            }
        protected void txtFactura_TextChanged(object sender, EventArgs e)
        {
            ParametrosCorporativosN parametrosCorporativosN = new ParametrosCorporativosN();
            if (!String.IsNullOrEmpty(txtFactura.Text)  && !String.IsNullOrEmpty(txtPeso.Text))
            {
                if (Convert.ToDouble(txtFactura.Text) > 5000)
                {
                    script = "alert('La Categoría C no puede exceder los $ 5000 FOB, validar como consumo.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtFactura.Text = string.Empty;
                    txtFactura.Focus();
                }
                else
                {
                    List<ParametrosCorporativosE> parametroTazaFlete = new List<ParametrosCorporativosE>();
                    parametroTazaFlete = parametrosCorporativosN.BuscaParametrosCorporativos("TAZAFLETEINTERNACIONAL");
                    double taza = 0;
                    foreach (var items in parametroTazaFlete)
                    {
                        taza = Convert.ToDouble(items.valordecimal);
                    }

                    // Calculo de Flete Internacional

                    double fleteInternacional = Math.Round((Convert.ToDouble(txtPeso.Text) * taza), 2);
                    lblFleteInternacional.Text = fleteInternacional.ToString();

                    List<ParametrosCorporativosE> parametroDomicilio = new List<ParametrosCorporativosE>();
                    parametroDomicilio = parametrosCorporativosN.BuscaParametrosCorporativos("VALORDOMICILIO");
                    decimal pagoServicioDomicilio = 0;
                    foreach (var items in parametroDomicilio)
                    {
                        pagoServicioDomicilio = Convert.ToDecimal(items.valordecimal);
                    }

                    if (Session["rol"].ToString() == "5")
                    {
                        List<ParametrosCorporativosE> parametroDomicilioVip = new List<ParametrosCorporativosE>();
                        parametroDomicilioVip = parametrosCorporativosN.BuscaParametrosCorporativos("VALORDOMICILIOVIP");
                        foreach (var items in parametroDomicilioVip)
                        {
                            pagoServicioDomicilio = Convert.ToDecimal(items.valordecimal);
                        }
                    }


                    // Costo por Libra
                    List<ParametrosCorporativosE> parametroValorLibra = new List<ParametrosCorporativosE>();
                    parametroValorLibra = parametrosCorporativosN.BuscaParametrosCorporativos("COSTOLIBRA");
                    decimal valorLibra = 0;
                    foreach (var items in parametroValorLibra)
                    {
                        valorLibra = Convert.ToDecimal(items.valordecimal);
                    }

                    if (Session["rol"].ToString() == "5")
                    {
                        List<ParametrosCorporativosE> parametroValorLibraVip = new List<ParametrosCorporativosE>();
                        parametroValorLibraVip = parametrosCorporativosN.BuscaParametrosCorporativos("COSTOLIBREVIP");
                        foreach (var items in parametroValorLibraVip)
                        {
                            valorLibra = Convert.ToDecimal(items.valordecimal);
                        }
                    }



                    // Calculo de Envio a Domicilio
                    if (Math.Round(Convert.ToDouble(txtPeso.Text), 2) > 2.2)
                    {
                        decimal librasextra = Convert.ToDecimal(txtPeso.Text) - Convert.ToDecimal(2.2);
                        decimal pagoextraEnvio = librasextra * Convert.ToDecimal(valorLibra);
                        pagoServicioDomicilio = Math.Round(Convert.ToDecimal(5 + pagoextraEnvio), 2);
                       // lblServicioDomicilio.Text = pagoServicioDomicilio.ToString();
                    }
                    else
                    {
                       // lblServicioDomicilio.Text = pagoServicioDomicilio.ToString();
                    }

                    decimal sumatoriaTotalCourier = (Convert.ToDecimal(lblNacionalizacion.Text) +
                        Convert.ToDecimal(fleteInternacional) + Convert.ToDecimal(pagoServicioDomicilio));

                    lblTotalServicioCorrier.Text = sumatoriaTotalCourier.ToString();


                    // Calculo de Base Imponible
                    decimal baseImponible;
                    decimal seguro;
                    double porcentajeUno = 0.01;
                    if (!String.IsNullOrEmpty(txtFactura.Text) || !String.IsNullOrEmpty(txtPeso.Text))
                    {
                        seguro = (decimal)Math.Round((Convert.ToDouble(txtFactura.Text) + Convert.ToDouble(fleteInternacional)) * porcentajeUno, 2);
                        baseImponible = (decimal)Math.Round((Convert.ToDecimal(txtFactura.Text) + Convert.ToDecimal(fleteInternacional) + seguro), 2);
                        lblBaseImponible.Text = baseImponible.ToString();



                        // Calculo delArancel
                        if (!String.IsNullOrEmpty(txtPorcentajeOperacion.Text))
                        {
                            decimal arancel = Math.Round(baseImponible * (Convert.ToDecimal(txtPorcentajeOperacion.Text) / 100), 2);
                            lblArancel.Text = arancel.ToString();
                            decimal tazafonfimfa = 0;
                            List<ParametrosCorporativosE> parametroFOd = new List<ParametrosCorporativosE>();
                            parametroFOd = parametrosCorporativosN.BuscaParametrosCorporativos("TAZAFODIMFA");
                            foreach (var items in parametroFOd)
                            {
                                tazafonfimfa = Convert.ToDecimal(items.valordecimal);
                            }
                            decimal fondinfa = Math.Round(Convert.ToDecimal(lblBaseImponible.Text) * Convert.ToDecimal(tazafonfimfa), 2);
                            lblFondimfa.Text = fondinfa.ToString();

                            decimal calculoIva = baseImponible + arancel + fondinfa;
                            decimal tazaIva = 0;
                            List<ParametrosCorporativosE> parametroIVA = new List<ParametrosCorporativosE>();
                            parametroIVA = parametrosCorporativosN.BuscaParametrosCorporativos("IVA");
                            foreach (var items in parametroIVA)
                            {
                                tazaIva = Convert.ToDecimal(items.valordecimal);
                            }

                            decimal iva = Math.Round(Convert.ToDecimal(calculoIva) * Convert.ToDecimal(tazaIva), 2);
                            lblIVA.Text = iva.ToString();

                            // Sumatoria Impuestos SENAE

                            decimal sumatoriaImpuestos;

                            sumatoriaImpuestos = arancel + fondinfa + iva;
                            lblTotalImpuestosSenae.Text = sumatoriaImpuestos.ToString();

                            // Sumatoria Total Importacion

                            decimal sumatoriaTotalImportacion;
                            sumatoriaTotalImportacion = sumatoriaTotalCourier + sumatoriaImpuestos;
                            lblSumatoria.Text = sumatoriaTotalImportacion.ToString();
                        }
                    }
                }
            }

        }
    }

}