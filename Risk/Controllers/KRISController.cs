using Newtonsoft.Json;
using Risk.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Risk.Controllers
{
    public class KRISController : Controller
    {

        BD_Riesgos BD_Riesgos = new BD_Riesgos();
        BD_IndicadoresKRIS BD_IndicadoresKRIS = new BD_IndicadoresKRIS();
        BD_MontaDatosTabledata BD_MontaDatosTabledata = new BD_MontaDatosTabledata();


        // GET: KRIS
        public ActionResult KrisFicha(int id)
        {
            qIndicadores indicadorRecup = BD_IndicadoresKRIS.recuperarQIndicadores(id);
            return View(indicadorRecup);
        }


        public ActionResult Main(int id)
        {
            qIndicadores indicadorRecup = BD_IndicadoresKRIS.recuperarQIndicadores(id);
            return PartialView(indicadorRecup);
        }

        public ActionResult Scoope(int id)
        {
            
            TablaRiesgos_Risks tabla = new TablaRiesgos_Risks();
            Dictionary<string, object> filtros = new Dictionary<string, object>();
            DatosTablaModel tablaR = tabla.dameTabla(filtros);

            return PartialView(tablaR);
        }

        public ActionResult Archive(int id)
        {
            string colVer = "Estado,IndiiEvolucion,UltimaFecha,UltimoValor,NivelAlarma,NivelPrecaucion";
            string colTitulo = "Estado,IndiiEvolucion,UltimaFecha,UltimoValor,NivelAlarma,NivelPrecaucion";

            TablaIndicadores_KRIS tablaIndicadores = new TablaIndicadores_KRIS(colVer,colTitulo);
            DatosTablaModel modelTablaIndicadores = tablaIndicadores.dameTablaPorIdIndicador(id);
              

            return PartialView(modelTablaIndicadores);
        }



        public string pintaGrafico()
        {
            string cadena = ConfigurationManager.ConnectionStrings["RiskMVCConnectionString"].ConnectionString;
            string connectionString = cadena.Split(new[] { "Password=" }, StringSplitOptions.None)[1].Replace("\"", "");
            EncritPass encryt = new EncritPass();
            string passOK = encryt.Desencrit(connectionString);

            DataSet miDataSet = new DataSet();
            SqlConnection conexionBD = new SqlConnection(cadena.Replace(connectionString, passOK));
            SqlDataAdapter adaptador;
            SqlCommandBuilder builder;
            string jsonString = string.Empty;

            using (conexionBD = new SqlConnection(cadena.Replace(connectionString, passOK)))
            {
                try
                {
                    conexionBD.Open();
                    adaptador = new SqlDataAdapter("SELECT year, value FROM GraficoYear", conexionBD);
                    builder = new SqlCommandBuilder(adaptador);
                    adaptador.Fill(miDataSet, "GraficoYear");

                    DataTable miTabla = miDataSet.Tables["GraficoYear"];
                    jsonString = JsonConvert.SerializeObject(miTabla);

                }
                catch (SqlException)
                {
                }
                finally
                {
                    conexionBD.Close();
                    Session["miDataSet"] = miDataSet;
                }
            }
            return jsonString;
        }

        public ActionResult DeleteKris(int id)
        {
            // bool delete = BD_Riesgos.deleteKRIS(id);

            return RedirectToAction("KRISIndicators", "Assign");
        }
    }
}