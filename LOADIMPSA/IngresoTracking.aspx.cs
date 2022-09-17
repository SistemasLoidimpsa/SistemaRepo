using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Configuration;
using System.Text.RegularExpressions;

namespace LOADIMPSA
{

    public partial class IngresoTracking : System.Web.UI.Page
    {
        ClienteN clienteN;
        TransportistasN transportistasN;
        EmpleadoN empleadoN;
        string script;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    DateTime fechaHoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    txtFechaRecibidoMiami.Text = fechaHoy.ToString("yyyy-MM-dd");

                    CargarTransportistas();
                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        Session["cod_usu"] = item.cod_usu;

                    }
                    Session["listAdjuntos"] = new List<ArchivosAdjuntoE>();
                    Session["listAdjuntos"] = new List<ArchivosAdjuntoE>();
                }
            }
        }



        #region Cargar Datos

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
            pnlClientesTracking.Visible = true;

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


        private void CargarTransportistas()
        {
            ddlTransportista.Items.Clear();
            transportistasN = new TransportistasN();

            var carreras = transportistasN.Transportistas();
            foreach (var item in carreras)
            {
                ddlTransportista.Items.Add(new ListItem(item.Key, item.Value.ToString()));
            }
            ddlTransportista.Items.Add(new ListItem("Nuevo", "-1"));

        }


        #endregion

        protected void btnIngresarOrden_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtOrdenInterno.Text))

            {
                if (Convert.ToInt32(txtOrdenInterno.Text) <= 1999999999)
                {
                    if (!String.IsNullOrEmpty(txtTracking.Text))
                    {
                        if (!String.IsNullOrEmpty(txtPeso.Text))
                        {
                            if (!String.IsNullOrEmpty(txtFechaRecibidoMiami.Text))
                            { 
                                if (!String.IsNullOrEmpty(txtDescripcion.Text))
                                { 

                                    string strTransportista = ddlTransportista.SelectedValue == "-1" ? txtOtroTransportista.Text : ddlTransportista.SelectedItem.Text;

                                    empleadoN = new EmpleadoN();
                                    if (empleadoN.InsertaOrdenInterna(Convert.ToInt32(txtOrdenInterno.Text), txtTracking.Text,
                                        lBlCed.Text, Convert.ToInt32(ddlTransportista.SelectedValue),
                                        strTransportista,
                                        Convert.ToDecimal(txtPeso.Text), Convert.ToDateTime(txtFechaRecibidoMiami.Text),
                                        txtDescripcion.Text,
                                        txtObservaciones.Text,
                                        Session["cod_usu"].ToString(), hddNombreArchivoFactura.Value, Convert.ToInt32(ddlsepararPaquete.SelectedValue), ddlBodega.SelectedValue))
                                    {
                                        script = "alert('La Orden: " + txtOrdenInterno.Text + " del cliente: " + lblNombresCom.Text + " se ha registrado correctamente.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                        LimpiarCajas();
                                    }
                                    else
                                    {
                                        script = "alert('La Orden: " + txtOrdenInterno.Text + " ya se ha registrado, revise la información.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                    }
                                }
                                else
                                {
                                    script = "alert('La descripción del Envio es obligatoria.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                    txtDescripcion.Focus();
                                }
                            }
                            else
                            {

                                script = "alert('La Fecha de Ingreso a la Bodega en Miami es obligatoria');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                txtFechaRecibidoMiami.Focus();
                            }
                        }
                        else
                        {

                            script = "alert('El peso de la Orden es obligatorio.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            txtPeso.Focus();
                        }

                    }
                    else
                    {
                        script = "alert('El numero de Tracking es obligatorio.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                        txtTracking.Focus();
                    }
                }
                else
                {
                    script = "alert('El numero es demasiado grande, no soporta mas de 10 digitos');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtTracking.Focus();
                }
            }
            else
            {
                script = "alert('El numero de Orden es obligatorio.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                txtOrdenInterno.Focus();

            }
        }


        public void LimpiarCajas()
        {
            txtOrdenInterno.Text = string.Empty;
            txtTracking.Text = string.Empty;
            txtPeso.Text = string.Empty;
            DateTime fechaHoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            txtDescripcion.Text = string.Empty;
            txtFechaRecibidoMiami.Text = fechaHoy.ToString("yyyy-MM-dd");
            txtObservaciones.Text = string.Empty;
            txtOtroTransportista.Text = string.Empty;
            CargarTransportistas();
            pnlClientesTracking.Visible = false;
            pnlDatosEstudiante.Visible = false;
            pnlClientes.Visible = false;
        }

        protected void ddlTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransportista.SelectedValue == "-1")
            {
                txtOtroTransportista.Visible = true;
            }
            else
            {
                txtOtroTransportista.Visible = false;
                txtOtroTransportista.Text = string.Empty;
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
                        ruta = (@"C:/FACTURASCLIENTES/" + lBlCed.Text + "/" + txtOrdenInterno.Text + "/").ToString();
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
        protected void ddlseparar_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        protected void btnCargarFileResolutor_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOrdenInterno.Text))
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
                            adj.Nom_Arc = Path.GetFileName(examinarAdjuntoResolutor.PostedFile.FileName);
                            hddNombreArchivoFactura.Value = Path.GetFileName(examinarAdjuntoResolutor.PostedFile.FileName);
                            adj.Ext = examinarAdjuntoResolutor.FileName.Split('.')[examinarAdjuntoResolutor.FileName.Split('.').Length - 1];
                            adj.Archivo = examinarAdjuntoResolutor.FileBytes;
                            adj.nroOrden = txtOrdenInterno.Text;
                            adj.identificacionCliente = lBlCed.Text;
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
                  
                }
                catch (Exception ex)

                {
                    String script = "alert('Error al cargar el archivo - Consulte con el Administrador');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", script, true);
                }
            }
            else
            {
                script = "alert('El numero de Orden es obligatorio para cargar el archivo.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                txtOrdenInterno.Focus();
            }
        }
        public void CrearCarpetas()
        {

            string ruta = string.Empty;

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                string fname = System.IO.Path.GetFileName(file.FileName);
                ruta = (@"C:/IMGTRACKING/" + lBlCed.Text + "/" + txtOrdenInterno.Text + "/").ToString();
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }
                string filePath = Path.Combine(ruta, fname);
                file.SaveAs(filePath);
            }

        }

    }
}