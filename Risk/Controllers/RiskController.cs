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
using System.Reflection;

namespace Risk.Controllers
{
    public class RiskController : Controller
    {
        BD_Riesgos BD_Riesgos = new BD_Riesgos();


        // GET: Risk
        public ActionResult RiskFicha(int id)
        {         
            qRiesgosNombres riesgoRecup = BD_Riesgos.recuperarRiesgo(id);
            return View(riesgoRecup);
        }

        public ActionResult General(int id)
        {
            FichaRiesgoVM fichaRiesgoVM = montaVM(id);
            return PartialView(fichaRiesgoVM);
        }

        public ActionResult OperationalImpact() {
            return PartialView();
        }

        public ActionResult Historical(int id)
        {
            FichaRiesgoVM fichaRiesgoVM = montaVM(id);
            return PartialView(fichaRiesgoVM);
        }

        public ActionResult FinancialImpactCombos(int id)
        {
            FichaRiesgoVM fichaRiesgoVM = montaVM(id);
            return PartialView(fichaRiesgoVM);
        }

        public ActionResult FinancialImpactTextBox(int id)
        {
            FichaRiesgoVM fichaRiesgoVM = montaVM(id);
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
                catch (SqlException)
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
            qRiesgosNombres riesgoRecup = new qRiesgosNombres(); ;
            qRiesgos_Evaluaciones_Valores evaluaciones = new qRiesgos_Evaluaciones_Valores();

            if (id != 0) {
                riesgoRecup = BD_Riesgos.recuperarRiesgo(id);
                evaluaciones = BD_Riesgos.recuperaEvaluaciones(id);
            }

            FichaRiesgoVM fichaRiesgoVM = new FichaRiesgoVM();
            fichaRiesgoVM.qRiesgosNombre_VM = riesgoRecup;
            fichaRiesgoVM.qRiesgos_Evaluaciones_Valores_VM = evaluaciones;

            DropDownModel dropdowns = new DropDownModel();
            dropdowns.datosClasificacion2= dropdowns.listadoClasifDinamic(Convert.ToInt32(fichaRiesgoVM.qRiesgosNombre_VM.IdClasificacion1));
            dropdowns.datosClasificacion3 = dropdowns.listadoClasifDinamic(Convert.ToInt32(fichaRiesgoVM.qRiesgosNombre_VM.IdClasificacion2));

            fichaRiesgoVM.dropDowns = dropdowns;

            return fichaRiesgoVM;
        }

        [HttpPost]
        public bool formGeneral(FormGeneralModel datosFormulario)
        {

            if (datosFormulario.IdRiesgo.Equals("0")) {
                return insertarNuevoRiesgo(datosFormulario);
            }

            else {
                return updateRiesgo(datosFormulario);
            }       
        }


        private bool insertarNuevoRiesgo(FormGeneralModel datosFormulario) {
            PropertyInfo[] props = datosFormulario.GetType().GetProperties();
            bool insert = false;

            tRiesgos riesgoNuevo = new tRiesgos();

            riesgoNuevo.CodRiesgo = datosFormulario.CodRiesgo;
            riesgoNuevo.CodRiesgoLocalizado = datosFormulario.CodRiesgo.Substring(0, 8);
            riesgoNuevo.Nombre = datosFormulario.Nombre;
            riesgoNuevo.IdCategoria = int.Parse(datosFormulario.IdCategoria);
            riesgoNuevo.IdClasificacion1 = int.Parse(datosFormulario.IdClasificacion1);
            riesgoNuevo.IdClasificacion2 = int.Parse(datosFormulario.IdClasificacion2);
            riesgoNuevo.IdClasificacion3 = int.Parse(datosFormulario.IdClasificacion3);
            riesgoNuevo.Descripcion = datosFormulario.Descripcion;
            riesgoNuevo.Justificacion = riesgoNuevo.Justificacion;
            riesgoNuevo.Ejemplo = datosFormulario.Ejemplo;
            riesgoNuevo.IdSegmentacion1 = int.Parse(datosFormulario.IdSegmentacion1);
            riesgoNuevo.IdResponsable = int.Parse(datosFormulario.IdResponsable);
            riesgoNuevo.IdSupervisor = int.Parse(datosFormulario.IdResponsable2);

            //Insertar el riesgo en la BD
            // Crear tRelEstructuraRiesgos

            return insert;
        }

        private bool updateRiesgo(FormGeneralModel datosFormulario) {

            List<string> datosQRiesgosNombre = new List<string>();
            List<string> datosQRiesgosEvaluacionesValores = new List<string>();

            PropertyInfo[] props = datosFormulario.GetType().GetProperties();
            bool update = false;

            foreach (PropertyInfo item in props) {
                if (item.GetValue(datosFormulario, null) != null) {
                    try {

                        string tabla = item.GetValue(datosFormulario, null).ToString().Split(':')[1];

                        switch (tabla) {
                            case "qRiesgosNombre":
                                datosQRiesgosNombre.Add(item.Name + ":" + item.GetValue(datosFormulario, null).ToString().Split(':')[0]);
                                break;
                            case "qRiesgosEvaluacionesValores":
                                datosQRiesgosEvaluacionesValores.Add(item.Name + ":" + item.GetValue(datosFormulario, null).ToString().Split(':')[0]);
                                break;
                        }

                    } catch (Exception) { }
                }
            }

            update = BD_Riesgos.actualizaQRiesgosNombre(datosQRiesgosNombre, int.Parse(datosFormulario.IdRiesgo));
            return update;
        }


        // Metodo (llamada desde scripts2.js) para recuperar el siguiente riesgo disponible y rellenar Particle Code en un riesgo nuevo
        public string dameUltimoRiesgoDisponible (string idEstructura) {
            return BD_Riesgos.ultimoRiesgoDisponible(idEstructura);
        }


        /****************************************************************/
        public string recuperaDrop(string nombre)
        {
            DropDownModel dropdown = new DropDownModel();
            Dictionary<int, List<string>> dicEnvio = nombre ==("datosEvaFrecuencia") ? dropdown.datosEvaFrecuencia : dropdown.datosEvaSeveridad;
            var j = JsonConvert.SerializeObject(dicEnvio);
            return j;
        }

    }
}