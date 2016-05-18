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
        string colVer = "Activa,Ultima,Fecha,NombreFrecAntes,NombreSeveAntes,NombreFrecDespues,NombreSeveDespues";
        string colTitulos = "Activa,Ultima,Fecha,FrecuenciaA,SeveridadA,FrecuenciaD,SeveridadD";


        // GET: Risk
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idEstructura"></param>
        /// <returns></returns>
        public ActionResult RiskFicha(int id, int idEstructura = 0)
        {
            qRiesgosNombres riesgoRecup = BD_Riesgos.recuperarRiesgo(id);
            ViewBag.idEstructura = idEstructura;
            return View(riesgoRecup);
        }

        public ActionResult General(int id, int idEstructura = 0)
        {
            FichaRiesgoVM fichaRiesgoVM = montaVM(id);

            if(idEstructura != 0) {
                fichaRiesgoVM.qRiesgosNombre_VM.CodRiesgo = dameUltimoRiesgoDisponible(idEstructura.ToString());
                fichaRiesgoVM.qRiesgosNombre_VM.CodRiesgoLocalizado = fichaRiesgoVM.qRiesgosNombre_VM.CodRiesgo.Substring(0, 8);
            }

            return PartialView(fichaRiesgoVM);
        }


        public ActionResult OperationalImpact()
        {
            return PartialView();
        }


        public ActionResult Historical(int id)
        {
            FichaRiesgoVM fichaRiesgoVM = montaVM(id);

            //TempData["datosthead"] = BD_Riesgos.nombresColTabla("qRiesgos_Evaluaciones_Valores", colVer, colTitulos);
            //TempData["datostbody"] = BD_Riesgos.cargaTablaDatos("qRiesgos_Evaluaciones_Valores", colVer, colTitulos, null, 0, 0, 0, 0, Convert.ToInt32(id));

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

        public FichaRiesgoVM montaVM(int id)
        {
            qRiesgosNombres riesgoRecup = new qRiesgosNombres(); ;
            qRiesgos_Evaluaciones_Valores evaluaciones = new qRiesgos_Evaluaciones_Valores();

            if (id != 0)
            {
                riesgoRecup = BD_Riesgos.recuperarRiesgo(id);
                evaluaciones = BD_Riesgos.recuperaEvaluaciones(id);
            }


            FichaRiesgoVM fichaRiesgoVM = new FichaRiesgoVM();
            fichaRiesgoVM.qRiesgosNombre_VM = riesgoRecup;
            fichaRiesgoVM.qRiesgos_Evaluaciones_Valores_VM = evaluaciones;


            DropDownModel dropdowns = new DropDownModel();
            dropdowns.datosClasificacion2 = dropdowns.listadoClasifDinamic(Convert.ToInt32(fichaRiesgoVM.qRiesgosNombre_VM.IdClasificacion1));
            dropdowns.datosClasificacion3 = dropdowns.listadoClasifDinamic(Convert.ToInt32(fichaRiesgoVM.qRiesgosNombre_VM.IdClasificacion2));

            fichaRiesgoVM.dropDowns = dropdowns;

            return fichaRiesgoVM;
        }

        [HttpPost]
        public ActionResult formGeneral(FormGeneralModel datosFormulario)
        {
            int idRiesgo = 0;

            if (datosFormulario.IdRiesgo.Equals("0")) {
                idRiesgo = insertarNuevoRiesgo(datosFormulario);
            }

            else {
                 idRiesgo = updateRiesgo(datosFormulario);
            }

            
            return Json(Url.Action("RiskFicha", "Risk", new { id = idRiesgo }));

        }


        private int insertarNuevoRiesgo(FormGeneralModel datosFormulario)
        {
            PropertyInfo[] props = datosFormulario.GetType().GetProperties();
            bool insert = false;

            tRiesgos riesgoNuevo = new tRiesgos();

            riesgoNuevo.CodRiesgo = datosFormulario.CodRiesgo != null ? datosFormulario.CodRiesgo.Split(':')[0] : null;
            riesgoNuevo.CodRiesgoLocalizado = datosFormulario.CodRiesgo != null ? datosFormulario.CodRiesgo.Substring(0, 8) : null;
            riesgoNuevo.Nombre = datosFormulario.Nombre != null ? datosFormulario.Nombre.Split(':')[0] : null;
            riesgoNuevo.IdCategoria = datosFormulario.IdCategoria != null ? int.Parse(datosFormulario.IdCategoria.Split(':')[0]) : 0;
            riesgoNuevo.IdClasificacion1 = datosFormulario.IdClasificacion1 != null ? int.Parse(datosFormulario.IdClasificacion1.Split(':')[0]) : 0;
            riesgoNuevo.IdClasificacion2 = datosFormulario.IdClasificacion2 != null ? int.Parse(datosFormulario.IdClasificacion2.Split(':')[0]) : 0;
            riesgoNuevo.IdClasificacion3 = datosFormulario.IdClasificacion3 != null ? int.Parse(datosFormulario.IdClasificacion3.Split(':')[0]) : 0;
            riesgoNuevo.Descripcion = datosFormulario.Descripcion != null ? datosFormulario.Descripcion.Split(':')[0] : null;
            riesgoNuevo.Justificacion = datosFormulario.Justificacion != null ? datosFormulario.Justificacion.Split(':')[0] : null;
            riesgoNuevo.Ejemplo = datosFormulario.Ejemplo != null ? datosFormulario.Ejemplo.Split(':')[0] : null;
            riesgoNuevo.IdSegmentacion1 = datosFormulario.IdSegmentacion1 != null ? int.Parse(datosFormulario.IdSegmentacion1.Split(':')[0]) : 0;
            riesgoNuevo.IdResponsable = datosFormulario.IdResponsable != null ? int.Parse(datosFormulario.IdResponsable.Split(':')[0]) : 0;
            riesgoNuevo.IdSupervisor = datosFormulario.IdResponsable2 != null ? int.Parse(datosFormulario.IdResponsable2.Split(':')[0]) : 0;

            //Insertar el riesgo en la BD (Devuelve el riesgo insertado con el idRiesgo autogenerado)
            tRiesgos riesgoInsertado = BD_Riesgos.insertarNuevoRiesgo(riesgoNuevo);


            if (datosFormulario.idEstructura != null)
            {
                // Crear tRelEstructuraRiesgos
                tRelEstructuraRiesgos estructuraNuevo = new tRelEstructuraRiesgos();
                estructuraNuevo.IdRiesgo = riesgoInsertado.IdRiesgo;
                estructuraNuevo.IdEstructura = int.Parse(datosFormulario.idEstructura.Split(':')[0]);


                // Insertar tRelEstructuraRiesgo devuelve true si está todo OK
                insert = BD_Riesgos.insertarTRelEstructuraRiesgoNuevo(estructuraNuevo);
            }

            BD_Riesgos.insertarTRiesgosEvaluaciones(riesgoInsertado.IdRiesgo);

            return riesgoInsertado.IdRiesgo;
        }

        private int updateRiesgo(FormGeneralModel datosFormulario)
        {

            List<string> datosQRiesgosNombre = new List<string>();
            List<string> datosQRiesgosEvaluacionesValores = new List<string>();

            PropertyInfo[] props = datosFormulario.GetType().GetProperties();

            foreach (PropertyInfo item in props)
            {
                if (item.GetValue(datosFormulario, null) != null)
                {
                    try
                    {

                        string tabla = item.GetValue(datosFormulario, null).ToString().Split(':')[1];

                        switch (tabla)
                        {
                            case "qRiesgosNombre":
                                datosQRiesgosNombre.Add(item.Name + ":" + item.GetValue(datosFormulario, null).ToString().Split(':')[0]);
                                break;
                            case "qRiesgosEvaluacionesValores":
                                datosQRiesgosEvaluacionesValores.Add(item.Name + ":" + item.GetValue(datosFormulario, null).ToString().Split(':')[0]);
                                break;
                        }

                    }
                    catch (Exception) { }
                }
            }

           return BD_Riesgos.actualizaQRiesgosNombre(datosQRiesgosNombre, int.Parse(datosFormulario.IdRiesgo));
        }

        [HttpPost]
        public JsonResult deleteRiesgo(int id)
        {
            bool flag = BD_Riesgos.deleteRiesgo(id);
            return Json(Url.Action("Risks", "Assign"));
        }



        [HttpPost]
        public ActionResult nuevoRiskDesdeStructure(int idEstructura) {
            return Json(Url.Action("RiskFicha", "Risk", new { id = 0, idEstructura = idEstructura }));
        }


        // Metodo (llamada desde scripts2.js) para recuperar el siguiente riesgo disponible y rellenar Particle Code en un riesgo nuevo
        public string dameUltimoRiesgoDisponible(string idEstructura)
        {
            return BD_Riesgos.ultimoRiesgoDisponible(idEstructura);
        }

        public string recuperaDrop(string nombre)
        {
            DropDownModel dropdown = new DropDownModel();
            Dictionary<int, List<string>> dicEnvio = nombre == ("datosEvaFrecuencia") ? dropdown.datosEvaFrecuencia : dropdown.datosEvaSeveridad;
            var j = JsonConvert.SerializeObject(dicEnvio);
            return j;
        }

    }
}