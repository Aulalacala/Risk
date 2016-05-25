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
        /// Vista de detalles de un riesgo. Puede ser de tres formas:
        /// 1. Detalles de un riesgo ya existente --> llamada desde la tabla de Risk.cshtml (BtnEditar)
        /// 2. En blanco para crear uno nuevo --> llamada desde botoneraPartials.js (BtnNew de la partial view General.cshtml)
        /// 3. En blanco pero con estructura indicada para crear uno nuevo --> llamada desde Structure.cshtml
        /// </summary>
        /// <param name="id">idRiesgo para localizar los datos de ese riesgo en la BD / Si es 0 creará una vista limpia para crear un riesgo nuevo</param>
        /// <param name="idEstructura">Opcional, para cuando se le llama desde la forma 3</param>
        /// <returns></returns>
        public ActionResult RiskFicha(int id, int idEstructura = 0)
        {
            qRiesgosNombres riesgoRecup = BD_Riesgos.recuperarRiesgo(id);
            ViewBag.idEstructura = idEstructura;
            return View(riesgoRecup);
        }




        public ActionResult General(int id, int idEstructura = 0)
        {

          int idUltimaEvaluacion = BD_Riesgos.recuperaIdUltimaEvaluacion(id);
          FichaRiesgoVM fichaRiesgoVM = montaVM(id, idUltimaEvaluacion); //recuperar el idEvaluacion de la última evaluacion de ese riesgo
          
            if (idEstructura != 0)
            {
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

            DatosTablaModel datosTabla = new DatosTablaModel();
            datosTabla.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosEvalVal", colVer, colTitulos);
            datosTabla.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosEvalVal", colVer, colTitulos, null, 0, 0, 0, 0, Convert.ToInt32(id));
            datosTabla.vistaProcedencia = "Historical";

            fichaRiesgoVM.datosTabla_VM = datosTabla;
            fichaRiesgoVM.referencia = 0;

            return PartialView(fichaRiesgoVM);
        }


        public ActionResult FinancialImpactCombos(int id)
        {
            FichaRiesgoVM fichaRiesgoVM = montaVM(id);
            return PartialView(fichaRiesgoVM);
        }

        public ActionResult FinancialImpactTextBox(int id, int idEvaluacion = 0)
        {
            FichaRiesgoVM fichaRiesgoVM = new FichaRiesgoVM();

            if (idEvaluacion != 0)
            {
                qRiesgosEvalVal qRiesgosEvalValEspecifico = new qRiesgosEvalVal();
                fichaRiesgoVM = montaVM(id, idEvaluacion);
                fichaRiesgoVM.idEvaluacion = idEvaluacion;
            }else
            {
                fichaRiesgoVM = montaVM(id);
            }
            return PartialView(fichaRiesgoVM);
        }

        public ActionResult FinancialImpactCombosHelpers(int id, int idEvaluacion = 0)
        {
            FichaRiesgoVM fichaRiesgoVM = new FichaRiesgoVM();

            if (idEvaluacion != 0)
            {
                qRiesgosEvalVal qRiesgosEvalValEspecifico = new qRiesgosEvalVal();
                fichaRiesgoVM = montaVM(id, idEvaluacion);
                fichaRiesgoVM.idEvaluacion = idEvaluacion;
            }
            else
            {
                fichaRiesgoVM = montaVM(id);

                Dictionary<int, qRiesgosEvalVal> dicEvaluaciones = new Dictionary<int, qRiesgosEvalVal>();
                dicEvaluaciones.Add(0, new qRiesgosEvalVal());

                fichaRiesgoVM.qRiesgosEvalVal_Dic_VM = dicEvaluaciones;
                fichaRiesgoVM.idEvaluacion = 0;
            }
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

        public FichaRiesgoVM montaVM(int id, int idEvaluacion = 0)
        {
            FichaRiesgoVM fichaRiesgoVM = new FichaRiesgoVM();

            qRiesgosNombres riesgoRecup = new qRiesgosNombres(); 

            
            if (id != 0)
            {
                riesgoRecup = BD_Riesgos.recuperarRiesgo(id);
            }


            if (idEvaluacion == 0)
            {
                Dictionary<int, qRiesgosEvalVal> dicEvaluaciones = new Dictionary<int, qRiesgosEvalVal>();
                qRiesgosEvalVal evaluacion = new qRiesgosEvalVal();
                evaluacion.IdFrecAntes = 0;
                evaluacion.IdSeveAntes = 0;
                evaluacion.IdFrecDespues = 0;
                evaluacion.IdSeveDespues = 0;
                evaluacion.IdSevePeorAntes = 0;
                evaluacion.IdSevePeorDespues = 0;
                evaluacion.IdEfectividad = 0;
                evaluacion.Efectividad = 0;
                evaluacion.IdFrecPlanDespues = 0;
                evaluacion.IdSevePlanDespues = 0;
                evaluacion.IdSevePeorPlanDespues = 0;

                dicEvaluaciones.Add(0, evaluacion);
                fichaRiesgoVM.qRiesgosEvalVal_Dic_VM = dicEvaluaciones;
                fichaRiesgoVM.idEvaluacion = 0;
            }else
            {
                fichaRiesgoVM.qRiesgosEvalVal_Dic_VM = BD_Riesgos.recuperaEvaluaciones(id, idEvaluacion);
            }


            fichaRiesgoVM.qRiesgosNombre_VM = riesgoRecup;
            //fichaRiesgoVM.qRiesgosEvalVal_VM = evaluaciones;


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

            if (datosFormulario.IdRiesgo.Equals("0"))
            {
                idRiesgo = insertarNuevoRiesgo(datosFormulario);
            }

            else
            {
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

            //BD_Riesgos.insertarTRiesgosEvaluaciones(riesgoInsertado.IdRiesgo);

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
        public ActionResult nuevoRiskDesdeStructure(int idEstructura)
        {
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