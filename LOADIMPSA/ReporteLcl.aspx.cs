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
    public partial class ReporteLcl : System.Web.UI.Page
    {
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
            Response.AddHeader("content-disposition", "attachment;filename=Cotizacion Lcl " + Session["strCliente"].ToString() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Panel1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            iTextSharp.text.Document pdfdoc = new iTextSharp.text.Document(PageSize.A9, 40f, 40f, 2f, 20f);

            pdfdoc.SetPageSize(iTextSharp.text.PageSize.LETTER.Rotate());
            // Creamos la imagen y le ajustamos el tamaño
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:/Imagenes/cotizador.png");
            imagen.BorderWidth = 0;
            imagen.Alignment = iTextSharp.text.Image.UNDERLYING;
            //imagen.Alignment = Element.ALIGN_CENTER;
            imagen.ScalePercent(45);


            iTextSharp.text.html.simpleparser.HTMLWorker htmlParser = new HTMLWorker(pdfdoc);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfdoc, Response.OutputStream);


            // Header
       
            iTextSharp.text.Font fontHeader_2 = FontFactory.GetFont("Calibri", 15, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(125, 125, 125));


            using (MemoryStream memoryStream = new MemoryStream())
            {
                HTMLWorker htmlparser = new HTMLWorker(pdfdoc);
                PdfWriter writer = PdfWriter.GetInstance(pdfdoc, memoryStream);
                writer.PageEvent = new HeaderFooter();
                pdfdoc.Open();
                pdfdoc.Add(imagen);
                htmlParser.Parse(sr);


                lblfecha.Text = "Quito" + ", " + DateTime.Now.ToString("dd") + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Now.ToString("yyyy");


                //Para el header 


                //pdfdoc.AddHeader("Owner", "Civil Security Department");

                //PdfContentByte cb = writer.DirectContent;
                //cb.MoveTo(20f, 2f);
                //cb.LineTo(200, 2f);
                //cb.Stroke();

                //Paragraph paraHeader_1 = new Paragraph("Civil Security Department", fontHeader_2);
                //paraHeader_1.Alignment = Element.ALIGN_RIGHT;
                //paraHeader_1.SpacingBefore =0f ;
                //paraHeader_1.SpacingAfter = 0f;
                //pdfdoc.Add(paraHeader_1);

                //Paragraph paraHeader_2 = new Paragraph("Loidimpsa.com", fontHeader_2);
                //paraHeader_2.Alignment = Element.ALIGN_RIGHT;
                //paraHeader_2.SpacingAfter = 0;
                //paraHeader_2.SpacingBefore = 0;
                //pdfdoc.Add(paraHeader_2);
                // coninuacion


                var datos = new ReportesN().ReporteCotizacionLcl(ConfigurationManager.AppSettings["cnnSQL"], Session["strCliente"].ToString());
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 90;
                float[] columnWidths = new float[] { 15f, 15f, 35f, 35f };
                table.SetWidths(columnWidths);
                table.SpacingBefore = 0;
                table.SpacingAfter = 0;
                table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                BaseFont bf = BaseFont.CreateFont(
                BaseFont.HELVETICA,
                BaseFont.CP1252,
                BaseFont.EMBEDDED);
                Font font = new Font(bf, 8, Font.NORMAL, BaseColor.BLACK);
                Font font2 = new Font(bf, 9, Font.BOLD, BaseColor.WHITE);
                Font font3 = new Font(bf, 8, Font.BOLD, BaseColor.WHITE);

                PdfPCell cell = new PdfPCell(new Phrase("COTIZACIÓN LCL NRO. " + datos.idCotizacion, font3));
                cell.BackgroundColor = new BaseColor(29, 66, 137);
                cell.Colspan = 4;
                cell.FixedHeight = 30;
                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table.AddCell(cell);
                table.AddCell(new PdfPCell(new Phrase("CLIENTE", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.nombreCliente, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("CORREO", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.correo, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("FECHA EMISIón", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                
                table.AddCell(new PdfPCell(new Phrase( Convert.ToString(datos.fechaEmisionCoti).Split(' ')[0], font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("FECHA VENCIMIENTO", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.fechaExpiraCotizacion.Split(' ')[0], font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("DESCRIPCIÓN", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.descripcionMercaderia, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("PUERTO ORIGEN", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.puertoOrigen, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("PUERTO DESTINO", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.puertoDestino, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("DIAS DE TRANSITO", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.diasTranscurrido, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("PESO (Kg)", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.peso, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("METROS CÚBICOS", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.metrosCubicos, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("EMITIDO POR", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.usuarioRegistro, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("FLETE", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.fleteInternacional, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                table.AddCell(new PdfPCell(new Phrase("OBSERVACIÓN", font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 1;
                table.AddCell(new PdfPCell(new Phrase(datos.observacion, font)) { Colspan = 2, FixedHeight = 20 }).HorizontalAlignment = 0;
                PdfPCell cell2 = new PdfPCell(new Phrase("GASTOS LOCALES", font3));
                cell2.BackgroundColor = new BaseColor(29, 66, 137);
                cell2.Colspan = 4;
                cell2.FixedHeight = 30;
                cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                table.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase("GASTOS LOCALES SPOT", font));
                //cell3.BackgroundColor = new BaseColor(55, 135, 222);
                cell3.Colspan = 2;
                cell3.FixedHeight = 20;
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell(new Phrase("$. " + datos.gastoSpot, font));
                //cell4.BackgroundColor = new BaseColor(55, 135, 222);
                cell4.Colspan = 2;
                cell4.FixedHeight = 20;
                cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell4);
                PdfPCell cell5 = new PdfPCell(new Phrase("COLLECT FEE 5%", font));
                //cell5.BackgroundColor = new BaseColor(55, 135, 222);
                cell5.Colspan = 2;
                cell5.FixedHeight = 20;
                cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell5);
                PdfPCell cell6 = new PdfPCell(new Phrase("$. " + datos.coFee, font));
                //cell6.BackgroundColor = new BaseColor(55, 135, 222);
                cell6.Colspan = 2;
                cell6.FixedHeight = 20;
                cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell6);
                PdfPCell cell7 = new PdfPCell(new Phrase("IVA", font));
                //cell6.BackgroundColor = new BaseColor(55, 135, 222);
                cell7.Colspan = 2;
                cell7.FixedHeight = 20;
                cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell7);
                PdfPCell cell8 = new PdfPCell(new Phrase("$. " + datos.iva, font));
                //cell6.BackgroundColor = new BaseColor(55, 135, 222);
                cell8.Colspan = 2;
                cell8.FixedHeight = 20;
                cell8.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell8);
                PdfPCell cell20 = new PdfPCell(new Phrase("TOTAL GASTOS LOCALES", font));
                //cell6.BackgroundColor = new BaseColor(55, 135, 222);
                cell20.Colspan = 2;
                cell20.FixedHeight = 20;
                cell20.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell20);
                PdfPCell cell21 = new PdfPCell(new Phrase("$. " + datos.totalGasto, font));
                //cell6.BackgroundColor = new BaseColor(55, 135, 222);
                cell21.Colspan = 2;
                cell21.FixedHeight = 20;
                cell21.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell21);
                
                PdfPCell cell12 = new PdfPCell(new Phrase("TOTAL COTIZACION*:", font3));
                cell12.BackgroundColor = new BaseColor(29, 66, 137);
                cell12.Colspan = 2;
                cell12.FixedHeight = 20;
                cell12.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell12);

                PdfPCell cell13 = new PdfPCell(new Phrase("$. " + datos.totalCotiza, font3));
                cell13.BackgroundColor = new BaseColor(29, 66, 137);
                cell13.Colspan = 2;
                cell13.FixedHeight = 20;
                cell13.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell13);
                PdfPCell cell14 = new PdfPCell(new Phrase(" | COTIZACIÓN REALIZADA LCL | \n", font));
                //cell9.BackgroundColor = new BaseColor(55, 135, 222);
                cell14.Colspan = 4;
                cell14.FixedHeight = 35;
                cell14.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell14);
                
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

        private IPdfPageEvent HeaderFooter()
        {
            throw new NotImplementedException();
        }
    }
    class HeaderFooter : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {   
            PdfPTable tbHeader = new PdfPTable(3);
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;
            tbHeader.AddCell(new Paragraph());
            PdfPCell _cell = new PdfPCell(new Phrase("RUC: 0993089559001"));
            _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbHeader.AddCell(_cell);
            tbHeader.AddCell(new Paragraph());
            tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin)+40, writer.DirectContent);

           
        }
    }
}