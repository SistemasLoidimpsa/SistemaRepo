using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using Entidades;
namespace LOADIMPSA
{
    public partial class Productos : System.Web.UI.Page
    {
        TrackingsN trackingsN;
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
                    PanelIngreso.Visible = false;
                    PanelConsulta.Visible = false;
                    // cargar Fechas en Textbox
                    DateTime fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                    dllProductEstado.SelectedValue = "1";


                    List<UsuarioE> usuE = (List<UsuarioE>)Session["usuario"];
                    foreach (var item in usuE)
                    {
                        Session["cod_usu"] = item.cod_usu;

                    }
                    Session["listAdjuntos"] = new List<ArchivosAdjuntoE>();
                }
                else
                {
                    Response.Redirect("ServicioTicket.aspx");
                }
            }
        }

        //Cargar panel
        protected void radio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //do whatever you want by calling the name of the radio id
            //example

            if (panelId.SelectedItem.Value == "0")
            {

                PanelIngreso.Visible = true;
                pnlClientesTracking.Visible = true;
                PanelConsulta.Visible = false;


            }
            else if (panelId.SelectedItem.Value == "1")
            {
                LimpiarCajas();
                PanelIngreso.Visible = false;
                PanelConsulta.Visible = true;
                btnBuscar_ClickC(null, null);

            }

        }

        #region Cargar Datos









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
                        string[] nums = Path.GetFileName(examinarAdjuntoResolutor.PostedFile.FileName).Split(' ');
                        var totalElements = nums.Count();
                        if (totalElements > 1)
                        {
                            hddNombreArchivo.Value = Path.GetFileName(examinarAdjuntoResolutor.PostedFile.FileName).Split(' ')[0] + "." + examinarAdjuntoResolutor.FileName.Split('.')[examinarAdjuntoResolutor.FileName.Split('.').Length - 1];
                        }else
                        {
                            hddNombreArchivo.Value = Path.GetFileName(examinarAdjuntoResolutor.PostedFile.FileName);
                        }
                        CrearCarpetas();
                        adj.Nom_Arc = hddNombreArchivo.Value;

                        adj.Ext = examinarAdjuntoResolutor.FileName.Split('.')[examinarAdjuntoResolutor.FileName.Split('.').Length - 1];
                        adj.Archivo = examinarAdjuntoResolutor.FileBytes;

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
        public void CrearCarpetas()
        {

            string ruta = string.Empty;

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                string fname = System.IO.Path.GetFileName(file.FileName);
                ruta = AppDomain.CurrentDomain.BaseDirectory + "/images/" + "/recompensa/"; 
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }
                string filePath = Path.Combine(ruta, fname);
                file.SaveAs(filePath);
            }

        }


        #endregion
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
                        ruta = (@"C:/PRODUCTOS/" + hddNombreArchivo.Value).ToString();
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

        protected void btnIngresarProducto_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNombreProdu.Text))

            {
                if (!String.IsNullOrEmpty(txtCantidaProdu.Text))
                {


                    if (!String.IsNullOrEmpty(txtPuntos.Text))
                    {

                        if (!String.IsNullOrEmpty(hddNombreArchivo.Value))
                        {



                            empleadoN = new EmpleadoN();
                            if (empleadoN.InsertaProducto(txtNombreProdu.Text,
                              Convert.ToInt32(txtCantidaProdu.Text), Convert.ToInt32(txtPuntos.Text),"images/recompensa/"+ hddNombreArchivo.Value))
                            {
                                script = "alert('El Producto se ha registrado correctamente.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                LimpiarCajas();
                            }
                            else
                            {
                                script = "alert('El prdocuto ya se ha registrado, revise la información.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            }
                        }
                        else
                        {
                            script = "alert('La imagen del Producto es obligatoria.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            txtPuntos.Focus();
                        }

                    }
                    else
                    {
                        script = "alert('La puntuacion del Producto es obligatoria.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                        txtPuntos.Focus();
                    }





                }
                else
                {
                    script = "alert('El numero de Productos disponibles es obligatorio.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtCantidaProdu.Focus();
                }

            }
            else
            {
                script = "alert('El nombre del Producto no se ha ingresado.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                txtNombreProdu.Focus();

            }
        }


        public void LimpiarCajas()
         {
            txtNombreProdu.Text = string.Empty;
            txtCantidaProdu.Text = string.Empty;
            txtPuntos.Text = string.Empty;
            ((List<ArchivosAdjuntoE>)Session["listAdjuntos"]).Clear();
            gvArchivosResolutor.DataSource = (List<ArchivosAdjuntoE>)Session["listAdjuntos"];
            gvArchivosResolutor.DataBind();
            examinarAdjuntoResolutor.Enabled = true;

            pnlClientesTracking.Visible = false;



        }

        public void LimpiarCajasConsulta()
        {
            ddlEstadoActi.SelectedIndex = 0;
            lbltitulo2.Text = string.Empty;
            txtName.Text = string.Empty;
            txtCount.Text = string.Empty;
            txtPtn.Text = string.Empty;
        }



        protected void txtOtroTransportista_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtOrdenInterno_TextChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mdlPopup.Show();
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }


        //Seccion de Consulta





        public void CargarListadoProduc(string estadoProduc)
        {
            try
            {
                trackingsN = new TrackingsN();
                var list = trackingsN.ListadoProductos(ConfigurationManager.AppSettings["cnnSQL"], Convert.ToInt32(estadoProduc));
                dtgProductos.DataSource = list;
                dtgProductos.DataBind();
            }
            catch (Exception ex)
            {

            }

        }

        protected void dtgTicketDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void dtgTicket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            trackingsN = new TrackingsN();

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView dtgDetalle = (e.Row.FindControl("dtgTicketDetalle") as GridView);
                    String idTicket = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "idTicket"));
                    var dat = new TrackingsN().ListadoTicketDetalle(ConfigurationManager.AppSettings["cnnSQL"], idTicket);
                    dtgDetalle.DataSource = dat;
                    dtgDetalle.DataBind();
                    dtgDetalle.Visible = true;

                    if (e.Row.Cells[9].Text == "RESUELTO")
                    {
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).Enabled = false;
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).ImageUrl = "~/images/checki.png";
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).ToolTip = "EL envio se encuentra finalizado.";

                    }
                    else
                    {
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).Enabled = true;
                        (e.Row.FindControl("btnRegistroCheckOut") as ImageButton).ToolTip = "Click para Resolver el Ticket.";
                    }
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


        protected void btnBuscar_ClickC(object sender, EventArgs e)
        {
            pnlListado.Visible = true;
            if (!String.IsNullOrEmpty(dllProductEstado.SelectedValue))
            {

                CargarListadoProduc(dllProductEstado.SelectedValue);
            }
            else
            {
                script = "alert('La fecha fin es obligatoria para buscar.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
            }

        }

        protected void btnBorrarTicket_Click(object sender, ImageClickEventArgs e)
        {
            string confirmvalue = Request.Form["confirm_value"];
            var respuesta = confirmvalue.Split(',');
            if (respuesta.LastOrDefault() == "Yes")
            {
                string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
                trackingsN = new TrackingsN();
                var respuestas = trackingsN.EliminaProduc(commandArgs[0].ToString(), "ELIMINADO", "Orden eliminada por el Administrador", Session["cod_usu"].ToString());
                if (respuestas == true)
                {
                    script = "alert('EL producto: #" + commandArgs[0].ToString() + " se a eliminado correctamente.');";
                    ScriptManager.RegisterStartupScript(updPnlCustomerDetail, updPnlCustomerDetail.GetType(), "script", script, true);

                    btnBuscar_ClickC(null, null);
                }
            }
        }

        protected void btnCheckOut_ClickR(object sender, EventArgs e)
        {
            trackingsN = new TrackingsN();
            if (!String.IsNullOrEmpty(lbltitulo2.Text))
            {
                if (!String.IsNullOrEmpty(txtName.Text))
                {
                    if (!String.IsNullOrEmpty(txtCount.Text))
                    {
                        if (!String.IsNullOrEmpty(txtPtn.Text))
                        {
                            if (!String.IsNullOrEmpty(ddlEstadoActi.SelectedValue))
                            {



                                empleadoN = new EmpleadoN();
                                if (empleadoN.ActualizaProducto(Convert.ToInt32(lbltitulo2.Text), txtName.Text, Convert.ToInt32(txtCount.Text), Convert.ToInt32(txtPtn.Text),
                                    Convert.ToInt32(ddlEstadoActi.SelectedValue))
                                    )
                                {
                                    script = "alert('El producto: " + txtName.Text+ " se ha actualizado correctamente.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                    LimpiarCajasConsulta();
                                    CargarListadoProduc(dllProductEstado.SelectedValue);
                                }
                                else
                                {
                                    script = "alert('El producto: " + txtName.Text + " No se ha actulizado, revise la información.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                                }
                            }
                            else
                            {
                                script = "alert('Selecciona el estado el producto');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                               
                                mdlPopup.Show();
                            }
                        }
                        else
                        {
                            script = "alert('Los puntos es obligatorio.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                            txtPtn.Focus();
                            mdlPopup.Show();
                        }
                    }
                    else
                    {

                        script = "alert('La cantidad de procutos es obligatoria');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                        txtCount.Focus();
                        mdlPopup.Show();
                    }
                }
                else
                {

                    script = "alert('EL nombre del producto es obligatorio.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
                    txtName.Focus();
                    mdlPopup.Show();
                }

            }
            else
            {
                script = "alert('El producto no existe consulte al administrador.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), script, script, true);
              
                mdlPopup.Show();
            }

        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            mdlPopup.Show();
            string[] commandArgs = (sender as ImageButton).CommandArgument.ToString().Split(new char[] { ';' });
            lbltitulo2.Text = commandArgs[0].ToString();
            txtName.Text = commandArgs[1].ToString();
            txtCount.Text = commandArgs[3].ToString();
            txtPtn.Text = commandArgs[2].ToString();
            ddlEstadoActi.SelectedValue = commandArgs[4].ToString();

        }
    }
}