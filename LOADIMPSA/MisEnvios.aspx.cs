using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.Threading;
using System.Drawing;

namespace LOADIMPSA
{
    public partial class Mis_Envios : System.Web.UI.Page
    {

        TrackingsN trackingsN;
        ClienteN clienteN;
        GeneralesN generalesN;
        UsuarioN usuN;
        DataTable dt = new DataTable();
        DataTable dIm = new DataTable();
        DataTable dC = new DataTable();
        
        private static readonly HttpClient client = new HttpClient();
        protected async void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Session["respuesta"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", Session["respuesta"].ToString(), true);
                    Session["respuesta"] = null;
                }

                if (Session["confirm"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", Session["confirm"].ToString(), true);
                    Session["confirm"] = null;
                }

                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("numeroOrdenInterno", typeof(string)),
                                                            new DataColumn("tracking", typeof(string)),
                                                            new DataColumn("nombreTransportista", typeof(string)),
                                                            new DataColumn("peso", typeof(string)),
                    new DataColumn("paqueteSeparado", typeof(string))});

                Session["enviosGestion"] = dt;


                dIm.Columns.AddRange(new DataColumn[5] { new DataColumn("numeroOrdenInterno", typeof(string)),
                                                            new DataColumn("tracking", typeof(string)),
                                                            new DataColumn("nombreTransportista", typeof(string)),
                                                            new DataColumn("peso", typeof(string)),
                    new DataColumn("paqueteSeparado", typeof(string))});

                Session["enviosImpuesto"] = dIm;

                dC.Columns.AddRange(new DataColumn[7] { new DataColumn("numeroOrdenInterno", typeof(string)),
                                                            new DataColumn("tracking", typeof(string)),
                                                            new DataColumn("nombreTransportista", typeof(string)),
                                                            new DataColumn("peso", typeof(string)),

           new DataColumn("valorImp", typeof(string)),   new DataColumn("idImpC", typeof(string)), new DataColumn("descripImp", typeof(string))});

                Session["enviosGestionC"] = dC;

                dB.Columns.AddRange(new DataColumn[3] { new DataColumn("num", typeof(string)),
                                                            new DataColumn("Rubro", typeof(string)),
                                                            new DataColumn("Valor", typeof(string))

                });
                Session["calculoEnvio"] = dB;

                if (Session["usuario"] != null)
                {
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {

                        if (item.rol == 1)
                        {
                            ddlMetodoPago.Visible = true;
                            pnlBusquedaClientes.Visible = true;
                            hddCodigoUsuario.Value = item.cod_usu;
                            btnIngImp.Visible = true;
                            //ddlMetodoPago.Items[2].Enabled = false;
                            

                        }
                        else if (item.rol == 2 || item.rol == 4 || item.rol == 6 || item.rol == 8 || item.rol == 9)
                        {
                            pnlBusquedaClientes.Visible = true;
                            ddlMetodoPago.Visible = true;
                            hddCodigoUsuario.Value = item.cod_usu;
                            btnIngImp.Visible = true;
                            

                        }
                        else if (item.rol == 3 || item.rol == 5 || item.rol == 7)
                        {
                            dtgAduana.Columns[0].Visible = false;
                            btnIngImp.Visible = false;
                            pnlBusquedaClientes.Visible = false;
                            pnlDatosCliente.Visible = true;
                            DatosClientes(item.identificacion);
                            hddIdentificacion.Value = item.identificacion;
                            CargarEnviosEstado(hddIdentificacion.Value);
                            hddCodigoUsuario.Value = item.cod_usu;
                            Accordion1.Visible = true;
                            ddlMetodoPago.Visible = true;
                            dtgAduana.Columns[0].Visible = false;
                            
                            ddlMetodoPago.Items[1].Enabled = false;
                            
                        }

                        Session["rol"] = item.rol;
                        Session["rolUser"] = item.rol;


                        Session["listAdjuntos"] = new List<ArchivosAdjuntoE>();
                    }
                }
            }
        }
        #region
        int numOr;
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarEnviosEstado(Server.HtmlDecode(""));

            try
            {
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("numeroOrdenInterno", typeof(string)),
                                                            new DataColumn("tracking", typeof(string)),
                                                            new DataColumn("nombreTransportista", typeof(string)),
                                                            new DataColumn("peso", typeof(string)),
                    new DataColumn("paqueteSeparado", typeof(string))});

                Session["enviosGestion"] = dt;
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
            
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("numeroOrdenInterno", typeof(string)),
                                                            new DataColumn("tracking", typeof(string)),
                                                            new DataColumn("nombreTransportista", typeof(string)),
                                                            new DataColumn("peso", typeof(string)),
                    new DataColumn("paqueteSeparado", typeof(string))});

            Session["enviosGestion"] = dt;
            GridViewRow gv = gvClientes.SelectedRow;
            DatosClientes(Server.HtmlDecode(gv.Cells[1].Text));
            pnlDatosCliente.Visible = true;
            CargarEnviosEstado(Server.HtmlDecode(gv.Cells[1].Text));
            Accordion1.Visible = true;
            hddIdentificacion.Value = Server.HtmlDecode(gv.Cells[1].Text);
            hddNombreCliente.Value = Server.HtmlDecode(gv.Cells[2].Text);
            
        }
        
        private bool DatosClientes(string identificacion)
        {
            bool save = false;
            pnlDatosCliente.Visible = true;
            try
            {
                clienteN = new ClienteN();
                var datos = clienteN.DatosClientes(identificacion);
                if (datos.numeroidentificacion != null)
                {
                    lblNombresCom.Text = datos.primerNombre + " " + datos.segundoNombre + " " + datos.primerApellido + " " + datos.segundoApellido;
                    lBlCed.Text = datos.numeroidentificacion.ToString();
                    lblCodCliente.Text = "LO" + datos.idCasillero.ToString();
                    lblvalorFob.Text = datos.valorFob.ToString();
                    Session["rol"] = datos.id_rol.ToString();
                    
                }
            }
            catch (Exception ex)
            {

            }
            return save;
        }

        #endregion


        
        private void CargarEnviosEstado(string strCliente)
        {
            try
            {
                dtgRecibidoMiami.DataSource = null;
                dtgRecibidoMiami.DataBind();
                dtgAutorizadosVolar.DataSource = null;
                dtgAutorizadosVolar.DataBind();
                dtgAduana.DataSource = null;
                dtgAduana.DataBind();
                dtgBodegaLoidimpsa.DataSource = null;
                dtgBodegaLoidimpsa.DataBind();
                dtgBodegaC.DataSource = null;
                dtgBodegaC.DataBind();


                dtgFinalizado.DataSource = null;
                dtgFinalizado.DataBind();

                var lista = new TrackingsN().ListadoEnviosEstado(ConfigurationManager.AppSettings["cnnSQL"], strCliente);
                for (int i = 0; i < lista.Count; i++)
                {
                    for (int j = 0; j < lista[i].Count; j++)
                    {

                        if (lista[i][j].estado.Equals("MIAMI"))
                        {
                            var datoRecibido = lista[i];
                            dtgRecibidoMiami.DataSource = datoRecibido;
                            dtgRecibidoMiami.DataBind();
                            break;
                        }
                        else if (lista[i][j].estado.Equals("AUTORIZADO"))
                        {
                            var datoAutorizado = lista[i];
                            dtgAutorizadosVolar.DataSource = datoAutorizado;
                            dtgAutorizadosVolar.DataBind();
                            if (datoAutorizado.Count == 0)
                            {
                                dtgAutorizadosVolar.Columns[7].Visible = false;
                            }
                            break;
                        }
                        else if (lista[i][j].estado.Equals("ADUANA"))
                        {
                            var datoAduana = lista[i];
                            dtgAduana.DataSource = datoAduana;
                            dtgAduana.DataBind();


                            break;
                        }
                        else if (lista[i][j].estado.Equals("BODEGA B"))
                        {
                            var datoBodegaB = lista[i];
                            dtgBodegaLoidimpsa.DataSource = datoBodegaB;
                            dtgBodegaLoidimpsa.DataBind();
                           
                            break;
                        }

                        else if (lista[i][j].estado.Equals("BODEGA C"))
                        {
                            var datoBodegaC = lista[i];
                            dtgBodegaC.DataSource = datoBodegaC;
                            dtgBodegaC.DataBind();
                            break;
                        }

                        else if (lista[i][j].estado.Equals("FINALIZADO"))
                        {
                            var datoFinalizado = lista[i];
                            dtgFinalizado.DataSource = datoFinalizado;
                            dtgFinalizado.DataBind();
                            break;
                        }
                    }
                }
                if (dtgBodegaLoidimpsa.DataSource == null && dtgBodegaC.DataSource == null)
                {
                    dtgBodegaLoidimpsa.EmptyDataText = "No Existen envios que se encuentren en la Bodega Loidimpsa.";
                    dtgBodegaC.Visible = false;
                }
                else if (dtgBodegaLoidimpsa.DataSource != null && dtgBodegaC.DataSource == null)
                {
                    dtgBodegaC.Visible = false;
                }
                else if (dtgBodegaLoidimpsa.DataSource != null && dtgBodegaC.DataSource != null)
                {
                    dtgBodegaLoidimpsa.Visible = true;
                    dtgBodegaC.Visible = true;
                }
                else
                {
                    dtgBodegaLoidimpsa.Visible = false;
                    dtgBodegaC.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #region Cargar Factura
        //protected void UploadButton_Click(object sender, EventArgs e)
        //{
        //    if (fuploadImagen.HasFile == false)
        //    {
        //        UploadDetails.Text = "Primero seleccione el Archivo de Subida...";
        //        mdlPopup.Show();

        //    }
        //    else
        //    {
        //        byte[] input = fuploadImagen.FileBytes;
        //        Session["File"] = input;
        //        string FileName = fuploadImagen.FileName;
        //        UploadDetails.Text = string.Format(
        //                @"Pago Cargado: {0}<br />
        //      Tamaño: (in bytes): {1:N0}<br />
        //      Tipo de Imagen: {2}",
        //                  FileName,
        //                  fuploadImagen.FileBytes.Length,
        //                  fuploadImagen.PostedFile.ContentType);
        //        UploadDetails.Visible = true;
        //        int tamanio = fuploadImagen.PostedFile.ContentLength;
        //        byte[] ImagenOriginal = new byte[tamanio];
        //        fuploadImagen.PostedFile.InputStream.Read(ImagenOriginal, 0, tamanio);
        //        Bitmap ImagenOriginalBinaria = new Bitmap(fuploadImagen.PostedFile.InputStream);
        //        string ImageDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(ImagenOriginal);
        //        imgPreview2.ImageUrl = ImageDataURL64;
        //        mdlPopup.Show();
        //    }
        //}

        #endregion


        protected void btnAutorizar_Click(object sender, ImageClickEventArgs e)
        {
            lblErrores.Visible = false;
            mdlPopup.Show();
            hddNombreArchivo.Value = "";
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lblTituloV.Text = "ORDEN No: " + commandArgs[0].ToString();
            hddIdOrden.Value = commandArgs[0].ToString();
            lbltitulo2.Text = commandArgs[1].ToString();
            lblFechaRecibidoMiami.Text = commandArgs[2].ToString().Substring(0, 10);
            pnlAutorizar.Visible = true;
            pnlEnvios.Visible = false;
            btnActualizarF.Visible = false;
            pnlFileUp.Visible = false;
            btnGenerarOrdenEnvio.Visible = false;
            btnAutorizarEnvio.Visible = true;
            btnIngresarImp.Visible = false;
        }
        
        protected void btnLoadFile_Click(object sender, ImageClickEventArgs e)
        {
            lblErrores.Visible = false;
            mdlPopup.Show();
            hddNombreArchivo.Value = "";
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lblTituloV2.Text = "ORDEN No: " + commandArgs[0].ToString();
            hddIdOrden.Value = commandArgs[0].ToString();
            lbltitulo3.Text = commandArgs[1].ToString();
            
            pnlAutorizar.Visible = false;
            pnlEnvios.Visible = false;
            btnGenerarOrdenEnvio.Visible = false;
            btnAutorizarEnvio.Visible = false;
            btnIngresarImp.Visible = false;
            btnActualizarF.Visible = true;
            pnlFileUp.Visible = true;
        }

        string script;
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            if (gvArchivosResolutor.Rows.Count > 0)
            {
                dtgPago_RowDeleting(null, null);
                txtValor.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
            }
            if (!String.IsNullOrEmpty(hddTotalPeso.Value))
            {
                ///session["enviosGestion"] = null;

            }
        }
        protected void Check_Clicked(object sender, EventArgs e)
        {
            var chk = checkbox1.Checked;
            hddCk.Value = (chk == true) ? "1" : "";
            mdlPopup.Show();

        }

        protected void btnAutorizarEnvio_Click(object sender, EventArgs e)
        {

            trackingsN = new TrackingsN();

            if (!String.IsNullOrEmpty(txtValor.Text))
            {
                if (!String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    if (!String.IsNullOrEmpty(hddCk.Value) && hddCk.Value == "1")
                    {
                        if (gvArchivosResolutor.Rows.Count > 0)
                        {
                            byte[] documentoBytes = (byte[])Session["File"];
                            string nombreArchivoFactura = hddNombreArchivoFactura.Value;
                            if ((bool)trackingsN.ActualizaEnvioCliente(Convert.ToInt32(hddIdOrden.Value), nombreArchivoFactura, Convert.ToDecimal(txtValor.Text), hddCodigoUsuario.Value, txtDescripcion.Text, hddCk.Value))
                            {
                                Session["confirm"] = "alert('Se ha autorizado para volar el Envio " + hddIdOrden.Value + ".');";
                                Response.Redirect("MisEnvios.aspx");
                            }
                            else
                            {
                                lblErrores.Text = "Error en la Autorización.";
                                mdlPopup.Show();
                            }

                        }
                        else
                        {

                            lblErrores.Text = "No se ha cargado la Factura de la Orden de Envio.";
                            lblErrores.Visible = true;

                            mdlPopup.Show();
                        }
                    }
                    else
                    {
                        lblErrores.Text = "Estimado cliente, antes de continuar favor aceptar declaración de información";
                        lblErrores.Visible = true;
                        checkbox1.Focus();
                        mdlPopup.Show();
                    }
                }
                else
                {
                    lblErrores.Text = "La descripcion del envio es obligatoria antes de Autorizar el Vuelo del envio.";
                    lblErrores.Visible = true;
                    txtDescripcion.Focus();
                    mdlPopup.Show();
                }
            }
            else
            {
                lblErrores.Text = "El valor del Envio es un campo obligatorio para Autorizar el Vuelo del envio.";
                lblErrores.Visible = true;
                txtValor.Focus();
                mdlPopup.Show();
            }
        }
        
        protected void btnIngresarImp_Click(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();
           
            if (!String.IsNullOrEmpty(txtNumAduana.Text))
            {
                if (!String.IsNullOrEmpty(txtImpuesto.Text))
                {
                    if (!String.IsNullOrEmpty(txtObImp.Text))
                    {

                        
                        int? idImp = trackingsN.IngresoImpuestoAduana(Convert.ToInt32(hddIdOI.Value), ddlTipoPagoImp.SelectedValue,
                            txtObImp.Text, hddIdentificacion.Value, "EN TRANSITO Y PROCESO DE ADUANA", Convert.ToDecimal(hddTotalPesoImp.Value),
                           Convert.ToDecimal(txtImpuesto.Text), txtNumAduana.Text, hddCodigoUsuario.Value);
                        int contador = 0;
                        if (idImp > 0)
                        {
                            foreach (GridViewRow gr in dtgListadoImpuestoAduana.Rows)
                            {

                                var respuesta = trackingsN.GuardaInformacionDetalleImpuesto(Convert.ToInt32(idImp), Convert.ToInt32(gr.Cells[0].Text));
                                if (respuesta == true)
                                {
                                    contador++;
                                }
                            }
                            //mail para cliente , mail para ejecutivo de cuenta.
                            Session["respuesta"] = "alert('Se ha el ingreso de Impuesto #id :" + idImp + " con " + contador + " Orden/es agregada/as.');";
                            Response.Redirect("MisEnvios.aspx");

                        }
                        else
                        {
                            Session["respuesta"] = "alert('No se pudo Guardar el Impuesto');";
                            Response.Redirect("MisEnvios.aspx");
                        }
                    }
                    else
                    {

                        lblErrores.Text = "No se ha ingresado la Observación..";
                        lblErrores.Visible = true;

                        mdlPopup.Show();
                    }
                }
                else
                {
                    lblErrores.Text = "El valor de impuesto es obligatorio.";
                    lblErrores.Visible = true;
                    txtDescripcion.Focus();
                    mdlPopup.Show();
                }
            }
            else
            {
                lblErrores.Text = "El numero de Aduana es obligatorio.";
                lblErrores.Visible = true;
                txtValor.Focus();
                mdlPopup.Show();
            }
        }

        protected void btnActualizarFile_Click(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();

            if (gvArchivosResolutor2.Rows.Count > 0)
            {
                byte[] documentoBytes = (byte[])Session["File"];
                string nombreArchivoFactura = hddNombreArchivoFactura.Value;
                if ((bool)trackingsN.ActualizaArchivoFactura(Convert.ToInt32(hddIdOrden.Value), nombreArchivoFactura, hddCodigoUsuario.Value))
                {
                    Session["confirm"] = "alert('Se actualizado la factura con Orden #" + hddIdOrden.Value + ".');";
                    Response.Redirect("MisEnvios.aspx");
                }
                else
                {
                    lblErrores.Text = "Error en la Autorización.";
                    mdlPopup.Show();
                }

            }
            else
            {

                lblErrores.Text = "No se ha cargado la Factura de la Orden de Envio.";
                lblErrores.Visible = true;

                mdlPopup.Show();
            }

        }



        protected void btnGenerarOrden_Click(object sender, EventArgs e)
        {
            
            pnlImpuesto.Visible = false;
            btnIngresarImp.Visible = false;
            ddlTipoEnvio.SelectedIndex = 0;

            lblValorEnvioDomicilio.Visible = false;

            Button btn = (sender as Button);
            if (btn.ID == "btnGenerarOrden")
                hddCategoria.Value = "B";
            else
                hddCategoria.Value = "C";


            if (hddCategoria.Value == "B")
            {

                DataTable dp = Session["enviosGestion"] as DataTable;
                if (dp.Rows.Count <= 0)
                {
                    Session["confirm"] = "alert('No se ha seleccionado ninguna Orden');";
                    Response.Redirect("MisEnvios.aspx");


                }
                dtgListadoEnviosGestionar.DataSource = dp;
                dtgListadoEnviosGestionar.DataBind();
                
            }
            else
            {

                DataTable dp = Session["enviosGestionC"] as DataTable;
                if (dp.Rows.Count <= 0)
                {
                    Session["confirm"] = "alert('No se ha seleccionado ninguna Orden');";
                    Response.Redirect("MisEnvios.aspx");


                }
                dtgListadoEnviosGestionarC.DataSource = dp;
                dtgListadoEnviosGestionarC.DataBind();
                pnldtgCatgC.Visible = true;
                pnldtgCatgB.Visible = false;
            }
            ddlTipoEnvio_SelectedIndexChanged(this, EventArgs.Empty);

            mdlPopup.Show();

            hddNombreArchivo.Value = "";
            //pnlCargarPago.Visible = false;

            lblErrores.Visible = false;
            //lblArchivoCargado.Visible = false;
            usuN = new UsuarioN();
            lblTituloV.Text = "GENERAR ORDEN DE ENVIO";
            btnActualizarF.Visible = false;
            pnlFileUp.Visible = false;
            pnlAutorizar.Visible = false;
            pnlEnvios.Visible = true;
            btnGenerarOrdenEnvio.Visible = true;
            btnAutorizarEnvio.Visible = false;
            if (dtgListadoEnviosGestionar.Rows.Count > 0 || dtgListadoEnviosGestionarC.Rows.Count > 0)
            {
                btnGenerarOrdenEnvio.Visible = true;
            }
            else
            {
                btnGenerarOrdenEnvio.Visible = false;
            }

            List<ListaPuntos> puntos = usuN.ListaPuntos(hddCodigoUsuario.Value);
            if (puntos.Count == 0)
            {
                lblPuntos.Text = "Ud. cuenta con 0 pts, aún no podra realizar canjes.";

            }

            else
            {
                lblPuntos.Text = "Ud. cuenta con  + " + puntos[0].puntosObtenidos + " pts disponibles";
                btnVerPro.Visible = true;
            }
            
            CargarCupones();

        }
        protected void btnIrCanje_Click(object sender, CommandEventArgs e)
        {

            mdlPopup.Show();
            string s = "window.open('LugarCanje.aspx', '_new_tab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", s, true);






        }
        
        protected void btnCargarImpuesto_Click(object sender, EventArgs e)
        {
            ddlTipoPagoImp.SelectedIndex = 0;



            DataTable dp = Session["enviosImpuesto"] as DataTable;
            if (dp.Rows.Count <= 0)
            {
                Session["confirm"] = "alert('No se ha seleccionado ninguna Orden');";
                Response.Redirect("MisEnvios.aspx");


            }
            
            dtgListadoImpuestoAduana.DataSource = dp;
            dtgListadoImpuestoAduana.DataBind();


            ddlTipoEnvioImpuesto_SelectedIndexChanged(this, EventArgs.Empty);

            mdlPopup.Show();
            hddNombreArchivo.Value = "";
            //pnlCargarPago.Visible = false;

            lblErrores.Visible = false;
            //lblArchivoCargado.Visible = false;

            lblTituloV.Text = "INGRESAR IMPUESTO ADUANA";
            pnlImpuesto.Visible = true;
            btnActualizarF.Visible = false;
            pnlFileUp.Visible = false;
            pnlAutorizar.Visible = false;
            pnlEnvios.Visible = false;
            btnGenerarOrdenEnvio.Visible = false;
            btnAutorizarEnvio.Visible = false;
            if (dtgListadoImpuestoAduana.Rows.Count > 0)
            {
                btnIngresarImp.Visible = true;
            }
            else
            {
                btnIngresarImp.Visible = false;
            }

        }
        
        protected void chkEstadoImp_CheckedChanged(object sender, EventArgs e)
        {
            
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                int index = row.RowIndex;
                CheckBox cb1 = (CheckBox)dtgAduana.Rows[index].FindControl("chkImpuesto");
                if (cb1.Checked == true)
                {
                    DataTable dp = Session["enviosImpuesto"] as DataTable;
                    DataRow dr1 = dp.NewRow();
                    numOr = Convert.ToInt32(dtgAduana.Rows[index].Cells[1].Text);
                    dr1[0] = Server.HtmlDecode(dtgAduana.Rows[index].Cells[1].Text);
                    dr1[1] = Server.HtmlDecode(dtgAduana.Rows[index].Cells[2].Text);
                    dr1[2] = Server.HtmlDecode(dtgAduana.Rows[index].Cells[4].Text);
                    dr1[3] = Server.HtmlDecode(dtgAduana.Rows[index].Cells[5].Text);
                    dr1[4] = Server.HtmlDecode(dtgAduana.Rows[index].Cells[6].Text);
                    dp.Rows.Add(dr1);

                    Session["enviosImpuesto"] = dp;
                }
                else if (cb1.Checked == false)
                {
                    DataTable dp = Session["enviosImpuesto"] as DataTable;
                    for (int i = dp.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dp.Rows[i];
                        if (((dr["numeroOrdenInterno"]).ToString() == Server.HtmlDecode(dtgBodegaLoidimpsa.Rows[index].Cells[1].Text)))
                        {
                            dr.Delete();
                        }
                    }
                    Session["enviosImpuesto"] = dp;
                }
                hddIdOI.Value =numOr.ToString();
            }
            catch (Exception ex)
            {

            }
        }
        protected void chkEstado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                int index = row.RowIndex;
                CheckBox cb1 = (CheckBox)dtgBodegaLoidimpsa.Rows[index].FindControl("chkEstado");
                if (cb1.Checked == true)
                {
                    DataTable dp = Session["enviosGestion"] as DataTable;
                    DataRow dr1 = dp.NewRow();
                    dr1[0] = Server.HtmlDecode(dtgBodegaLoidimpsa.Rows[index].Cells[1].Text);
                    dr1[1] = Server.HtmlDecode(dtgBodegaLoidimpsa.Rows[index].Cells[2].Text);
                    dr1[2] = Server.HtmlDecode(dtgBodegaLoidimpsa.Rows[index].Cells[4].Text);
                    dr1[3] = Server.HtmlDecode(dtgBodegaLoidimpsa.Rows[index].Cells[5].Text);
                    dr1[4] = Server.HtmlDecode(dtgBodegaLoidimpsa.Rows[index].Cells[9].Text);
                    dp.Rows.Add(dr1);

                    Session["enviosGestion"] = dp;
                }
                else if (cb1.Checked == false)
                {
                    DataTable dp = Session["enviosGestion"] as DataTable;
                    for (int i = dp.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dp.Rows[i];
                        if (((dr["numeroOrdenInterno"]).ToString() == Server.HtmlDecode(dtgBodegaLoidimpsa.Rows[index].Cells[1].Text)))
                        {
                            dr.Delete();
                        }
                    }
                    Session["enviosGestion"] = dp;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnGenerarOrdenEnvio_Click(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();
            int contador = 0;
            int? codigoSolicitud = 0;
            double valorDesc = 0;
            var valorNumDesc = "";

            string nombreArchivoPago = hddNombreArchivo.Value;

            if (nombreArchivoPago != "")
            {
                if (ddlBancos.SelectedValue != "*")
                {
                    
                    if (Convert.ToDouble(lblValorEnvioDomicilioDesc.Text) != Convert.ToDouble(lblValorEnvioDomicilio.Text))
                    {
                        valorDesc = Convert.ToDouble(lblValorEnvioDomicilioDesc.Text);
                        valorNumDesc = DropDownList1.Text;
                    }
                    else
                    {
                        valorDesc = Convert.ToDouble(lblValorEnvioDomicilio.Text);
                        valorNumDesc = null;
                    }
                    codigoSolicitud = trackingsN.GeneraOrdenEnvio(ddlTipoEnvio.SelectedValue,
                        hddCodigoUsuario.Value, nombreArchivoPago, hddCategoria.Value, txtCodigo.Text,
                        hddIdentificacion.Value, Decimal.Parse(hddTotalPeso.Value), Decimal.Parse(valorDesc.ToString()), Decimal.Parse(hddTotalEnvio.Value), ddlMetodoPago.SelectedValue, ddlBancos.SelectedValue, valorNumDesc, hddIdentificacion.Value);

                    if (hddCategoria.Value == "B")
                    {
                        foreach (GridViewRow gr in dtgListadoEnviosGestionar.Rows)
                        {

                            var respuesta = trackingsN.GuardaInformacionDetalleEnvio(Convert.ToInt32(codigoSolicitud), Convert.ToInt32(gr.Cells[0].Text));
                            if (respuesta == true)
                            {
                                contador++;
                            }
                        }
                    }
                    else
                    {
                        foreach (GridViewRow gr in dtgListadoEnviosGestionarC.Rows)
                        {

                            var respuesta = trackingsN.GuardaInformacionDetalleEnvio(Convert.ToInt32(codigoSolicitud), Convert.ToInt32(gr.Cells[0].Text));
                            if (respuesta == true)
                            {
                                contador++;
                            }
                        }
                    }
                    //mail para cliente , mail para ejecutivo de cuenta.
                    Session["respuesta"] = "alert('Se ha generado la orden de retiro: " + codigoSolicitud + " con " + contador + " guías de entrega.');";
                    Response.Redirect("MisEnvios.aspx");
                }
                else
                {
                    lblErrores.Text = "Seleccionar la cuenta bancaria donde se realizo la transferencia.";
                    lblErrores.Visible = true;
                    ddlBancos.Focus();
                    mdlPopup.Show();
                }
            }
            else if (ddlMetodoPago.SelectedValue == "EFECTIVO")
            {
                if (Convert.ToDouble(lblValorEnvioDomicilioDesc.Text) != Convert.ToDouble(lblValorEnvioDomicilio.Text))
                {
                    valorDesc = Convert.ToDouble(lblValorEnvioDomicilioDesc.Text);
                    valorNumDesc = lblValorEnvioDomicilio.Text;
                }
                else
                {
                    valorDesc = Convert.ToDouble(lblValorEnvioDomicilio.Text);
                    valorNumDesc = "";
                }
                codigoSolicitud = trackingsN.GeneraOrdenEnvio(ddlTipoEnvio.SelectedValue,
                    hddCodigoUsuario.Value, nombreArchivoPago, hddCategoria.Value, txtCodigo.Text,
                    hddIdentificacion.Value, Decimal.Parse(hddTotalPeso.Value), Decimal.Parse(valorDesc.ToString()), Decimal.Parse(hddTotalEnvio.Value), ddlMetodoPago.SelectedValue, ddlBancos.SelectedValue, valorNumDesc, hddIdentificacion.Value);
                if (hddCategoria.Value == "B")
                {
                    foreach (GridViewRow gr in dtgListadoEnviosGestionar.Rows)
                    {

                        var respuesta = trackingsN.GuardaInformacionDetalleEnvio(Convert.ToInt32(codigoSolicitud), Convert.ToInt32(gr.Cells[0].Text));
                        if (respuesta == true)
                        {
                            contador++;
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow gr in dtgListadoEnviosGestionarC.Rows)
                    {

                        var respuesta = trackingsN.GuardaInformacionDetalleEnvio(Convert.ToInt32(codigoSolicitud), Convert.ToInt32(gr.Cells[0].Text));
                        if (respuesta == true)
                        {
                            contador++;
                        }
                    }
                }
                //mail para cliente , mail para ejecutivo de cuenta.
                Session["respuesta"] = "alert('Se ha generado la orden de retiro: " + codigoSolicitud + " con " + contador + " guías de entrega.');";
                Response.Redirect("MisEnvios.aspx");
            }
            else if (ddlMetodoPago.SelectedValue == "DEBITO/CREDITO" && lblConfirm.Text == "PAGO CORRECTO" & hddVerificar.Value == "confirmado")
            {
                if (Convert.ToDouble(lblValorEnvioDomicilioDesc.Text) != Convert.ToDouble(lblValorEnvioDomicilio.Text))
                {
                    valorDesc = Convert.ToDouble(lblValorEnvioDomicilioDesc.Text);
                    valorNumDesc = lblValorEnvioDomicilio.Text;
                }
                else
                {
                    valorDesc = Convert.ToDouble(lblValorEnvioDomicilio.Text);
                    valorNumDesc = "";
                }
                codigoSolicitud = trackingsN.GeneraOrdenEnvio(ddlTipoEnvio.SelectedValue,
                    hddCodigoUsuario.Value, nombreArchivoPago, hddCategoria.Value, txtCodigo.Text,
                    hddIdentificacion.Value, Decimal.Parse(hddTotalPeso.Value), Decimal.Parse(valorDesc.ToString()), Decimal.Parse(hddTotalEnvio.Value), ddlMetodoPago.SelectedValue, ddlBancos.SelectedValue, valorNumDesc, hddIdentificacion.Value);

                if (hddCategoria.Value == "B")
                {
                    foreach (GridViewRow gr in dtgListadoEnviosGestionar.Rows)
                    {

                        var respuesta = trackingsN.GuardaInformacionDetalleEnvio(Convert.ToInt32(codigoSolicitud), Convert.ToInt32(gr.Cells[0].Text));
                        if (respuesta == true)
                        {
                            contador++;
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow gr in dtgListadoEnviosGestionarC.Rows)
                    {

                        var respuesta = trackingsN.GuardaInformacionDetalleEnvio(Convert.ToInt32(codigoSolicitud), Convert.ToInt32(gr.Cells[0].Text));
                        if (respuesta == true)
                        {
                            contador++;
                        }
                    }
                }
                //mail para cliente , mail para ejecutivo de cuenta.
                Session["respuesta"] = "alert('Se ha generado la orden de retiro: " + codigoSolicitud + " con " + contador + " guías de entrega.');";
                Response.Redirect("MisEnvios.aspx");
            }

            else
            {
                lblErrores.Text = "Porfavor carge el documento de  transferencia del pago o realice pago en efectivo, en nuestra oficina Guayaquil.";
                lblErrores.Visible = true;
                ddlTipoPago_SelectedIndexChanged(null, null);
                mdlPopup.Show();
            }





        }

        decimal total;
        decimal contarSep;
        protected void dtgListadoEnviosGestionar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    total += Convert.ToDecimal(e.Row.Cells[3].Text.Replace("$ ", "").Replace("$", ""));
                    if (e.Row.Cells[4].Text == "SI")
                    {
                        contarSep += 1;
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    parametrosCorporativosN = new ParametrosCorporativosN();
                    double tarifaSepCliente = 0;
                    double tarifaSepClientVip = 0;
                    List<ParametrosCorporativosE> tarifaClietSep = new List<ParametrosCorporativosE>();
                    tarifaClietSep = parametrosCorporativosN.BuscaParametrosCorporativos("SEPRACIONTAFCLIENTE");
                    foreach (var items in tarifaClietSep)
                    {
                        tarifaSepCliente = Convert.ToDouble(items.valordecimal);
                    }

                    List<ParametrosCorporativosE> tarifaClietVipSep = new List<ParametrosCorporativosE>();
                    tarifaClietVipSep = parametrosCorporativosN.BuscaParametrosCorporativos("SEPRACIONTAFCLIENTEVIP");
                    foreach (var items in tarifaClietVipSep)
                    {
                        tarifaSepClientVip = Convert.ToDouble(items.valordecimal);
                    }

                    var rol = Session["rol"].ToString();
                    e.Row.Cells[2].Text = "Peso total en (lbs): ";
                    e.Row.Cells[3].Text = total.ToString();
                    hddTotalPeso.Value = total.ToString();
                    if (rol == "3")
                    {
                        hddTotalSep.Value = (Convert.ToDouble(contarSep) * tarifaSepCliente).ToString();
                    }
                    else if (rol == "5")
                    {
                        hddTotalSep.Value = (Convert.ToDouble(contarSep) * tarifaSepClientVip).ToString();
                    }

                    decimal pagoServicioDomicilio;
                    // Calculo de Envio a Domicilio
                    if (Math.Round(Convert.ToDouble(hddTotalPeso.Value), 2) > 2.2)
                    {
                        decimal librasextra = Convert.ToDecimal(hddTotalPeso.Value) - Convert.ToDecimal(2.2);
                        decimal pagoextraEnvio = librasextra * Convert.ToDecimal(0.50);
                        pagoServicioDomicilio = Math.Round(Convert.ToDecimal(5 + pagoextraEnvio), 2);

                    }
                    else
                    {
                        pagoServicioDomicilio = 4;

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        decimal totalC;
        decimal totalImp = 0;
        decimal idAnterior = 0;
        protected void dtgListadoEnviosGestionarC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    totalC += Convert.ToDecimal(e.Row.Cells[3].Text.Replace("$ ", "").Replace("$", ""));
                    decimal idActual = Convert.ToDecimal(e.Row.Cells[5].Text);
                    string tipPago = (e.Row.Cells[6].Text);
                    if (idActual != idAnterior && tipPago != "PAGADO POR CLIENTE")
                    {
                        idAnterior = idActual;
                        totalImp += Convert.ToDecimal(e.Row.Cells[4].Text);
                    }

                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[2].Text = "Peso total en (lbs): ";
                    e.Row.Cells[3].Text = totalC.ToString();
                    e.Row.Cells[4].Text = "Impuesto Total a cancelar:" + totalImp.ToString();

                    hddTotalPeso.Value = totalC.ToString();
                    hddTotalImp.Value = totalImp.ToString();
                    decimal pagoServicioDomicilio;
                    // Calculo de Envio a Domicilio
                    if (Math.Round(Convert.ToDouble(hddTotalPeso.Value), 2) > 2.2)
                    {
                        decimal librasextra = Convert.ToDecimal(hddTotalPeso.Value) - Convert.ToDecimal(2.2);
                        decimal pagoextraEnvio = librasextra * Convert.ToDecimal(0.50);
                        pagoServicioDomicilio = Math.Round(Convert.ToDecimal(5 + pagoextraEnvio), 2);

                    }
                    else
                    {
                        pagoServicioDomicilio = 4;

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        decimal total2;
        
        protected void dtgListadoEnviosImpuesto_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    
                    total2 += Convert.ToDecimal(e.Row.Cells[3].Text.Replace("$ ", "").Replace("$", ""));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[2].Text = "Peso total en (lbs): ";
                    e.Row.Cells[3].Text = total2.ToString();
                    hddTotalPesoImp.Value = total2.ToString();


                }
            }
            catch (Exception ex)
            {

            }
        }



        #region Cargar Archivo Pago


        //private void GuardarArchivo(HttpPostedFile file)
        //{
        //    // Se carga la ruta física de la carpeta temp del sitio
        //    string ruta = "C:/Pagos/" + hddIdentificacion.Value + "/";
        //    Session["ruta"] = ruta;
        //    // Si el directorio no existe, crearlo
        //    if (!Directory.Exists(ruta))
        //        Directory.CreateDirectory(ruta);

        //    string archivo = String.Format("{0}\\{1}", ruta, file.FileName);

        //    // Verificar que el archivo no exista
        //    if (File.Exists(archivo))
        //    {
        //        lblErrores.Text = "El archivo ya existe.";
        //        lblErrores.Visible = true;
        //        mdlPopup.Show();
        //    }
        //    else
        //    {
        //        file.SaveAs(archivo);
        //        hddNombreArchivo.Value = file.FileName;
        //        lblArchivoCargado.Text = "El archivo:" + file.FileName + " se cargo correctamente";
        //        lblArchivoCargado.Visible = true;
        //    }
        //}

        DataTable dB = new DataTable();
        ParametrosCorporativosN parametrosCorporativosN;
        double totalSumaCatB;
        protected void ddlTipoEnvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            var rol = Session["rol"].ToString();
            var rolUser = Session["rolUser"].ToString();
            decimal libras = 0;
            double taza = 0;
            double costo = 0;
            double nacionalizacion = 0;
            double fleteInter = 0;
            double ivaVal = 0;
            double servicioDomicilio = 0;
            double servicioVipDomicilio = 0;
            double valorMinCourier = 0;
            double valorMinCourierVip = 0;
            parametrosCorporativosN = new ParametrosCorporativosN();

            List<ParametrosCorporativosE> valorMinParametro = new List<ParametrosCorporativosE>();
            valorMinParametro = parametrosCorporativosN.BuscaParametrosCorporativos("VALORMINCOUR");
            foreach (var items in valorMinParametro)
            {
                valorMinCourier = Convert.ToDouble(items.valordecimal);
            }

            List<ParametrosCorporativosE> valorMinParametroVip = new List<ParametrosCorporativosE>();
            valorMinParametroVip = parametrosCorporativosN.BuscaParametrosCorporativos("VALORMINCOURVIP");
            foreach (var items in valorMinParametroVip)
            {
                valorMinCourierVip = Convert.ToDouble(items.valordecimal);
            }

            List<ParametrosCorporativosE> ivaParametro = new List<ParametrosCorporativosE>();
            ivaParametro = parametrosCorporativosN.BuscaParametrosCorporativos("IVA");
            foreach (var items in ivaParametro)
            {

                ivaVal = Convert.ToDouble(items.valordecimal);
            }
            List<ParametrosCorporativosE> envioDomiciParametro = new List<ParametrosCorporativosE>();
            envioDomiciParametro = parametrosCorporativosN.BuscaParametrosCorporativos("VALORDOMICILIO");
            foreach (var items in envioDomiciParametro)
            {
                servicioDomicilio = Convert.ToDouble(items.valordecimal);
            }


            List<ParametrosCorporativosE> envioDomiciVipParametro = new List<ParametrosCorporativosE>();
            envioDomiciVipParametro = parametrosCorporativosN.BuscaParametrosCorporativos("VALORDOMICILIOVIP");
            foreach (var items in envioDomiciVipParametro)
            {
                servicioVipDomicilio = Convert.ToDouble(items.valordecimal);
            }
            if (Session["rol"].ToString() == "5")
            {
                List<ParametrosCorporativosE> tazaP = new List<ParametrosCorporativosE>();
                tazaP = parametrosCorporativosN.BuscaParametrosCorporativos("TAZAVIP");
                foreach (var items in tazaP)
                {
                    taza = Convert.ToDouble(items.valordecimal);
                }
                List<ParametrosCorporativosE> libra = new List<ParametrosCorporativosE>();
                libra = parametrosCorporativosN.BuscaParametrosCorporativos("COSTOLIBREVIP");
                foreach (var items in libra)
                {
                    libras = Convert.ToDecimal(items.valordecimal);
                }
                List<ParametrosCorporativosE> costoE = new List<ParametrosCorporativosE>();
                costoE = parametrosCorporativosN.BuscaParametrosCorporativos("COSTOVIP");
                foreach (var items in costoE)
                {
                    costo = Convert.ToDouble(items.valordecimal);
                }

                List<ParametrosCorporativosE> parametroNacionalizacion = new List<ParametrosCorporativosE>();

                parametroNacionalizacion = parametrosCorporativosN.BuscaParametrosCorporativos("NACIONALIZACION");
                foreach (var items in parametroNacionalizacion)
                {
                    nacionalizacion = Convert.ToDouble(items.valordecimal);
                }
                List<ParametrosCorporativosE> FleteC = new List<ParametrosCorporativosE>();

                FleteC = parametrosCorporativosN.BuscaParametrosCorporativos("TAZAFLETEINTERNACIONAL");
                foreach (var items in FleteC)
                {
                    fleteInter = Convert.ToDouble(items.valordecimal);
                }

            }
            else
            {
                List<ParametrosCorporativosE> tazaP = new List<ParametrosCorporativosE>();
                tazaP = parametrosCorporativosN.BuscaParametrosCorporativos("TAZA");
                foreach (var items in tazaP)
                {
                    taza = Convert.ToDouble(items.valordecimal);
                }

                List<ParametrosCorporativosE> libra = new List<ParametrosCorporativosE>();
                libra = parametrosCorporativosN.BuscaParametrosCorporativos("COSTOLIBRA");
                foreach (var items in libra)
                {
                    libras = Convert.ToDecimal(items.valordecimal);
                }
                List<ParametrosCorporativosE> costoE = new List<ParametrosCorporativosE>();
                costoE = parametrosCorporativosN.BuscaParametrosCorporativos("COSTO");
                foreach (var items in costoE)
                {
                    costo = Convert.ToDouble(items.valordecimal);
                }

                List<ParametrosCorporativosE> parametroNacionalizacion = new List<ParametrosCorporativosE>();

                parametroNacionalizacion = parametrosCorporativosN.BuscaParametrosCorporativos("NACIONALIZACIONVIP");
                foreach (var items in parametroNacionalizacion)
                {
                    nacionalizacion = Convert.ToDouble(items.valordecimal);
                }

                List<ParametrosCorporativosE> FleteC = new List<ParametrosCorporativosE>();

                FleteC = parametrosCorporativosN.BuscaParametrosCorporativos("TAZAFLETEINTERNACIONAL");
                foreach (var items in FleteC)
                {
                    fleteInter = Convert.ToDouble(items.valordecimal);
                }
            }





            double respuesta = 0;
            double adicSeparar;
            double pagoServicioDomicilio = 0;
            double fleteInterna = 0;


            if (hddCategoria.Value == "B")
            {
                var valor = (hddTotalSep.Value == "") ? 0 : Convert.ToDouble(hddTotalSep.Value);
                adicSeparar = valor;

                dtgCalculoEnvioB.Visible = true;


                if (ddlTipoEnvio.SelectedValue == "ENVIO A DOMICILIO")
                {
                    lblValorEnvioDomicilio.Visible = true;
                    ddlMetodoPago.Items[1].Enabled = false;

                    if (Convert.ToDouble(hddTotalPeso.Value) < 0.5)
                    {
                        if (rol == "3")
                        {

                            respuesta = valorMinCourier;
                            pagoServicioDomicilio = servicioDomicilio;
                        }
                        else if (rol == "5")
                        {
                            respuesta = valorMinCourierVip;
                            pagoServicioDomicilio = servicioVipDomicilio;
                        }

                    }

                    else if (Math.Round(Convert.ToDouble(hddTotalPeso.Value), 2) > 2.2)
                    {
                        decimal librasextra = Convert.ToDecimal(hddTotalPeso.Value) - Convert.ToDecimal(2.2);
                        decimal pagoextraEnvio = librasextra * Convert.ToDecimal(libras);
                        pagoServicioDomicilio = Math.Round(Convert.ToDouble(costo + Convert.ToDouble(pagoextraEnvio)), 2);
                        respuesta = Math.Truncate((Convert.ToDouble(hddTotalPeso.Value) * taza) * 100) / 100;

                    }
                    else
                    {
                        respuesta = Math.Truncate((Convert.ToDouble(hddTotalPeso.Value) * taza) * 100) / 100;

                        pagoServicioDomicilio = Math.Round(Convert.ToDouble(costo), 2);

                        //Elimine una fila

                    }
                    //pnlCargarPago.Visible = true;
                    ddlTipoPago_SelectedIndexChanged(null, null);

                    mdlPopup.Show();
                }
                else if (ddlTipoEnvio.SelectedValue == "ENVIO A DOMICILIO (RESTO DEL PAIS)")
                {
                    lblValorEnvioDomicilio.Visible = true;
                    Label17.Visible = true;
                    //pnlCargarPago.Visible = true;
                    ddlTipoPago_SelectedIndexChanged(null, null);
                    mdlPopup.Show();
                    if (Math.Round(Convert.ToDouble(hddTotalPeso.Value), 2) > 2.2)
                    {
                        decimal librasextra = Convert.ToDecimal(hddTotalPeso.Value) - Convert.ToDecimal(2.2);
                        decimal pagoextraEnvio = librasextra * Convert.ToDecimal(libras);
                        pagoServicioDomicilio = Math.Round(Convert.ToDouble(costo + +Convert.ToDouble(pagoextraEnvio)), 2);
                        respuesta = Math.Truncate((Convert.ToDouble(hddTotalPeso.Value) * taza) * 100) / 100;
                    }
                    else
                    {
                        pagoServicioDomicilio = Convert.ToDouble(5.6);

                        respuesta = Math.Truncate((Convert.ToDouble(hddTotalPeso.Value) * taza) * 100) / 100;
                    }
                    //pnlCargarPago.Visible = true;
                    lblValorEnvioDomicilio.Visible = true;
                    ddlMetodoPago.Items[1].Enabled = true;
                    ddlTipoPago_SelectedIndexChanged(null, null);
                    mdlPopup.Show();
                }
                else if (ddlTipoEnvio.SelectedValue == "RETIRAR EN OFICINA (GUAYAQUIL)")

                {
                    pagoServicioDomicilio = 0;
                    if (Convert.ToDouble(hddTotalPeso.Value) < 0.5)
                    {
                        if (rol == "3")
                        {

                            respuesta = valorMinCourier;
                        }
                        else { respuesta = valorMinCourierVip; }

                    }
                    else
                    {
                        respuesta = Math.Truncate((Convert.ToDouble(hddTotalPeso.Value) * taza) * 100) / 100; ;
                    }

                    if (rolUser != "5" && rolUser != "3")
                    {
                        // pnlCargarPago.Visible = true;
                        ddlMetodoPago.Items[1].Enabled = true;
                    }
                    ddlTipoPago_SelectedIndexChanged(null, null);
                    mdlPopup.Show();
                }
                else
                {

                    if (Convert.ToDouble(hddTotalPeso.Value) < 0.5)
                    {
                        if (rol == "3")
                        {

                            respuesta = valorMinCourier;
                        }
                        else { respuesta = valorMinCourierVip; }
                    }
                    else
                    {
                        respuesta = Math.Truncate((Convert.ToDouble(hddTotalPeso.Value) * taza) * 100) / 100;
                    }

                    // pnlCargarPago.Visible = true;

                    ddlTipoPago_SelectedIndexChanged(null, null);
                    mdlPopup.Show();
                }

                DataTable dp = Session["calculoEnvio"] as DataTable;
                dp.Clear();
                DataRow dr1 = dp.NewRow();
                DataRow dr3 = dp.NewRow();
                dr3[0] = 1.ToString();
                dr3[1] = "Servicio de Separación";
                dr3[2] = adicSeparar.ToString();
                dp.Rows.Add(dr3);
                dr1[0] = 2.ToString();
                dr1[1] = "Servicio Domicilio";
                dr1[2] = pagoServicioDomicilio.ToString();
                dp.Rows.Add(dr1);
                DataRow dr2 = dp.NewRow();
                dr2[0] = 3.ToString();
                dr2[1] = "Servicio Courier";
                dr2[2] = respuesta.ToString();
                dp.Rows.Add(dr2);
                

                hddTotalEnvio.Value = pagoServicioDomicilio.ToString();

                dtgCalculoEnvioB.DataSource = dp;
                dtgCalculoEnvioB.DataBind();
                
            }
            else
            { // Para categoria C

                respuesta = 0;
                dtgCalculoEnvioB.Visible = true;
                var rolVip = Session["rol"].ToString();
                if (rolVip == "5")
                {
                    // pnlCargarPago.Visible = true;
                    nacionalizacion = 30;
                }

                if (ddlTipoEnvio.SelectedValue == "ENVIO A DOMICILIO")
                {
                    lblValorEnvioDomicilio.Visible = true;
                    ddlMetodoPago.Items[1].Enabled = false;

                    respuesta = Math.Round(nacionalizacion + nacionalizacion * ivaVal, 2);
                    fleteInterna = Math.Round((Convert.ToDouble(hddTotalPeso.Value) * fleteInter), 2);

                    if (Math.Round(Convert.ToDouble(hddTotalPeso.Value), 2) > 2.2)
                    {
                        decimal librasextra = Convert.ToDecimal(hddTotalPeso.Value) - Convert.ToDecimal(2.2);
                        decimal pagoextraEnvio = librasextra * Convert.ToDecimal(0.5);
                        pagoServicioDomicilio = Math.Round(Convert.ToDouble(costo + Convert.ToDouble(pagoextraEnvio)), 2);


                    }
                    else
                    {


                        pagoServicioDomicilio = Math.Round(Convert.ToDouble(costo), 2);

                        //Elimine una fila

                    }
                    //pnlCargarPago.Visible = true;
                    ddlTipoPago_SelectedIndexChanged(null, null);

                    mdlPopup.Show();
                }
                else if (ddlTipoEnvio.SelectedValue == "ENVIO A DOMICILIO (RESTO DEL PAIS)")
                {
                    lblValorEnvioDomicilio.Visible = true;
                    Label17.Visible = true;
                    //pnlCargarPago.Visible = true;
                    ddlTipoPago_SelectedIndexChanged(null, null);
                    mdlPopup.Show();
                    if (Math.Round(Convert.ToDouble(hddTotalPeso.Value), 2) > 2.2)
                    {
                        decimal librasextra = Convert.ToDecimal(hddTotalPeso.Value) - Convert.ToDecimal(2.2);
                        decimal pagoextraEnvio = librasextra * Convert.ToDecimal(fleteInter);
                        pagoServicioDomicilio = Math.Round(Convert.ToDouble(costo + Convert.ToDouble(pagoextraEnvio)), 2);
                        respuesta = Math.Round(nacionalizacion + nacionalizacion * ivaVal, 2);
                        fleteInterna = Math.Round((Convert.ToDouble(hddTotalPeso.Value) * fleteInter), 2);
                    }
                    else
                    {
                        pagoServicioDomicilio = Convert.ToDouble(5.6);
                        respuesta = Math.Round(nacionalizacion + nacionalizacion * ivaVal, 2);
                        fleteInterna = Math.Round((Convert.ToDouble(hddTotalPeso.Value) * fleteInter), 2);
                    }
                    //pnlCargarPago.Visible = true;
                    lblValorEnvioDomicilio.Visible = true;
                    ddlMetodoPago.Items[1].Enabled = true;
                    ddlTipoPago_SelectedIndexChanged(null, null);
                    mdlPopup.Show();
                }
                else if (ddlTipoEnvio.SelectedValue == "RETIRAR EN OFICINA (GUAYAQUIL)")

                {
                    pagoServicioDomicilio = 0;

                    fleteInterna = Math.Round((Convert.ToDouble(hddTotalPeso.Value) * fleteInter), 2);
                    if (Convert.ToDouble(hddTotalPeso.Value) < 0.5)
                    {
                        if (rol == "3")
                        {

                            respuesta = valorMinCourier;
                        }
                        else { respuesta = valorMinCourierVip; }
                    }
                    else
                    {
                        respuesta = Math.Round(nacionalizacion + nacionalizacion * ivaVal, 2);

                    }


                    if (rol != "5" && rol != "3")
                    {
                        // pnlCargarPago.Visible = true;
                        ddlMetodoPago.Items[1].Enabled = true;
                    }
                    ddlTipoPago_SelectedIndexChanged(null, null);
                    mdlPopup.Show();
                }


                DataTable dp = Session["calculoEnvio"] as DataTable;
                dp.Clear();
                DataRow dr1 = dp.NewRow();
                DataRow dr2 = dp.NewRow();
                DataRow dr3 = dp.NewRow();
                DataRow dr4 = dp.NewRow();
                DataRow dr5 = dp.NewRow();
                dr3[0] = 1.ToString();
                dr3[1] = "Flete Internacional";
                dr3[2] = fleteInterna.ToString();
                dp.Rows.Add(dr3);
                dr1[0] = 2.ToString();
                dr1[1] = "Servicio Domicilio";
                dr1[2] = pagoServicioDomicilio.ToString();
                dp.Rows.Add(dr1);

                dr2[0] = 3.ToString();
                dr2[1] = "Honorarios Categoria C";
                dr2[2] = respuesta.ToString();
                dp.Rows.Add(dr2);

                dr4[0] = 4.ToString();
                dr4[1] = "Impuesto Aduana pagado por LOIDIMPSA";
                dr4[2] = hddTotalImp.Value;
                dp.Rows.Add(dr4);

                dr5[0] = 5.ToString();
                dr5[1] = "Gestión por Pago";
                dr5[2] = (hddTotalImp.Value != "0") ? "5" : "0";
                dp.Rows.Add(dr5);



                hddTotalEnvio.Value = pagoServicioDomicilio.ToString();

                dtgCalculoEnvioB.DataSource = dp;
                dtgCalculoEnvioB.DataBind();


                //pnlCargarPago.Visible = false;
                dtgCalculoEnvioB.Visible = true;
                mdlPopup.Show();
            }
        }



        //protected void cargarImagen_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fileUploader1.HasFile)
        //        {
        //            // Se verifica que la extensión sea de un formato válido
        //            string ext = fileUploader1.PostedFile.FileName;
        //            ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
        //            string[] formatos =
        //              new string[] { "jpg", "jpeg", "bmp", "png", "gif", "pdf" };
        //            if (Array.IndexOf(formatos, ext) < 0)
        //            {
        //                lblErrores.Text = "Formato de imagen inválido.";
        //                lblErrores.Visible = true;
        //                mdlPopup.Show();
        //            }
        //            else
        //            {
        //                GuardarArchivo(fileUploader1.PostedFile);
        //                mdlPopup.Show();
        //            }
        //        }
        //        else
        //        {
        //            lblErrores.Text = "Seleccione un archivo del disco duro.";
        //            lblErrores.Visible = true;
        //            mdlPopup.Show();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        #endregion

        //private void CargarAnios()
        //{
        //    var anioActual = Convert.ToString(DateTime.Now.Year);
        //    int seccAnio = Convert.ToInt32(anioActual.Substring(anioActual.Length - 2));

        //    for (int i = seccAnio; i < seccAnio + 10; i++)
        //    {
        //        tcanoexpiracion.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
        //    }

        //}
        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCargarDebito.Visible = true;

            mdlPopup.Show();

        }
        protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCargarDebito.Visible = true;
            mdlPopup.Show();

        }
        protected void ddlTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMetodoPago.SelectedValue == "TRANSFERENCIA/DEPOSITO")
            {
                pnlCargarPago.Visible = true;
                pnlCargarEfectivo.Visible = false;
                pnlCargarDebito.Visible = false;

                mdlPopup.Show();
            }
            else if (ddlMetodoPago.SelectedValue == "EFECTIVO")
            {
                pnlCargarPago.Visible = false;
                pnlCargarEfectivo.Visible = true;
                pnlCargarDebito.Visible = false;
                mdlPopup.Show();
            }
            else if (ddlMetodoPago.SelectedValue == "DEBITO/CREDITO")
            {
                //CargarAnios();
                pnlCargarPago.Visible = false;
                pnlCargarEfectivo.Visible = false;
                pnlCargarDebito.Visible = true;

                mdlPopup.Show();
            }

        }

        protected void ddlTipoBanco_SelectedIndexChanged(object sender, EventArgs e)
        {


            mdlPopup.Show();


        }

        protected void ddlTipoEnvioImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoPagoImp.SelectedValue == "PAGADO POR CLIENTE")
            {

                mdlPopup.Show();
            }
            else if (ddlTipoPagoImp.SelectedValue == "PAGADO POR LOIDIMPSA")
            {
                mdlPopup.Show();
            }


        }
        protected void dtgBodegaLoidimpsa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Session["rolUser"] == null)

                    {

                        Session["rolUser"] = "";

                    }
                    if (e.Row.Cells[12].Text != "0" && (Session["rolUser"].ToString() != "1" && Session["rolUser"].ToString() != "2"))
                    {

                        (e.Row.FindControl("chkEstado") as CheckBox).Enabled = false;
                        (e.Row.FindControl("chkEstado") as CheckBox).BackColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("chkEstado") as CheckBox).ToolTip = "Ya se ha realizado el checkout de esta guia.";
                    }
                    else if (Session["rolUser"].ToString() == "1" || Session["rolUser"].ToString() == "2")
                    {


                        (e.Row.FindControl("chkEstado") as CheckBox).BackColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("chkEstado") as CheckBox).ToolTip = "Ya se ha realizado el checkout de esta guia.";
                    }
                    else
                    {

                        (e.Row.FindControl("chkEstado") as CheckBox).Enabled = true;
                        (e.Row.FindControl("chkEstado") as CheckBox).BackColor = System.Drawing.Color.LightGreen;
                        (e.Row.FindControl("chkEstado") as CheckBox).ToolTip = "El envio esta listo para realizar el checkout.";
                    }
                }
                //&& (Session["rolUser"].ToString() == "1" && Session["rolUser"].ToString() == "2")
                // Muestr oculta botones
                if (dtgBodegaLoidimpsa.Rows.Count > 0)
                {
                    btnGenerarOrden.Visible = true;
                }
                else
                {
                    btnGenerarOrden.Visible = false;

                }


            }
            catch (Exception ex)
            {

            }
        }
        
        protected void dtgImp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    
                    // Guia Filtro aplica cuando el peso es mayor a 8.6 y el precio es mayor a 400, tercera decision propia al cambiar de categoria 
                    //|| Convert.ToDouble(e.Row.Cells[7])  
                    if (Convert.ToDouble(e.Row.Cells[5].Text) > 8.6 || Convert.ToDouble(e.Row.Cells[6].Text) > 400)
                    {
                        btnIngImp.Visible = true;

                        (e.Row.FindControl("chkImpuesto") as CheckBox).Visible = true;

                        hddActi.Value = "true";
                    }
                    else
                    {

                        (e.Row.FindControl("chkImpuesto") as CheckBox).Visible = false;
                    }

                    if (e.Row.Cells[5].Text != "0")
                    {

                        (e.Row.FindControl("chkImpuesto") as CheckBox).Enabled = true;

                        (e.Row.FindControl("chkImpuesto") as CheckBox).BackColor = System.Drawing.Color.Red;

                        (e.Row.FindControl("chkImpuesto") as CheckBox).ToolTip = "Ya se ha ingresado Impuesto de esta guia.";
                        btnIngImp.Visible = false;
                    }
                    else
                    {
                        btnIngImp.Visible = true;
                        (e.Row.FindControl("chkImpuesto") as CheckBox).Enabled = true;
                        (e.Row.FindControl("chkImpuesto") as CheckBox).BackColor = System.Drawing.Color.LightGreen;
                        (e.Row.FindControl("chkImpuesto") as CheckBox).ToolTip = "El envio esta listo para realizar el checkout.";
                    }
                    //(e.Row.FindControl("chkImpuesto") as CheckBox).Enabled = true;
                }

                // Muestr oculta botones
                if (dtgAduana.Rows.Count > 0 && hddActi.Value != "")
                {
                    btnIngImp.Visible = true;
                    dtgAduana.Columns[0].Visible = true;
                }
                else
                {
                    btnIngImp.Visible = false;
                    dtgAduana.Columns[0].Visible = false;
                }


            }
            catch (Exception ex)
            {

            }
        }

        protected void chkEstadoC_CheckedChanged(object sender, EventArgs e)
        {
            
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                int index = row.RowIndex;
                CheckBox cb1 = (CheckBox)dtgBodegaC.Rows[index].FindControl("chkEstadoC");
                if (cb1.Checked == true)
                {
                    DataTable dp = Session["enviosGestionC"] as DataTable;
                    DataRow dr1 = dp.NewRow();
                    dr1[0] = Server.HtmlDecode(dtgBodegaC.Rows[index].Cells[1].Text);
                    dr1[1] = Server.HtmlDecode(dtgBodegaC.Rows[index].Cells[2].Text);
                    dr1[2] = Server.HtmlDecode(dtgBodegaC.Rows[index].Cells[7].Text);
                    dr1[3] = Server.HtmlDecode(dtgBodegaC.Rows[index].Cells[8].Text);
                    dr1[4] = Server.HtmlDecode(dtgBodegaC.Rows[index].Cells[14].Text);
                    dr1[5] = Server.HtmlDecode(dtgBodegaC.Rows[index].Cells[13].Text);
                    dr1[6] = Server.HtmlDecode(dtgBodegaC.Rows[index].Cells[15].Text);
                    dp.Rows.Add(dr1);

                    Session["enviosGestionC"] = dp;
                }
                else if (cb1.Checked == false)
                {
                    DataTable dp = Session["enviosGestionC"] as DataTable;
                    for (int i = dp.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dp.Rows[i];
                        if (((dr["numeroOrdenInterno"]).ToString() == Server.HtmlDecode(dtgBodegaC.Rows[index].Cells[1].Text)))
                        {
                            dr.Delete();
                        }
                    }
                    Session["enviosGestionC"] = dp;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void dtgBodegaC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                var rol = Session["rol"].ToString();
                if (Session["rolUser"] == null)

                {

                    Session["rolUser"] = "";

                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[12].Text != "0" && (Session["rolUser"].ToString() != "1" && Session["rolUser"].ToString() != "2"))
                    {
                        (e.Row.FindControl("chkEstadoC") as CheckBox).Enabled = false;
                        (e.Row.FindControl("chkEstadoC") as CheckBox).BackColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("chkEstadoC") as CheckBox).ToolTip = "Ya se ha realizado el checkout de esta guia.";
                    }
                    else if (Session["rolUser"].ToString() == "1" || Session["rolUser"].ToString() == "2")
                    {


                        (e.Row.FindControl("chkEstadoC") as CheckBox).BackColor = System.Drawing.Color.Red;
                        (e.Row.FindControl("chkEstadoC") as CheckBox).ToolTip = "Ya se ha realizado el checkout de esta guia.";
                    }
                    else
                    {
                        (e.Row.FindControl("chkEstadoC") as CheckBox).Enabled = true;
                        (e.Row.FindControl("chkEstadoC") as CheckBox).BackColor = System.Drawing.Color.LightGreen;
                        (e.Row.FindControl("chkEstadoC") as CheckBox).ToolTip = "El envio esta listo para realizar el checkout.";
                    }
                }
                if (dtgBodegaC.Rows.Count > 0)
                {
                    btnGenerarOrdenC.Visible = true;

                }
                else
                {
                    btnGenerarOrdenC.Visible = false;

                }
            }
            catch (Exception ex)
            {

            }
        }

        decimal totalEnviosCosto;
       
        protected void dtgCalculoEnvioB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    totalEnviosCosto += Convert.ToDecimal(e.Row.Cells[2].Text.Replace("$ ", "").Replace("$", ""));
                 
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[1].Text = " Valor Total: ";
                    e.Row.Cells[2].Text = totalEnviosCosto.ToString();
                    lblValorEnvioDomicilio.Text = totalEnviosCosto.ToString();
                    lblValorEnvioDomicilioDesc.Text = totalEnviosCosto.ToString();
                    lblValorEnvioDomicilio.Visible = true;
                }

            }

            catch (Exception ex)
            {

            }
        }
        // Pagar Debito o Credito
        protected async void btnPagar_ClickAsync(object sender, EventArgs e)
        {

            //Declarar funcones task
            var linkGenerar = OpenLink();

            var listTasks = new List<Task> { linkGenerar };

            while (listTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(listTasks);
                if (finishedTask == linkGenerar)
                {
                    Console.WriteLine("Ya se genero el link");
                }

                listTasks.Remove(finishedTask);
            }


            mdlPopup.Show();



        }



        protected void btnVerificar_Click(object sender, EventArgs e)
        {



            var idOrden = "";
            foreach (GridViewRow gr in dtgListadoEnviosGestionar.Rows)
            {
                idOrden += gr.Cells[0].Text + ".";
            }

            var cdl = "";
            if (Session["rol"] == "3" || Session["rol"] == "5")
            {
                cdl = hddIdentificacion.Value;
            }
            else
            {
                cdl = lBlCed.Text;
            }





            var codigoPers = cdl + idOrden;
            var estado = "aprobada";


            var lista = new GeneralesN().BuscarRegistroPago(ConfigurationManager.AppSettings["cnnSQL"], cdl, idOrden, estado);
            var listS = lista.ToString();
            if (lista.Count >= 1)
            {
                var cid = lista[0].cid.ToString();
                var idorden = lista[0].idorden.ToString();
                var fecha = lista[0].fechaRegistro.ToString().Replace("/", "-").Replace(":", ".");
                string path = AppDomain.CurrentDomain.BaseDirectory + cid + "-" + idorden + "-" + fecha;
                if (!File.Exists(path))
                {
                    hddVerificar.Value = "confirmado";
                }
                ddlTipoEnvio.Enabled = false;
                ddlMetodoPago.Enabled = false;
                btnPagoTarjeta.Visible = false;
                btnVerificaPago.Visible = false;
                imgConfir.ImageUrl = "~/images/card-check.png";
                imgConfir.Width = Unit.Percentage(57);
                lblConfirm.ForeColor = Color.Green;
                lblConfirm.Text = "PAGO CORRECTO";
                lblMessage.Text = "Su pago ha sido confirmado con exito, puede generar la Orden";

            }
            else
            {
                imgConfir.ImageUrl = "~/images/no-card.png";

                lblConfirm.Text = "PAGO INCORRECTO";
                lblMessage.Text = "Su pago no se ha realizado con exito, vuelva a generar el LINK DE PAGO y verifique los fondos de la Tarjeta o que los datos del propietario sean correctos";

            }

            mostrarCargar();
            mdlPopup.Show();



        }
        protected async Task<bool> OpenLink()
        {
            generalesN = new GeneralesN();
            var payyData = generalesN.Pay();
            var totalPagar = Double.Parse(lblValorEnvioDomicilio.Text);
            var baseIva = Math.Round((totalPagar / 1.12), 2);
            var Iva = Convert.ToString(Math.Round((baseIva * 0.12), 2));
            var idOrden = "";
            foreach (GridViewRow gr in dtgListadoEnviosGestionar.Rows)
            {
                idOrden += gr.Cells[0].Text + ".";
            }

            var cdl = "";
            if (Session["rol"] == "3" || Session["rol"] == "5")
            {
                cdl = hddIdentificacion.Value;
            }
            else
            {
                cdl = lBlCed.Text;
            }

            var values = new Dictionary<string, string>
                {
                    { "usuario", payyData["usuario"] },
                    { "llavemd5", payyData["llavemd5"]  },
                    { "urlback", HttpContext.Current.Server.UrlEncode(payyData["urlback"]+"?"+"cid="+cdl+"&idorden="+idOrden) },
                    { "referencia", "LOIDIMPSA"+idOrden},
                    { "moneda", "USD" },
                    { "valor",  lblValorEnvioDomicilio.Text.Replace(",",".") },
                    { "iva", Iva.Replace(",",".")  },
                    { "baseiva", Convert.ToString(baseIva).Replace(",",".")  },
                    { "descripcion", "PAGO A LOIDIMPSA de la orden "+ idOrden }


                };

            var content = new FormUrlEncodedContent(values);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var response = await client.PostAsync("https://plataforma.medianetpay.ec/secure/gangway/index.do?", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var rsp = (responseString.IndexOf("error") == -1) ? false : true;
                if (rsp)
                {
                    lblConfirm.Visible = true;
                    lblConfirm.Text = "SOLICITUD RECHAZADA POR MEDIANET";

                    return false;


                }
                else
                {
                    string[] subs = responseString.Split('\"');

                    string link = subs[3];

                    string s = "window.open('" + link + "', '_new_tab');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", s, true);

                    //var task = new Task(() => {
                    //    string s = "window.open('" + link + "', '_new_tab');";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", s, true);


                    //});
                    //task.Start();
                    //await task;

                    btnVerificaPago.Visible = true;
                    return true;
                }

            }
            else
            {
                return false;
            }
        }


        protected void mostrarCargar()
        {

            pnlCargando.Visible = true;
        }


        //Para la parte de factura autorizar
        protected void btnCargarFileResolutor2_Click(object sender, EventArgs e)
        {

            try
            {
                lbArchExiste2.Visible = false;

                if (!string.IsNullOrEmpty(examinarAdjuntoResolutorB.FileName))
                {
                    ArchivosAdjuntoE adj = new ArchivosAdjuntoE();

                    bool exist = false;

                    foreach (var item in (List<ArchivosAdjuntoE>)Session["listAdjuntos"])
                    {
                        if (item.Nom_Arc == examinarAdjuntoResolutorB.FileName)
                        {
                            exist = true;
                        }
                    }

                    if (!exist)
                    {
                        CrearCarpetas();
                        string extension = Path.GetExtension(examinarAdjuntoResolutorB.PostedFile.FileName);
                        adj.Nom_Arc = hddNombreArchivoFactura.Value;

                        adj.Ext = examinarAdjuntoResolutorB.FileName.Split('.')[examinarAdjuntoResolutorB.FileName.Split('.').Length - 1];
                        adj.Archivo = examinarAdjuntoResolutorB.FileBytes;
                        adj.nroOrden = hddIdOrden.Value;
                        adj.identificacionCliente = hddIdentificacion.Value;
                        ((List<ArchivosAdjuntoE>)Session["listAdjuntos"]).Add(adj);
                        gvArchivosResolutor2.DataSource = ((List<ArchivosAdjuntoE>)Session["listAdjuntos"]);
                        gvArchivosResolutor2.DataBind();
                    }
                    else if (exist)
                    {
                        lbArchExiste2.Visible = true;

                    }
                }
                if (gvArchivosResolutor2.Rows.Count > 0)
                {
                    examinarAdjuntoResolutorB.Enabled = false;
                }
                else
                {
                    examinarAdjuntoResolutorB.Enabled = true;
                }
                mdlPopup.Show();
            }
            catch (Exception ex)

            {
                String script = "alert('Error al cargar el archivo - Consulte con el Administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
            }
        }

        protected void btnCargarFileResolutor_Click(object sender, EventArgs e)
        {

            try
            {
                lbArchExiste.Visible = false;

                if (!string.IsNullOrEmpty(examinarAdjuntoResolutor.FileName))
                {
                    ArchivosAdjuntoE adj = new ArchivosAdjuntoE();

                    bool exist = false;



                    foreach (var item in (List<ArchivosAdjuntoE>)Session["listAdjuntos"])
                    {
                        if (item.Nom_Arc == examinarAdjuntoResolutor.FileName)
                        {
                            exist = true;
                        }
                    }

                    if (!exist)
                    {
                        CrearCarpetas();
                        string extension = Path.GetExtension(examinarAdjuntoResolutor.PostedFile.FileName);
                        adj.Nom_Arc = hddNombreArchivoFactura.Value;

                        adj.Ext = examinarAdjuntoResolutor.FileName.Split('.')[examinarAdjuntoResolutor.FileName.Split('.').Length - 1];
                        adj.Archivo = examinarAdjuntoResolutor.FileBytes;
                        adj.nroOrden = hddIdOrden.Value;
                        adj.identificacionCliente = hddIdentificacion.Value;
                        ((List<ArchivosAdjuntoE>)Session["listAdjuntos"]).Add(adj);
                        gvArchivosResolutor.DataSource = ((List<ArchivosAdjuntoE>)Session["listAdjuntos"]);
                        gvArchivosResolutor.DataBind();
                    }
                    else if (exist)
                    {
                        lbArchExiste.Visible = true;

                    }
                }
                if (gvArchivosResolutor.Rows.Count > 0)
                {
                    examinarAdjuntoResolutor.Enabled = false;
                }
                else
                {
                    examinarAdjuntoResolutor.Enabled = true;
                }
                mdlPopup.Show();
            }
            catch (Exception ex)

            {
                String script = "alert('Error al cargar el archivo - Consulte con el Administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
            }
        }
        public void CrearCarpetas()
        {
            DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string fechaFile = fecha.ToString("yyyy-MM-dd");


            string ruta = string.Empty;

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                string extension = Path.GetExtension(file.FileName);
                string fname = "FACTURA-" + hddIdentificacion.Value + "-" + hddIdOrden.Value + "-" + fechaFile;
                ruta = (@"C:/FACTURASCLIENTES/" + hddIdentificacion.Value + "/" + hddIdOrden.Value + "/").ToString();
                var rutaS1 = (@"C:/FACTURASCLIENTES/" + hddIdentificacion.Value + "/" + hddIdOrden.Value).ToString();

                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                string[] dirs = Directory.GetFiles(rutaS1, fname + ".*");
                int cantidad = dirs.Length;
                if (cantidad == 0)
                {
                    string filePath = Path.Combine(ruta, fname + extension);
                    hddNombreArchivoFactura.Value = fname + extension;
                    file.SaveAs(filePath);
                }
                else
                {


                    fname = "FACTURA-" + hddIdentificacion.Value + "-" + hddIdOrden.Value + "-" + fechaFile + "(" + Convert.ToString(cantidad + 1) + ")" + extension;
                    hddNombreArchivoFactura.Value = fname;
                    string filePath = Path.Combine(ruta, fname);
                    file.SaveAs(filePath);

                }
            }

        }

        public void CrearCarpetasPago()
        {
            DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string fechaFile = fecha.ToString("yyyy-MM-dd");
            string ruta = string.Empty;
            string rutaS = string.Empty;

            if (Request.Files.Count > 0)
            {
                var idOrden = "";
                foreach (GridViewRow gr in dtgListadoEnviosGestionar.Rows)
                {
                    idOrden += gr.Cells[0].Text + "-";
                }
                var file = Request.Files[0];
                string extension = Path.GetExtension(file.FileName);
                string fname = "PAGO-" + hddIdentificacion.Value + "-" + idOrden + fechaFile;
                var rutaS1 = (@"C:/PAGO/" + hddIdentificacion.Value).ToString();
                ruta = (@"C:/PAGO/" + hddIdentificacion.Value + "/").ToString();


                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                string[] dirs = Directory.GetFiles(rutaS1, fname + ".*");
                int cantidad = dirs.Length;
                if (cantidad == 0)
                {
                    string filePath = Path.Combine(ruta, fname + extension);

                    file.SaveAs(filePath);
                    hddNombreArchivo.Value = fname + extension;
                }
                else
                {


                    fname = "PAGO-" + hddIdentificacion.Value + "-" + hddIdOrden.Value + "-" + fechaFile + "(" + Convert.ToString(cantidad + 1) + ")" + extension;
                    hddNombreArchivo.Value = fname;
                    string filePath = Path.Combine(ruta, fname);
                    file.SaveAs(filePath);

                }

            }

        }

        protected void gvArchivosResolutor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow grAdjunto = gvArchivosResolutor.Rows[e.RowIndex];

            string ruta = string.Empty;
            try
            {
                foreach (var item in (List<ArchivosAdjuntoE>)Session["listAdjuntos"])
                {
                    if (item.Nom_Arc == Server.HtmlDecode(grAdjunto.Cells[1].Text.Trim()))
                    {
                        ruta = (@"C:/FACTURASCLIENTES/" + hddIdentificacion.Value + "/" + hddIdOrden.Value + "/").ToString();
                        foreach (var obj in Directory.GetFiles(ruta, grAdjunto.Cells[1].Text.Trim()))
                        {
                            File.SetAttributes(obj, FileAttributes.Normal);
                            File.Delete(obj);
                        }
                        ((List<ArchivosAdjuntoE>)Session["listAdjuntos"]).RemoveAt(e.RowIndex);
                        break;
                    }

                }
                gvArchivosResolutor.DataSource = (List<ArchivosAdjuntoE>)Session["listAdjuntos"];
                gvArchivosResolutor.DataBind();
                if (gvArchivosResolutor.Rows.Count > 0)
                {
                    examinarAdjuntoResolutor.Enabled = false;

                }
                else
                {
                    examinarAdjuntoResolutor.Enabled = true;
                }
                mdlPopup.Show();
            }
            catch (Exception ex)
            {
                String script = "alert('Error al eliminar el archivo - Consulte con el Administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
            }


        }


        protected void gvArchivosResolutor2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow grAdjunto = gvArchivosResolutor2.Rows[e.RowIndex];

            string ruta = string.Empty;
            try
            {
                foreach (var item in (List<ArchivosAdjuntoE>)Session["listAdjuntos"])
                {
                    if (item.Nom_Arc == Server.HtmlDecode(grAdjunto.Cells[1].Text.Trim()))
                    {
                        ruta = (@"C:/FACTURASCLIENTES/" + hddIdentificacion.Value + "/" + hddIdOrden.Value + "/").ToString();
                        foreach (var obj in Directory.GetFiles(ruta, grAdjunto.Cells[1].Text.Trim()))
                        {
                            File.SetAttributes(obj, FileAttributes.Normal);
                            File.Delete(obj);
                        }
                        ((List<ArchivosAdjuntoE>)Session["listAdjuntos"]).RemoveAt(e.RowIndex);
                        break;
                    }

                }
                gvArchivosResolutor2.DataSource = (List<ArchivosAdjuntoE>)Session["listAdjuntos"];
                gvArchivosResolutor2.DataBind();
                if (gvArchivosResolutor2.Rows.Count > 0)
                {
                    examinarAdjuntoResolutorB.Enabled = false;

                }
                else
                {
                    examinarAdjuntoResolutorB.Enabled = true;
                }
                mdlPopup.Show();
            }
            catch (Exception ex)
            {
                String script = "alert('Error al eliminar el archivo - Consulte con el Administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
            }


        }
        protected void gvArchivosResolutor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gr = gvArchivosResolutor.SelectedRow;
                string nameFile = Server.HtmlDecode(gr.Cells[1].Text.Trim());
                nameFile = Regex.Replace(nameFile, @"\s+", "_");
                byte[] documento = ((List<ArchivosAdjuntoE>)Session["listAdjuntos"])[gr.RowIndex].Archivo;
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + nameFile);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(documento);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }


            catch (Exception ex)
            { }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                lbArchExiste.Visible = false;

                if (!string.IsNullOrEmpty(fupCargarPago.FileName))
                {
                    ArchivosAdjuntoE adj = new ArchivosAdjuntoE();

                    bool exist = false;

                    foreach (var item in (List<ArchivosAdjuntoE>)Session["listAdjuntos"])
                    {
                        if (item.Nom_Arc == fupCargarPago.FileName)
                        {
                            exist = true;
                        }
                    }

                    if (!exist)
                    {
                        CrearCarpetasPago();
                        string extension = Path.GetExtension(fupCargarPago.PostedFile.FileName);
                        adj.Nom_Arc = hddNombreArchivo.Value;

                        adj.Ext = fupCargarPago.FileName.Split('.')[fupCargarPago.FileName.Split('.').Length - 1];
                        adj.Archivo = fupCargarPago.FileBytes;
                        adj.nroOrden = hddIdOrden.Value;
                        adj.identificacionCliente = hddIdentificacion.Value;
                        ((List<ArchivosAdjuntoE>)Session["listAdjuntos"]).Add(adj);
                        dtgPago.DataSource = ((List<ArchivosAdjuntoE>)Session["listAdjuntos"]);
                        dtgPago.DataBind();
                    }
                    else if (exist)
                    {
                        lblPagoExiste.Visible = true;

                    }
                }
                if (dtgPago.Rows.Count > 0)
                {
                    fupCargarPago.Enabled = false;
                }
                else
                {
                    fupCargarPago.Enabled = true;
                }
                mdlPopup.Show();
            }
            catch (Exception ex)

            {
                String script = "alert('Error al cargar el archivo - Consulte con el Administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
            }
        }

        protected void dtgPago_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow grAdjunto = dtgPago.Rows[e.RowIndex];

            string ruta = string.Empty;
            try
            {
                foreach (var item in (List<ArchivosAdjuntoE>)Session["listAdjuntos"])
                {
                    if (item.Nom_Arc == Server.HtmlDecode(grAdjunto.Cells[1].Text.Trim()))
                    {
                        string fileName = grAdjunto.Cells[1].Text.Trim();
                        ruta = (@"C:/PAGO/" + hddIdentificacion.Value + "/").ToString();
                        foreach (var obj in Directory.GetFiles(ruta, fileName))
                        {
                            File.SetAttributes(obj, FileAttributes.Normal);
                            File.Delete(obj);
                        }
                        ((List<ArchivosAdjuntoE>)Session["listAdjuntos"]).RemoveAt(e.RowIndex);
                        break;
                    }

                }
                dtgPago.DataSource = (List<ArchivosAdjuntoE>)Session["listAdjuntos"];
                dtgPago.DataBind();
                if (dtgPago.Rows.Count > 0)
                {
                    fupCargarPago.Enabled = false;
                }
                else
                {
                    fupCargarPago.Enabled = true;
                }
                mdlPopup.Show();
            }
            catch (Exception ex)
            {
                String script = "alert('Error al eliminar el archivo - Consulte con el Administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
            }

        }

        protected void dtgPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gr = dtgPago.SelectedRow;
                string nameFile = Server.HtmlDecode(gr.Cells[1].Text.Trim());
                nameFile = Regex.Replace(nameFile, @"\s+", "_");
                byte[] documento = ((List<ArchivosAdjuntoE>)Session["listAdjuntos"])[gr.RowIndex].Archivo;
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + nameFile);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(documento);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }


            catch (Exception ex)
            { }
        }


        protected void txtValor_TextChanged(object sender, EventArgs e)
        {
            var dovalor = Convert.ToDouble(txtValor.Text);
            if (dovalor > 400)
            {
                String script = "confirm('Usted ha ingresado el siguiente valor $" + dovalor.ToString() + "¿Es correcto?');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
            }
        }

        
        protected void CargarCupones()
        {
            trackingsN = new TrackingsN();
            var cupos = trackingsN.EstadoCuponesUser(hddIdentificacion.Value, DropDownList1.Text, numOr.ToString());

            if (cupos == 0)
            {
                trackingsN = new TrackingsN();
                var cups = trackingsN.EstadoCuponesProm();
                
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add(new ListItem("Seleccione cupon", " "));
                foreach (var item in cups)
                {
                    DropDownList1.Items.Add(new ListItem(item.Value, item.Key.ToString()));
                }
                
            }
            else
            {
                DropDownList1.Items.Add(new ListItem("No Aplica Cupon", " "));
            }
            
            
        }
        
        

 
        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            
            if (DropDownList1.SelectedValue.ToString() != " ")
            {
                var valorCup = Convert.ToDouble(DropDownList1.SelectedValue);
                var valorPagar = Convert.ToDouble(lblValorEnvioDomicilioDesc.Text);
                double valdesc = Math.Round(valorPagar - (valorCup * valorPagar / 100),2)  ;
                lblValorEnvioDomicilioDesc.Text = valdesc.ToString();                
            }else
            {
                lblValorEnvioDomicilioDesc.Text = lblValorEnvioDomicilio.Text;

            }
            mdlPopup.Show();
        }
    }
}