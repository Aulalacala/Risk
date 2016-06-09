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
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Report()
        {
            return View();
        }

        public void generaReport()
        {

            TablaRiesgos_Risks tabla = new TablaRiesgos_Risks();
            Dictionary<string, object> filtros = new Dictionary<string, object>();
            DatosTablaModel tablafiltrada = tabla.dameTabla(filtros);

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@"C:\Users\Sony\Desktop\Rg_Datos_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            //doc.AddTitle("Mi primer PDF");
            //doc.AddCreator("Roberto Torres");

            // Abrimos el archivo
            doc.Open();

            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("DATOS  RIESGOS"));
            doc.Add(Chunk.NEWLINE);

            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Creamos una tabla que contendrá el nombre, apellido y país 
            // de nuestros visitante.

            PdfPTable tblPrueba = new PdfPTable(tablafiltrada.datosTHead.Count());
            tblPrueba.WidthPercentage = 100;


            PdfPCell cl;
            foreach (var columna in tablafiltrada.datosTHead)
            {
                cl = new PdfPCell(new Phrase(columna.Value, _standardFont));
                cl.BorderWidth = 0;
                cl.BorderWidthBottom = 0.75f;
                tblPrueba.AddCell(cl);
            }

            //// Configuramos el título de las columnas de la tabla
            //PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", _standardFont));
            //clNombre.BorderWidth = 0;
            //clNombre.BorderWidthBottom = 0.75f;

            //PdfPCell clApellido = new PdfPCell(new Phrase("Apellido", _standardFont));
            //clApellido.BorderWidth = 0;
            //clApellido.BorderWidthBottom = 0.75f;

            //PdfPCell clPais = new PdfPCell(new Phrase("País", _standardFont));
            //clPais.BorderWidth = 0;
            //clPais.BorderWidthBottom = 0.75f;

            //// Añadimos las celdas a la tabla
            //tblPrueba.AddCell(clNombre);
            //tblPrueba.AddCell(clApellido);
            //tblPrueba.AddCell(clPais);

            // Llenamos la tabla con información

            foreach (var body in tablafiltrada.datosTBody)
            {
                cl = new PdfPCell(new Phrase("Roberto", _standardFont));
                cl.BorderWidth = 0;
            }

            //clNombre = new PdfPCell(new Phrase("Roberto", _standardFont));
            //clNombre.BorderWidth = 0;

            //clApellido = new PdfPCell(new Phrase("Torres", _standardFont));
            //clApellido.BorderWidth = 0;

            //clPais = new PdfPCell(new Phrase("Puerto Rico", _standardFont));
            //clPais.BorderWidth = 0;

            //// Añadimos las celdas a la tabla
            //tblPrueba.AddCell(clNombre);
            //tblPrueba.AddCell(clApellido);
            //tblPrueba.AddCell(clPais);

            //clNombre = new PdfPCell(new Phrase("Juan", _standardFont));
            //clNombre.BorderWidth = 0;

            //clApellido = new PdfPCell(new Phrase("Rodríguez", _standardFont));
            //clApellido.BorderWidth = 0;

            //clPais = new PdfPCell(new Phrase("México", _standardFont));
            //clPais.BorderWidth = 0;

            //tblPrueba.AddCell(clNombre);
            //tblPrueba.AddCell(clApellido);
            //tblPrueba.AddCell(clPais);

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(tblPrueba);

            doc.Close();
            writer.Close();

            //MessageBox.Show("¡PDF creado!");
        }
    }
}