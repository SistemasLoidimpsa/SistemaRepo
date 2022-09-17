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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LOADIMPSA
{
    public partial class BodegaLoisimpsa : System.Web.UI.Page 
    {

        private string _callbackresult = null;
        
        TrackingsN trackingsN;
        GeneralesN generalesN;
        Contifico jsonObject;
        ParametrosCorporativosN parametrosCorporativosN;
        private static readonly HttpClient client = new HttpClient();
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (Session["confirm"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", Session["confirm"].ToString(), true);
                    Session["confirm"] = null;
                }

                if (Session["usuario"] != null)
                {
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    CargarProvincias();

                
                    foreach (var item in usuE)
                    {

                        DateTime fechaFin = DateTime.Now;
                        txtFechaFin.Text = fechaFin.ToString("yyyy-MM-dd");
                        DateTime fechaInicio = fechaFin.AddMonths(-1);
                        txtFechaInicio.Text = fechaInicio.ToString("yyyy-MM-dd");
                        ddlEstadoOrden.SelectedValue = "INGRESADA";

                        if (item.rol == 1 || item.rol == 6 || item.rol == 2 || item.rol == 4 || item.rol == 8 || item.rol == 9)
                        {
                            Session["rol"] = item.rol;
                            Session["cod_usu"] = item.cod_usu;
                            if (item.rol == 6 )
                            {
                                dtgEnvios.Columns[12].Visible = false;
                                dtgEnvios.Columns[13].Visible = false;
                                dtgEnvios.Columns[17].Visible = false;
                            }
                        }
                        
                        else
                        {
                            Response.Redirect("MenuPrincipal.aspx");
                        }
                    }
                    
                }
                else
                {
                    Response.Redirect("MenuPrincipal.aspx");
                }

                

            }
        }

       

        public void CargarListado(DateTime datFechaIngreso, DateTime datFechaFin, string strOrdenEstado, string strTipoEnvio)
        {
            try
            {
                trackingsN = new TrackingsN();
                var list = trackingsN.ListadoEnviosCliente(ConfigurationManager.AppSettings["cnnSQL"], datFechaIngreso, datFechaFin, strOrdenEstado, strTipoEnvio);
                dtgEnvios.DataSource = list;
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
                    GridView dtgDetalle = (e.Row.FindControl("dtgEnviosDetalle") as GridView);
                    String idEnvio = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "idEnvio"));
                    var dat = new TrackingsN().ListadoEnviosDetalle(ConfigurationManager.AppSettings["cnnSQL"], idEnvio);
                    dtgDetalle.DataSource = dat;
                    dtgDetalle.DataBind();
                    dtgDetalle.Visible = true;

                    if (e.Row.Cells[3].Text == "ENVIO A DOMICILIO")
                    {
                        e.Row.Cells[3].ControlStyle.Font.Bold = true;

                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Blue;


                    }

                    if (e.Row.Cells[15].Text == "&nbsp;")
                    {
                        (e.Row.FindControl("btnRegistroCheckOut2") as ImageButton).ImageUrl = "~/images/notfile.png";


                    }
                    else
                    {
                        (e.Row.FindControl("btnRegistroCheckOut2") as ImageButton).Enabled = true;
                        (e.Row.FindControl("btnRegistroCheckOut2") as ImageButton).ToolTip = "Click para Ver el Archivo.";
                        (e.Row.FindControl("btnRegistroCheckOut2") as ImageButton).ImageUrl = "~/images/buscar.png";
                    }


                    if (e.Row.Cells[14].Text == "REALIZADO CHECK OUT")
                    {
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).Enabled = false;
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).ImageUrl = "~/images/checki.png";
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).ToolTip = "EL envio se encuentra finalizado.";

                    }
                    else
                    {
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).Enabled = true;
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).ToolTip = "Click para finalizar el Envio.";
                    }

                    if (e.Row.Cells[15].Text == "PAGO VERIFICADO")
                    {
                        (e.Row.FindControl("btnRevisarCheckOut") as ImageButton).Enabled = false;
                        (e.Row.FindControl("btnRevisarCheckOut") as ImageButton).ImageUrl = "~/images/dollarCheck.png";
                        (e.Row.FindControl("btnRevisarCheckOut") as ImageButton).ToolTip = "EL PAGO se encuentra verificado.";
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).Enabled = true;

                    }
                    else
                    {
                        (e.Row.FindControl("btnRevisarCheckOut") as ImageButton).Enabled = true;
                        (e.Row.FindControl("btnRevisarCheckOut") as ImageButton).ToolTip = "Click para Confirmar el Envio.";
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).Enabled = false;

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        

        string script;
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnlListado.Visible = true;
            if (!String.IsNullOrEmpty(txtFechaInicio.Text))
            {
                if (!String.IsNullOrEmpty(txtFechaFin.Text))
                {

                    CargarListado(Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text), ddlEstadoOrden.SelectedValue, ddlTipoEntrega.SelectedValue);
                }
                else
                {
                    script = "alert('La fecha fin es obligatoria para buscar.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                }
            }
            else
            {
                script = "alert('La fecha inicio es obligatoria para buscar.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);

            }
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();
            int cantidad = trackingsN.Envio(Convert.ToInt32(lbltitulo2.Text)).Count();

            if (cantidad == 0 && hddTipoEntrega.Value == "ENVIO A DOMICILIO")
            {
                hddValorEnvio.Value = "";

            }
            else
            { if (hddTipoEntrega.Value == "ENVIO A DOMICILIO")
                {
                    hddValorEnvio.Value = trackingsN.Envio(Convert.ToInt32(lbltitulo2.Text))["numSeguim"];
                    
                }
            }
            if (hddTipoEntrega.Value != "ENVIO A DOMICILIO")
            {
                hddValorEnvio.Value = "1";

            }
            trackingsN = new TrackingsN();
            if (!String.IsNullOrEmpty(txtObservacion.Text))
            {
                if (!String.IsNullOrEmpty(txtNumFacturaContf.Text))
                {
                    if (!String.IsNullOrEmpty(hddValorEnvio.Value) && hddValorEnvio.Value!= "0")
                    {
                        bool? res = trackingsN.ActualizaCheckOut(Convert.ToInt32(lbltitulo2.Text), txtObservacion.Text, Session["cod_usu"].ToString(), ddlMetodoPagoConfir.SelectedValue,txtNumFacturaContf.Text,  lblNumSe.Text, lblEnvido.Text);
                        var envi = trackingsN.EnvioCorreo(Convert.ToInt32(lbltitulo2.Text));
                        if (res == true && envi == 1 || envi == 0)
                    {
                        Session["confirm"] = "alert('Se finaliza correctamente la importación: " + lbltitulo2.Text + ".');";
                        Response.Redirect("BodegaLoidimpsa.aspx");
                    }
                    }
                    else
                    {
                        lblErrores.Text = "Esta orden de retiro es con Envío a domicilio, debe ingresarlo antes de finalizar.";
                        lblErrores.Visible = true;
                     
                        mdlPopup.Show();
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
        protected void btnGuardarFac_Click(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();
          if (!String.IsNullOrEmpty(txtNumFacturaContf.Text))
                {
                    bool? res = trackingsN.IngresoFactura(Convert.ToInt32(lbltitulo2.Text), txtNumFacturaContf.Text,Session["cod_usu"].ToString());

                    if (res == true)
                    {
                    lblConfirFac.Text = "Se ha guardado la Factura";
                        mdlPopup.Show();
                    }
                }
                else
                {
                    lblErrores.Text = "El numero de factura es obligatorio.";
                    lblErrores.Visible = true;
                    txtNumSeguim.Focus();
                    mdlPopup.Show();
                }

           

        }

        protected void btnGuardarEnv_Click(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();
            if (ddlEnviado.SelectedValue != "")
            {
                if (!String.IsNullOrEmpty(txtNumSeguim.Text) && !String.IsNullOrEmpty(ddlProvinciaBode.Text)  && !String.IsNullOrEmpty(ddlCiudadBode.Text)  )
                {
                    bool? res = trackingsN.IngresoEnvio(Convert.ToInt32(lbltitulo2.Text), txtNumSeguim.Text, 
                        ddlEnviado.SelectedValue, Session["cod_usu"].ToString(), ddlProvinciaBode.Text, ddlCiudadBode.Text);

                    if (res == true)
                    {

                        lblEnvido.Text = ddlEnviado.SelectedValue;
                        lblNumSe.Text = txtNumSeguim.Text;
                        pnlRegistro.Visible = true;
                        pnlAggEnvio.Visible = false;
                        pnlSegum.Visible = false;
                        pnlGuardar.Visible = false;
                        pnlAggCiudad.Visible = false;
                        pnlAggProv.Visible = false;
                        mdlPopup.Show();
                    }
                }
                else
                {
                    lblErrores.Text = "El numero de seguimiento es obligatorio.";
                    lblErrores.Visible = true;
                    txtNumSeguim.Focus();
                    mdlPopup.Show();
                }

            }
            else
            {
                lblErrores.Text = "Seleccionar una empresa de envio es obligatorio";
                lblErrores.Visible = true;
                ddlEnviado.Focus();
                mdlPopup.Show();
            }

        }

        protected void btnEnvio_Click(object sender, EventArgs e)
        {
            pnlAggEnvio.Visible = true;
            pnlSegum.Visible = true;
            pnlGuardar.Visible = true;
            mdlPopup.Show();
           

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(txtObservacion2.Text))
            {
                trackingsN = new TrackingsN();
                bool? res = trackingsN.ActualizaConfirm(ddlpagoVerifica.SelectedValue, Convert.ToInt32(lblorden.Text), Session["cod_usu"].ToString(), txtObservacion2.Text);

                if (res == true)
                {
                    Session["confirm"] = "alert('Se ha verificado el pago: " + lblorden.Text + ".');";
                    Response.Redirect("BodegaLoidimpsa.aspx");
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

        protected void btnRegistroCheckOut_Click(object sender, ImageClickEventArgs e)
        {

            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lbltitulo2.Text = commandArgs[1].ToString();
            lblNombreCliente.Text = commandArgs[0].ToString();
            hddTotalEnvio.Value = commandArgs[2].ToString();
            hddTipoPago.Value = commandArgs[3].ToString();
            hddTipoEntrega.Value = commandArgs[5].ToString();
            trackingsN = new TrackingsN();
           
            int cantidad = trackingsN.Factura(Convert.ToInt32(lbltitulo2.Text)).Count();

            if (cantidad != 0  )
            {
                txtNumFacturaContf.Text = trackingsN.Factura(Convert.ToInt32(lbltitulo2.Text))["codigoFactura"];

            }else {
                txtNumFacturaContf.Text = "";
                lblConfirFac.Text = "";
            }
            

            mdlPopup.Show();
            pnlFinalizar.Visible = true;
            pnlRevisar.Visible = false;
                btnEnvio.Visible = true;
            btnCheckOut.Visible = true;
            btnConfirm.Visible = false;
          
            if (commandArgs[4].ToString() == "B")
            {
                btnFacturar.Visible = true;
            }
        }

        protected void btnRevisarCheckOut_Click(object sender, ImageClickEventArgs e)
        {

            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });

            lblorden.Text = commandArgs[1].ToString();
            lblnombreC.Text = commandArgs[0].ToString();
            ddlpagoVerifica.SelectedValue = commandArgs[2].ToString();
            mdlPopup.Show();
            pnlFinalizar.Visible = false;
            pnlRevisar.Visible = true;
            btnEnvio.Visible = false;
            btnCheckOut.Visible = false;
            btnConfirm.Visible = true;

        }

        protected void btnRegistroCheckOut_ClickFile(object sender, ImageClickEventArgs e)
        {
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            var cedula = commandArgs[0].ToString();
            var nameFile = commandArgs[1].ToString();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string folder = path + "/temp/";
            string fullFilePath = "/temp/" + nameFile;
            string fullFile = path + "/temp/" + nameFile;
            string rutaFisica = @"C:/PAGO/" + cedula + "/" + nameFile;
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            if (File.Exists(rutaFisica))
            {
                if (!File.Exists(fullFile))
                {
                    File.Copy(rutaFisica, folder + "/" + nameFile);
                }
                else if (File.Exists(fullFile))
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
            
            if (ddlEnviado.SelectedValue == "LOIDIMP S.A.")
            {
                pnlAggProv.Visible = true;
                pnlAggCiudad.Visible = true;

                var guid = Guid.NewGuid();
                var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
                var seed = int.Parse(justNumbers.Substring(0, 4));

                var random = new Random(seed);
                var value = random.Next(0, 9);
                var value2 = random.Next(0, 9);
                var value3 = random.Next(0, 9);
                var value4 = random.Next(0, 9);

                txtNumSeguim.Text = "LE"+Convert.ToString(value)+ Convert.ToString(value2)+ Convert.ToString(value3)+ Convert.ToString(value4);
                ddlProvinciaBode.Items.Clear();
                ddlProvinciaBode.Items.Add(new ListItem("GUAYAS", "09"));
                CargarCantones(ddlProvinciaBode.SelectedValue);
            }
            if (ddlEnviado.SelectedValue == "SERVIENTREGA"){
                CargarProvincias();
                pnlAggProv.Visible = true;
                pnlAggCiudad.Visible = true;
                
               
            }
            if (ddlEnviado.SelectedValue != "SERVIENTREGA" && ddlEnviado.SelectedValue != "LOIDIMP S.A.")
            {
                txtNumSeguim.Text = "";
                pnlAggProv.Visible = false;
                pnlAggCiudad.Visible = false;


            }
            


            mdlPopup.Show();

        }

        protected void ddlProvinciaBode_TextChanged(object sender, EventArgs e)
        {
            CargarCantones(ddlProvinciaBode.SelectedValue);
            mdlPopup.Show();
        }


        private void CargarProvincias()
        {
            
           
            generalesN = new GeneralesN();
            var provincias = generalesN.Provincias();
            ddlProvinciaBode.Items.Clear();
            ddlProvinciaBode.Items.Add(new ListItem("Seleccionar", "0"));
            foreach (var item in provincias)
            {
                ddlProvinciaBode.Items.Add(new ListItem(item.Value, item.Key));
            }
            ddlProvinciaBode.Items.Add(new ListItem("SIN PROVINCIA", "NA"));
            
        }

        private void CargarCantones(string id_prov)
        {
            
            generalesN = new GeneralesN();
            var cantones = generalesN.Cantones(id_prov);
            ddlCiudadBode.Items.Clear();
            ddlCiudadBode.Items.Add(new ListItem("Seleccionar", "0"));
            foreach (var item in cantones)
            {
                ddlCiudadBode.Items.Add(new ListItem(item.Value, item.Key));
            }
            ddlCiudadBode.Items.Add(new ListItem("SIN CANTON", "NA"));
            
        }
        

        protected void btnFacturar_ClickAsync(object sender, EventArgs e)
        {

            generalesN = new GeneralesN();
            jsonObject = new Contifico();
            CobroE cobroE;
            CobroTc cobroTc;
            CobroTr cobroTr;
            var respues= "";
            string tpfac = "FAC";
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<int> idgoOperacion = new List<int>();
            int result_size = 2;
            int result_page = 1;
            string apiData = generalesN.Api("CONTIFICO")["apiToken"];
            string apiDataKey = generalesN.Api("CONTIFICO")["apiKey"];
            var output = "";
            double subTotal0 = 0;
            double subtotal12 = 0;
            double totalSinImp = 0;
            double ivaVal = 0;
            parametrosCorporativosN = new ParametrosCorporativosN();
            List<ParametrosCorporativosE> ivaParametro = new List<ParametrosCorporativosE>();
            ivaParametro = parametrosCorporativosN.BuscaParametrosCorporativos("IVA");
            foreach (var items in ivaParametro)
            {
                ivaVal = Convert.ToDouble(items.valordecimal);
            }
            var documento = new ProductoN().ObteneDocumentoFac(ConfigurationManager.AppSettings["cnnSQL"]);
            var descripcion = new ProductoN().ObtenerDescrip(ConfigurationManager.AppSettings["cnnSQL"], Convert.ToInt32(lbltitulo2.Text));
            ClienteC cliente = new ClienteN().BuscarClienteContf(ConfigurationManager.AppSettings["cnnSQL"], Convert.ToInt32(lbltitulo2.Text));
            EmpleadoC empleado = new EmpleadoN().BuscarEmpleadoContf(ConfigurationManager.AppSettings["cnnSQL"], Session["cod_usu"].ToString());
            List<ProductoE> productos = new ProductoN().BuscarProductoContf(ConfigurationManager.AppSettings["cnnSQL"], Convert.ToInt32(lbltitulo2.Text));
            List<Object> cobros = new List<Object>();
            if (hddTipoPago.Value == "EFECTIVO")
            {
                //var cobro = @"{'forma_cobro':'EF','monto':" + hddTotalEnvio.Value + ", 'fecha':'" + DateTime.Today.Date.ToString(@"dd\/MM\/yyyy") + "' }";

                cobroE = new CobroE()
                {
                    forma_cobro = "EF",
                    monto = Convert.ToDouble(hddTotalEnvio.Value),
                    fecha = DateTime.Today.Date.ToString(@"dd\/MM\/yyyy")

                };
 
                cobros.Add(cobroE);
                hddValLoidimp.Value = Convert.ToString(cobroE.monto);
            }
            else if (hddTipoPago.Value == "DEBITO/CREDITO	")
            {
                cobroTc = new CobroTc()
                {
                    forma_cobro = "TC",
                    monto = Convert.ToDouble(hddTotalEnvio.Value),
                    fecha = DateTime.Today.Date.ToString(@"dd\/MM\/yyyy"),
                    tipo_ping = "M"
                };
                cobros.Add(cobroTc);
                hddValLoidimp.Value = Convert.ToString(cobroTc.monto);

            }
            else if (hddTipoPago.Value == "TRANSFERENCIA/DEPOSITO")
            {
                cobroTr = new ProductoN().BuscarCobrosContf(ConfigurationManager.AppSettings["cnnSQL"], Convert.ToInt32(lbltitulo2.Text));
                cobros.Add(cobroTr);
                hddValLoidimp.Value = Convert.ToString(cobroTr.monto);
            }
            

            //string jsonString = (new JavaScriptSerializer()).Serialize((object)cobro);
            //var JSONObj2 = ser.Deserialize<Dictionary<string, string>>(jsonString); //JSON decoded

            foreach (ProductoE producto in productos)
            {
                if (producto.porcentaje_iva == 0)
                {
                    subTotal0 = producto.base_no_gravable;
                    totalSinImp += producto.base_no_gravable;
                }if(producto.producto_id == "gQbWn5G4MsP8Ma6w")
                {
                    producto.precio = producto.precio/1.12;
                }
                if (producto.producto_id == "pzb8qpJ46szg2dEw")
                {
                    producto.base_no_gravable = Math.Round(producto.precio * producto.cantidad - producto.porcentaje_descuento, 2);
                }
                else
                {

                    producto.base_gravable = Math.Round(producto.precio * producto.cantidad - producto.porcentaje_descuento,2);
                    subtotal12 += producto.base_gravable;
                }
                totalSinImp += producto.base_gravable;
                
            }
            //double suma = productos.Sum(item => Convert.ToDouble(item.base_gravable));
            double iva = Math.Round(subtotal12 * ivaVal,2);
            /*double subTotal2 = totalSinImp - subTotal0;
            double iva = subTotal2 * ivaVal;*/
            string valor = Convert.ToString(Convert.ToInt32(documento.Split('-')[2]) + 1);
            string documetNew = "";
            if (!String.IsNullOrEmpty(valor))
            {
                int tamanioDocu = 9;
                int cerosIzq = tamanioDocu - valor.Length;
                string ceros = string.Concat(Enumerable.Repeat("0", cerosIzq));
                documetNew = "001-002-" + ceros + valor;
            }
            
            
            var total = subTotal0 + subtotal12 + iva;
            hddValContif.Value = Convert.ToString( Math.Truncate((totalSinImp + iva) * 100) / 100);
          
            jsonObject.pos = apiData;
            jsonObject.fecha_emision = DateTime.Today.Date.ToString(@"dd\/MM\/yyyy");
            jsonObject.tipo_documento = "FAC";
            
            jsonObject.estado = "P";
            jsonObject.electronico = true;
            jsonObject.autorizacion = "";
            jsonObject.descripcion = descripcion;
            jsonObject.subtotal_0 = subTotal0;
            jsonObject.subtotal_12 = subtotal12;
            jsonObject.iva = iva;
            jsonObject.ice = 0.00;
            //jsonObject.total = Math.Truncate((totalSinImp + iva) * 100) / 100;
            jsonObject.total = Math.Round(total,2);
            jsonObject.cliente = cliente;
            jsonObject.vendedor = empleado;
            jsonObject.detalles = productos;
            jsonObject.cobros = (cobros);
            //  System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            
            var numdoc = Convert.ToString(Convert.ToInt32(documetNew.Split('-')[2]));

            var numfac = Convert.ToString(Convert.ToInt32(txtNumFacturaContf.Text.Split('-')[2]));
            if (Convert.ToInt32(numfac) >  Convert.ToInt32(numdoc))
            {
                jsonObject.documento = txtNumFacturaContf.Text; //falta
                output = ser.Serialize(jsonObject);
                postApi(apiDataKey, output, documetNew);
            }
            else
            {
                conApi(respues, apiDataKey, idgoOperacion, documetNew, tpfac, documento, result_size, result_page);

                jsonObject.documento = txtNumFacturaContf.Text;
                output = ser.Serialize(jsonObject);
                postApi(apiDataKey, output, txtNumFacturaContf.Text);
            }
            

            mdlPopup.Show();



        }

        protected void btnVerDoc_Click(object sender, EventArgs e)
        {
            generalesN = new GeneralesN();
            jsonObject = new Contifico();
            List<int> idgoOperacion = new List<int>();
            var respues = "";
            string tpfac = "FAC";
            int result_size = 2;
            int result_page = 1;
            //string apiData = generalesN.Api("CONTIFICO")["apiToken"];
            string apiDataKey = generalesN.Api("CONTIFICO")["apiKey"];
            
            var documento = new ProductoN().ObteneDocumentoFac(ConfigurationManager.AppSettings["cnnSQL"]);

            //string jsonString = (new JavaScriptSerializer()).Serialize((object)cobro);
            //var JSONObj2 = ser.Deserialize<Dictionary<string, string>>(jsonString); //JSON decoded

            var documetNew = genNumFac(documento);
            conApi( respues,  apiDataKey, idgoOperacion,  documetNew,  tpfac,  documento, result_size, result_page);
            
            mdlPopup.Show();

        }

        private string genNumFac(String documento)
        {
            string valor = Convert.ToString(Convert.ToInt32(documento.Split('-')[2]) + 1);
            string documetNew = "";
            if (!String.IsNullOrEmpty(valor))
            {
                int tamanioDocu = 9;
                int cerosIzq = tamanioDocu - valor.Length;
                string ceros = string.Concat(Enumerable.Repeat("0", cerosIzq));
                documetNew = "001-002-" + ceros + valor;
            }
            return documetNew;
        }

        private string numMax(string valor)
        {
            int tamanioDocu = 9;
            int cerosIzq = tamanioDocu - valor.Length;
            string ceros = string.Concat(Enumerable.Repeat("0", cerosIzq));
            var documetNew = "001-002-" + ceros + valor;
            return documetNew;
        }

        private void conApi(string respues, string apiDataKey, List<int> idgoOperacion, string documetNew, string tpfac, string documento, int result_size, int result_page)
        {
            try
            {
                var url = $"https://api.contifico.com/sistema/api/v1/registro/documento/?documento={documetNew}&tipo_documento={tpfac}&result_size={result_size}&result_page={result_page}";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("Authorization", apiDataKey);
                httpWebRequest.Accept = "application/json";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    respues = result;
                }
                string jsonObj = Convert.ToString(JsonConvert.DeserializeObject(respues));
                JArray jsonPreservar = JArray.Parse(jsonObj);
                foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                {
                    var ope = Convert.ToInt32(Convert.ToString(jsonOperaciones["documento"]).Split('-')[2]) + 1;
                    idgoOperacion.Add(ope);
                }
                string numMaxcon = Convert.ToString(idgoOperacion.Max() + 1);
                var numdoc = Convert.ToString(Convert.ToInt32(documento.Split('-')[2]));
                if (Convert.ToInt32(numMaxcon) > Convert.ToInt32(numdoc))
                {

                    txtNumFacturaContf.Text = numMax(numMaxcon);
                }
                else
                {
                    txtNumFacturaContf.Text = documento;
                }
            }
            catch (Exception ex)
            {
                String script = "alert(Respuesta :" + ex.Message + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);

            }
        }

        private void postApi(string apiDataKey, string output, string documetNew)
        {
            try
            {
                var json = "";
                var ope = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.contifico.com/sistema/api/v1/documento/");
                httpWebRequest.ContentType = "application/json; charset=UTF-8";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("Authorization", apiDataKey);


                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write(output);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var respues = streamReader.ReadToEnd();
                }

                
                txtNumFacturaContf.Text = documetNew;

                String script = "confirm('La factura : " + documetNew + " ha sido creada de manera correcta');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);


            }
            catch (WebException ex)
            {
                String script = "";
                /*String script = "alert('El servidor Contifico esta caido o el valor de pago no cuadra con los calculo del sistema, El valor de contifico es " + hddValContif.Value + ", para el aplicativo  Loidimpsa es el siguiente" + hddValLoidimp.Value + " decimas.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);*/
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        if ((int)response.StatusCode == 404) // Not Found
                        {
                             script = "alert('Error al ingresar la informacion " + response.ToString() +"');";
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
                        }
                        else
                        {
                            script = "alert('Error al ingresar la informacion " + response.ToString() + "');";
                        }
                    }
                }
                else
                {
                     script = "alert('Error al ingresar la informacion " + ex.ToString() + "');";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);

            }
        }







        //protected bool enviaContifico()
        //{
        //    generalesN = new GeneralesN();
        //    jsonObject = new Contifico();
        //    CobroE cobroE;
        //    CobroTc cobroTc;
        //    CobroTr cobroTr;
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    string apiData = generalesN.Api("CONTIFICO")["apiToken"];
        //    string apiDataKey = generalesN.Api("CONTIFICO")["apiKey"];
        //    double subTotal0 = 0;
        //    double totalSinImp = 0;
        //    double ivaVal = 0;
        //    parametrosCorporativosN = new ParametrosCorporativosN();
        //    List<ParametrosCorporativosE> ivaParametro = new List<ParametrosCorporativosE>();
        //    ivaParametro = parametrosCorporativosN.BuscaParametrosCorporativos("IVA");
        //    foreach (var items in ivaParametro)
        //    {
        //        ivaVal = Convert.ToDouble(items.valordecimal);
        //    }
        //    var documento = new ProductoN().ObteneDocumentoFac(ConfigurationManager.AppSettings["cnnSQL"]);
        //    var descripcion = new ProductoN().ObtenerDescrip(ConfigurationManager.AppSettings["cnnSQL"], Convert.ToInt32(lbltitulo2.Text));
        //    ClienteC cliente = new ClienteN().BuscarClienteContf(ConfigurationManager.AppSettings["cnnSQL"], Convert.ToInt32(lbltitulo2.Text));
        //    EmpleadoC empleado = new EmpleadoN().BuscarEmpleadoContf(ConfigurationManager.AppSettings["cnnSQL"], Session["cod_usu"].ToString());
        //    List<ProductoE> productos = new ProductoN().BuscarProductoContf(ConfigurationManager.AppSettings["cnnSQL"], Convert.ToInt32(lbltitulo2.Text));
        //    List<Object> cobros = new List<Object>();
        //    if (hddTipoPago.Value == "EFECTIVO")
        //    {
        //        //var cobro = @"{'forma_cobro':'EF','monto':" + hddTotalEnvio.Value + ", 'fecha':'" + DateTime.Today.Date.ToString(@"dd\/MM\/yyyy") + "' }";

        //        cobroE = new CobroE()
        //        {
        //            forma_cobro = "EF",
        //            monto = Convert.ToDouble(hddTotalEnvio.Value),
        //            fecha = DateTime.Today.Date.ToString(@"dd\/MM\/yyyy")

        //        };




        //        cobros.Add(cobroE);
        //    }
        //    else if (hddTipoPago.Value == "DEBITO/CREDITO	")
        //    {
        //        cobroTc = new CobroTc()
        //        {
        //            forma_cobro = "TC",
        //            monto = Convert.ToDouble( hddTotalEnvio.Value),
        //            fecha = DateTime.Today.Date.ToString(@"dd\/MM\/yyyy"),
        //            tipo_ping = "M"
        //        };
        //        cobros.Add(cobroTc);

        //    }
        //    else if (hddTipoPago.Value == "TRANSFERENCIA/DEPOSITO") {
        //         cobroTr = new ProductoN().BuscarCobrosContf(ConfigurationManager.AppSettings["cnnSQL"], Convert.ToInt32(lbltitulo2.Text));
        //        cobros.Add(cobroTr);
        //    }


        //    //string jsonString = (new JavaScriptSerializer()).Serialize((object)cobro);
        //    //var JSONObj2 = ser.Deserialize<Dictionary<string, string>>(jsonString); //JSON decoded

        //    foreach (ProductoE producto in productos)
        //    {
        //        if (producto.porcentaje_iva == 0)
        //        {
        //            subTotal0 = producto.base_no_gravable;
        //            totalSinImp += producto.base_no_gravable;
        //        }
        //        totalSinImp += producto.base_gravable;
        //    }

        //    double subTotal2 = totalSinImp - subTotal0;
        //    double iva = subTotal2 * ivaVal;
        //    string valor = Convert.ToString(Convert.ToInt32(documento.Split('-')[2]) + 1);
        //    string documetNew = "";
        //    if (!String.IsNullOrEmpty(valor))
        //    {
        //        int tamanioDocu = 9;
        //        int cerosIzq = tamanioDocu - valor.Length;
        //        string ceros = string.Concat(Enumerable.Repeat("0", cerosIzq));
        //        documetNew = "001-002-" + ceros + valor;
        //    }


        //    jsonObject.pos = apiData;
        //    jsonObject.fecha_emision = DateTime.Today.Date.ToString(@"dd\/MM\/yyyy");
        //    jsonObject.tipo_documento = "FAC";
        //    jsonObject.documento = documetNew; //falta
        //    jsonObject.estado = "P";
        //    jsonObject.electronico = true;
        //    jsonObject.autorizacion = "";
        //    jsonObject.descripcion = descripcion;
        //    jsonObject.subtotal_0 = Math.Round(subTotal0, 2);
        //    jsonObject.subtotal_12 = Math.Round(subTotal2, 2);
        //    jsonObject.iva = Math.Round(iva, 2);
        //    jsonObject.ice = 0.00;
        //    jsonObject.total = Math.Truncate((totalSinImp + iva)* 100) / 100 ;
        //    jsonObject.cliente = cliente;
        //    jsonObject.vendedor = empleado;
        //    jsonObject.detalles = productos;
        //    jsonObject.cobros = (cobros);
        //    //  System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        //    var output = ser.Serialize(jsonObject);
        //    try
        //    {
        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.contifico.com/sistema/api/v1/documento/");
        //        httpWebRequest.ContentType = "application/json";
        //        httpWebRequest.Method = "POST";
        //        httpWebRequest.Headers.Add("Authorization", apiDataKey);

        //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            string json = "";

        //            streamWriter.Write(output);
        //        }

        //        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        {
        //            var result = streamReader.ReadToEnd();
        //        }
        //        txtNumFacturaContf.Text = documetNew;
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        String script = "alert('El servidor Contifico esta caido o el valor de pago no cuadra con los calculo del sistema, revisar el cuadre de decimas.');";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
        //        return false;
        //    }


        //    //var JSONObj = ser.Deserialize<Dictionary<string, string>>(output); //JSON decoded

        //    //HttpClient httpClient = new HttpClient();
        //    //HttpContent content = new StringContent(
        //    //   JsonConvert.SerializeObject(jsonObject),
        //    //   Encoding.UTF8,
        //    //   "application/json"
        //    //);
        //    //HttpResponseMessage response = await httpClient.PostAsync("https://test.game.com/purchaseinitiation", content);


        //    //}
        //}




    }
}

