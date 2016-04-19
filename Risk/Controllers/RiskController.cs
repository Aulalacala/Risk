using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risk.Controllers;
using Risk.Models;
using Risk.ViewModels;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Risk.Controllers
{
    public class RiskController : Controller
    {
        BD_Riesgos BD_Riesgos = new BD_Riesgos();


        // GET: Risk
        public ActionResult RiskFicha(int id)
        {         
            FichaRiesgoVM fichaRiesgoVM = montaVM(id);
            return View(fichaRiesgoVM);
        }

        public ActionResult General(int id)
        {
            FichaRiesgoVM fichaRiesgoVM = montaVM(id);
            //Dropdowns
            Dictionary<int, string> dicCategorias = BD_Riesgos.listadoCategorias();
            ViewBag.dicCategorias = dicCategorias;

            Dictionary<int, string> dicClasificacion1 = BD_Riesgos.listadoClasif1();
            ViewBag.dicClasificacion1 = dicClasificacion1;

            Dictionary<int, string> dicClasificacion2 = BD_Riesgos.listadoClasifDinamic(Convert.ToInt32(fichaRiesgoVM.qRiesgosNombre_VM.IdClasificacion1));
            ViewBag.dicClasificacion2 = dicClasificacion2;

            Dictionary<int, string> dicClasificacion3 = BD_Riesgos.listadoClasifDinamic(Convert.ToInt32(fichaRiesgoVM.qRiesgosNombre_VM.IdClasificacion2));
            ViewBag.dicClasificacion3 = dicClasificacion3;


            //Financial Impact





            return PartialView(fichaRiesgoVM);
        }

        public ActionResult Graphic()
        {
            qRiesgosNombres riesgoRecup = (qRiesgosNombres)Session["riesgo"];

            FichaRiesgoVM fichaRiesgoVM = new FichaRiesgoVM();
            fichaRiesgoVM.qRiesgosNombre_VM = riesgoRecup;

            return PartialView();
        }

        //OTROS MÉTODOS NECESARIOS
        public string pintaGrafico()
        {
            DataSet miDataSet = new DataSet();
            SqlConnection conexionBD = new SqlConnection(ConfigurationManager.ConnectionStrings["RiskConnectionString"].ConnectionString);
            SqlDataAdapter adaptador;
            SqlCommandBuilder builder;
            string jsonString = string.Empty;
           
            using (conexionBD = new SqlConnection(ConfigurationManager.ConnectionStrings["RiskConnectionString"].ConnectionString))
            {
                try
                {
                    conexionBD.Open();
                    adaptador = new SqlDataAdapter("SELECT year, value FROM GraficoYear", conexionBD);
                    builder= new SqlCommandBuilder(adaptador);
                    adaptador.Fill(miDataSet, "GraficoYear");

                    DataTable miTabla = miDataSet.Tables["GraficoYear"];
                    jsonString = JsonConvert.SerializeObject(miTabla);

                }
                catch (SqlException ex)
                {
                }
                finally
                {
                    conexionBD.Close();
                    Session["miDataSet"] =miDataSet;
                }
            }
            return jsonString;
        }

        public FichaRiesgoVM montaVM(int id)
        {
            qRiesgosNombres riesgoRecup = BD_Riesgos.recuperarRiesgo(id);
            qRiesgos_Evaluaciones__Valores evaluaciones = BD_Riesgos.recuperaEvaluaciones(id);

            FichaRiesgoVM fichaRiesgoVM = new FichaRiesgoVM();
            fichaRiesgoVM.qRiesgosNombre_VM = riesgoRecup;
            fichaRiesgoVM.qRiesgos_Evaluaciones_Valores_VM = evaluaciones;

            return fichaRiesgoVM;
        }

    }
}