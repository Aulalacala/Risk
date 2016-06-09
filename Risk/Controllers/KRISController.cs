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

        // GET: KRIS
        public ActionResult KrisFicha(int id)
        {
            qRiesgosNombres riesgoRecup = BD_Riesgos.recuperarQriesgoNombre(id);
            return View(riesgoRecup);
        }

        public ActionResult Main(int id = 0)
        {
            return PartialView();
        }

        public ActionResult Scoope()
        {
            //string colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
            //string colTitulos = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";

            //DatosTablaModel datosTabla = new DatosTablaModel();
            //datosTabla.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosNombres", colVer, colTitulos);

            //Dictionary<int, List<Tuple<string, string>>> dicBody = new Dictionary<int, List<Tuple<string, string>>>();
            //Dictionary<int, List<Tuple<string, string>>> dic = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos);

            //foreach (var item in dic.Take(3))
            //{
            //    dicBody.Add(item.Key, item.Value);
            //}

            //datosTabla.datosTBody = dicBody;
            //datosTabla.vistaProcedencia = "Scoopes";
            //datosTabla.editable = false;
            //datosTabla.borrar = false;

            TablaRiesgos_Risks tabla = new TablaRiesgos_Risks();
            Dictionary<string, object> filtros = new Dictionary<string, object>();
            DatosTablaModel tablaR = tabla.dameTabla(filtros);

            return PartialView(tabla);
        }

        public ActionResult Archive()
        {
            TablaRiesgos_Risks tabla = new TablaRiesgos_Risks();
            Dictionary<string, object> filtros = new Dictionary<string, object>();
            DatosTablaModel tablaR = tabla.dameTabla(filtros);


            //string colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
            //string colTitulos = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";

            //DatosTablaModel datosTabla = new DatosTablaModel();
            //datosTabla.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosNombres", colVer, colTitulos);

            //Dictionary<int, List<Tuple<string, string>>> dicBody = new Dictionary<int, List<Tuple<string, string>>>();
            //Dictionary<int, List<Tuple<string, string>>> dic = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos);


            //foreach (var item in dic.Take(3))
            //{
            //    dicBody.Add(item.Key, item.Value);
            //}

            //datosTabla.datosTBody = dicBody;
            //datosTabla.vistaProcedencia = "Scoopes";
            //datosTabla.editable = false;
            //datosTabla.borrar = false;
            return PartialView(tablaR);
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