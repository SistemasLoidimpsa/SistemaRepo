using Entidades;
using Negocio;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;
using iTextSharp.text.pdf.security;
using System.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using iTextSharp.text.html.simpleparser;
using System.Collections.Generic;

namespace LOADIMPSA
{
    public partial class ReporteCotizaciones : System.Web.UI.Page
    {
        [Obsolete]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["strCliente"] != null)
                {

                    btnImprimir_Click(null, null);

                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
        }

        [Obsolete]
        protected void btnImprimir_Click(object sender, EventArgs e)
        {

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Cotizacion" + Session["strCliente"].ToString() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Panel1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            iTextSharp.text.Document pdfdoc = new iTextSharp.text.Document(PageSize.A9, 40f, 40f, 10f, 20f);

            pdfdoc.SetPageSize(iTextSharp.text.PageSize.LETTER.Rotate());
            // Creamos la imagen y le ajustamos el tamaño
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:/Imagenes/cotizador.png");
            imagen.BorderWidth = 0;
            imagen.Alignment = iTextSharp.text.Image.UNDERLYING;
            //imagen.Alignment = Element.ALIGN_CENTER;
            imagen.ScalePercent(45);


            iTextSharp.text.html.simpleparser.HTMLWorker htmlParser = new HTMLWorker(pdfdoc);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfdoc, Response.OutputStream);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                HTMLWorker htmlparser = new HTMLWorker(pdfdoc);
                PdfWriter writer = PdfWriter.GetInstance(pdfdoc, memoryStream);
                pdfdoc.Open();
                pdfdoc.Add(imagen);
                htmlParser.Parse(sr);


                lblfecha.Text = "Quito" + ", " + DateTime.Now.ToString("dd") + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Now.ToString("yyyy");

                var datos = new ReportesN().ReporteCotizacionN(ConfigurationManager.AppSettings["cnnSQL"], Session["strCliente"].ToString());
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 90;
                float[] columnWidths = new float[] { 25f, 25f, 15f, 35f };
                table.SetWidths(columnWidths);
                table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                BaseFont bf = BaseFont.CreateFont(
                BaseFont.HELVETICA,
                BaseFont.CP1252,
                BaseFont.EMBEDDED);
                Font font = new Font(bf, 10, Font.NORMAL, BaseColor.BLACK);
                Font font2 = new Font(bf, 12, Font.BOLD, BaseColor.WHITE);
                Font font3 = new Font(bf, 12, Font.BOLD, BaseColor.WHITE);

                PdfPCell cell = new PdfPCell(new Phrase("COTIZACIÓN NRO. " + datos.idCotizacion, font3));
                cell.BackgroundColor = new BaseColor(29, 66, 137);
                cell.Colspan = 4;
                cell.FixedHeight = 40;
                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table.AddCell(cell);
                table.AddCell(new PdfPCell(new Phrase("CLIENTE:", font)) { FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.Nombre, font)) { Colspan = 3, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("CORREO:", font)) { FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.correo, font)) { Colspan = 3, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("FECHA:", font)) {  FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.fechaCotizacion, font)) { Colspan = 3, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("DESCRIPCIÓN:", font)) {  FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.descripcionMercaderia, font)) { Colspan = 3, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("VALOR FOB:", font)) { FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.valorFof, font)) { FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("PESO (LBS):", font)) { FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.peso, font)) { FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("EMITIDO POR:", font)) { FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.usuarioRegstro, font)) { Colspan = 3, FixedHeight = 20 }).HorizontalAlignment = 0;
                PdfPCell cell2 = new PdfPCell(new Phrase("SERVICIO COURIER LOIDIMPSA EXPRESS", font3));
                cell2.BackgroundColor = new BaseColor(29, 66, 137);
                cell2.Colspan = 4;
                cell2.FixedHeight = 40;
                cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table.AddCell(cell2);
                //PdfPCell cell3 = new PdfPCell(new Phrase("TOTAL SERVICIO COURIER CAT B", font));
                ////cell3.BackgroundColor = new BaseColor(55, 135, 222);
                //cell3.Colspan = 2;
                //cell3.FixedHeight = 20;
                //cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.AddCell(cell3);
                //PdfPCell cell4 = new PdfPCell(new Phrase("$. " + datos.totalEnvio, font));
                ////cell4.BackgroundColor = new BaseColor(55, 135, 222);
                //cell4.Colspan = 2;
                //cell4.FixedHeight = 20;
                //cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.AddCell(cell4);
                //PdfPCell cell5 = new PdfPCell(new Phrase("ENTREGA A DOMICILIO A NIVEL NACIONAL", font));
                ////cell5.BackgroundColor = new BaseColor(55, 135, 222);
                //cell5.Colspan = 2;
                //cell5.FixedHeight = 20;
                //cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.AddCell(cell5);
                //PdfPCell cell6 = new PdfPCell(new Phrase("$. 5.00", font));
                ////cell6.BackgroundColor = new BaseColor(55, 135, 222);
                //cell6.Colspan = 2;
                //cell6.FixedHeight = 20;
                //cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.AddCell(cell6);
                PdfPCell cell7 = new PdfPCell(new Phrase("Total Importación *", font3));
                cell7.BackgroundColor = new BaseColor(29, 66, 137);
                cell7.Colspan = 2;
                cell7.FixedHeight = 20;
                cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell7);
                decimal sumatoria = Convert.ToDecimal(datos.totalEnvio);
                PdfPCell cell8 = new PdfPCell(new Phrase(sumatoria.ToString(), font3));
                cell8.BackgroundColor = new BaseColor(29, 66, 137);
                cell8.Colspan = 2;
                cell8.FixedHeight = 20;
                cell8.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell8);
                PdfPCell cell9 = new PdfPCell(new Phrase("(*) Los valores detallados son calculados en base a un peso referencial, estos pueden sufrir variaciones. En caso de no requerir entrega a domicilio podrá retirarlo en nuestras oficinas.", font));
                cell9.BackgroundColor = new BaseColor(0, 156, 222);
                cell9.Colspan = 4;
                cell9.FixedHeight = 35;
                cell9.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell9);
                PdfPCell cell10 = new PdfPCell(new Phrase("(*) Valor cotizado no incluye entrega a domicilio.", font));
                cell10.BackgroundColor = new BaseColor(0, 156, 222);
                cell10.Colspan = 4;
                cell10.FixedHeight = 35;
                cell10.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell10);


                htmlParser.Parse(sr);
                pdfdoc.Add(Chunk.NEWLINE);
                pdfdoc.Add(Chunk.NEWLINE);
                pdfdoc.Add(Chunk.NEWLINE);
                pdfdoc.Add(Chunk.NEWLINE);
                pdfdoc.Add(table);
                pdfdoc.Close();
                memoryStream.Close();
                Response.Write(pdfdoc);
                Response.End();
            }


        }

    }
}