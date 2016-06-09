using iTextSharp.text;
using iTextSharp.text.pdf;
using Risk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Risk.Controllers
{

    public class MyHeaderFooterEvent : PdfPageEventHelper
    {
        Font FONT = new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD);

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfContentByte canvas = writer.DirectContent;
            ColumnText.ShowTextAligned(
              canvas, Element.ALIGN_LEFT,
              new Phrase("Header", FONT), 10, 760, 0
            );
            ColumnText.ShowTextAligned(
              canvas, Element.ALIGN_LEFT,
              new Phrase("Footer", FONT), 10, 10, 0
            );
        }
    }

    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Report()
        {
            return View();
        }

        public ActionResult pdfOK()
        {
            return View();
        }

        public ActionResult generaReport()
        {
            //Datos de Riesgos que vamos a insertar
            TablaRiesgos_Risks tabla = new TablaRiesgos_Risks();
            Dictionary<string, object> filtros = new Dictionary<string, object>();
            DatosTablaModel tablaR = tabla.dameTabla(filtros);
           

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(iTextSharp.text.PageSize.LEDGER, 10, 10, 42, 35);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@"C:\Users\Sony\Desktop\Rg_Datos_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            //doc.AddTitle("Mi primer PDF");
            //doc.AddCreator("Roberto Torres");

            // Abrimos el archivo
            doc.Open();
            writer.PageEvent = new MyHeaderFooterEvent();

            // Escribimos el encabezamiento en el documento                    
            doc.Add(new Paragraph("DATOS  RIESGOS"));
            doc.Add(Chunk.NEWLINE);

            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Creamos una tabla que contendrá el nombre, apellido y país 
            // de nuestros visitante.

            PdfPTable tblPrueba = new PdfPTable(tablaR.datosTHead.Count());
            tblPrueba.WidthPercentage = 100;


            PdfPCell cl;
            // Configuramos el título de las columnas de la tabla
            foreach (var columna in tablaR.datosTHead)
            {
                cl = new PdfPCell(new Phrase(columna.Value, _standardFont));
                cl.BorderWidth = 0;
                cl.BorderWidthBottom = 0.75f;
                // Añadimos las celdas a la tabla
                tblPrueba.AddCell(cl);
            }
                    
            // Llenamos la tabla con información
            foreach (var body in tablaR.datosTBody.Values)
            {
                foreach (var item in body)
                {
                    cl = new PdfPCell(new Phrase(item.Item2 , _standardFont));
                    cl.BorderWidth = 0;
                    // Añadimos las celdas a la tabla
                    tblPrueba.AddCell(cl);
                }               
            }

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(tblPrueba);

            /***********************OTRA TABLA CON OTRAS INFORMACIONES**********************************/

            //Datos de KRIS que vamos a insertar
            TablaIndicadores_KRIS tablaIndicadores = new TablaIndicadores_KRIS();
            DatosTablaModel tablaK = tablaIndicadores.dameTabla(filtros);

            doc.NewPage();
            doc.Add(new Paragraph("DATOS  KRIS"));
            doc.Add(Chunk.NEWLINE);
            PdfPTable tblPruebaK = new PdfPTable(tablaK.datosTHead.Count());
            tblPruebaK.WidthPercentage = 100;
            PdfPCell cl2;
            // Configuramos el título de las columnas de la tabla
            foreach (var columna in tablaK.datosTHead)
            {
                cl2 = new PdfPCell(new Phrase(columna.Value, _standardFont));
                cl2.BorderWidth = 0;
                cl2.BorderWidthBottom = 0.75f;
                // Añadimos las celdas a la tabla
                tblPruebaK.AddCell(cl2);
            }

            // Llenamos la tabla con información
            foreach (var body in tablaK.datosTBody.Values)
            {
                foreach (var item in body)
                {
                    cl2 = new PdfPCell(new Phrase(item.Item2, _standardFont));
                    cl2.BorderWidth = 0;
                    // Añadimos las celdas a la tabla
                    tblPruebaK.AddCell(cl2);
                }
            }

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(tblPruebaK);

            doc.Close();
            writer.Close();

            return RedirectToAction("pdfOK", "Reports");
        }
    }
}