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
        CRUDSql_Switch _CRUDSql_Switch = new CRUDSql_Switch();
        CRUDSql _CRUDSql = new CRUDSql();


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
            datosTabla.editable = false;
            datosTabla.borrar = false;            


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
           
            //INSERTAR CON CRUD-SQL
            if (false) {
                insertOrUpdateCRUD(datosFormulario);

            //INSERTAR CON MODELOS DBML
            } else {

                tRiesgos riesgoNuevo = new tRiesgos();

                riesgoNuevo.CodRiesgo = datosFormulario.CodRiesgo != null ? datosFormulario.CodRiesgo : null;
                riesgoNuevo.CodRiesgoLocalizado = datosFormulario.CodRiesgo != null ? datosFormulario.CodRiesgo.Substring(0, 8) : null;
                riesgoNuevo.Nombre = datosFormulario.Nombre != null ? datosFormulario.Nombre : null;
                riesgoNuevo.IdCategoria = datosFormulario.IdCategoria != null ? int.Parse(datosFormulario.IdCategoria) : 0;
                riesgoNuevo.IdClasificacion1 = datosFormulario.IdClasificacion1 != null ? int.Parse(datosFormulario.IdClasificacion1) : 0;
                riesgoNuevo.IdClasificacion2 = datosFormulario.IdClasificacion2 != null ? int.Parse(datosFormulario.IdClasificacion2) : 0;
                riesgoNuevo.IdClasificacion3 = datosFormulario.IdClasificacion3 != null ? int.Parse(datosFormulario.IdClasificacion3) : 0;
                riesgoNuevo.Descripcion = datosFormulario.Descripcion != null ? datosFormulario.Descripcion : null;
                riesgoNuevo.Justificacion = datosFormulario.Justificacion != null ? datosFormulario.Justificacion : null;
                riesgoNuevo.Ejemplo = datosFormulario.Ejemplo != null ? datosFormulario.Ejemplo : null;
                riesgoNuevo.IdSegmentacion1 = datosFormulario.IdSegmentacion1 != null ? int.Parse(datosFormulario.IdSegmentacion1) : 0;
                riesgoNuevo.IdResponsable = datosFormulario.IdResponsable != null ? int.Parse(datosFormulario.IdResponsable) : 0;
                riesgoNuevo.IdSupervisor = datosFormulario.IdResponsable2 != null ? int.Parse(datosFormulario.IdResponsable2) : 0;


                // Insertar el riesgo en la BD (Devuelve el riesgo insertado con el idRiesgo autogenerado)
                tRiesgos idRiesgo = BD_Riesgos.insertarNuevoRiesgo(riesgoNuevo);


                if (datosFormulario.idEstructura != null) {
                    // Crear tRelEstructuraRiesgos
                    tRelEstructuraRiesgos estructuraNuevo = new tRelEstructuraRiesgos();
                    estructuraNuevo.IdRiesgo = idRiesgo.IdRiesgo;
                    estructuraNuevo.IdEstructura = int.Parse(datosFormulario.idEstructura);

                }

                return idRiesgo.IdRiesgo;

            }
         
        }


      

        private int updateRiesgo(FormGeneralModel datosFormulario)
        {

            //INSERTAR CON CRUD-SQL
            if (false) {
                insertOrUpdateCRUD(datosFormulario);

                //INSERTAR CON MODELOS DBML
            } else {

                tRiesgos riesgoUpdate = BD_Riesgos.recuperarTRiesgo(Convert.ToInt32(datosFormulario.IdRiesgo));

                riesgoUpdate.CodRiesgo = datosFormulario.CodRiesgo != null ? datosFormulario.CodRiesgo : riesgoUpdate.CodRiesgo;
                riesgoUpdate.CodRiesgoLocalizado = datosFormulario.CodRiesgo != null ? datosFormulario.CodRiesgo.Substring(0, 8) : riesgoUpdate.CodRiesgoLocalizado;
                riesgoUpdate.Nombre = datosFormulario.Nombre != null ? datosFormulario.Nombre : riesgoUpdate.Nombre;
                riesgoUpdate.IdCategoria = datosFormulario.IdCategoria != null ? int.Parse(datosFormulario.IdCategoria) : riesgoUpdate.IdCategoria;
                riesgoUpdate.IdClasificacion1 = datosFormulario.IdClasificacion1 != null ? int.Parse(datosFormulario.IdClasificacion1) : riesgoUpdate.IdClasificacion1;
                riesgoUpdate.IdClasificacion2 = datosFormulario.IdClasificacion2 != null ? int.Parse(datosFormulario.IdClasificacion2) : riesgoUpdate.IdClasificacion2;
                riesgoUpdate.IdClasificacion3 = datosFormulario.IdClasificacion3 != null ? int.Parse(datosFormulario.IdClasificacion3) : riesgoUpdate.IdClasificacion3;
                riesgoUpdate.Descripcion = datosFormulario.Descripcion != null ? datosFormulario.Descripcion : riesgoUpdate.Descripcion;
                riesgoUpdate.Justificacion = datosFormulario.Justificacion != null ? datosFormulario.Justificacion : riesgoUpdate.Justificacion;
                riesgoUpdate.Ejemplo = datosFormulario.Ejemplo != null ? datosFormulario.Ejemplo : riesgoUpdate.Ejemplo;
                riesgoUpdate.IdSegmentacion1 = datosFormulario.IdSegmentacion1 != null ? int.Parse(datosFormulario.IdSegmentacion1) : riesgoUpdate.IdSegmentacion1;
                riesgoUpdate.IdResponsable = datosFormulario.IdResponsable != null ? int.Parse(datosFormulario.IdResponsable) : riesgoUpdate.IdResponsable;
                riesgoUpdate.IdSupervisor = datosFormulario.IdResponsable2 != null ? int.Parse(datosFormulario.IdResponsable2) : riesgoUpdate.IdSupervisor;

                // Insertar el riesgo en la BD (Devuelve el riesgo insertado con el idRiesgo autogenerado)
                int idRiesgo = BD_Riesgos.updateRiesgo(riesgoUpdate);

                return idRiesgo;
            }
        }



        /// <summary>
        /// Método para hacer insert o update de un riesgo a través de las clases CRUDSql y CRUDSql_Switch con sentencias SQL tradicionales
        /// Entran los datos desde JQuery en botoneraPartials.js al método formGeneral que bifurca a insert o update
        /// </summary>
        /// <param name="datosFormulario"></param>
        private void insertOrUpdateCRUD(FormGeneralModel datosFormulario) {

            PropertyInfo[] props = datosFormulario.GetType().GetProperties();
            List<Tuple<string, string>> listaValues = new List<Tuple<string, string>>();

            foreach (PropertyInfo item in props) {
                if (item.GetValue(datosFormulario, null) != null) {
                    if (item.Name == "IdRiesgo" || item.Name == "idEstructura") { continue; } else {
                        Tuple<string, string> tupla = Tuple.Create(item.Name, item.GetValue(datosFormulario, null).ToString());
                        listaValues.Add(tupla);
                    }
                }
            }

            string tabla = "tRiesgos";

            if (datosFormulario.IdRiesgo.Equals("0")) {
                int idRiesgoNuevoSwitch = _CRUDSql_Switch.insert(tabla, listaValues, "riesgos");
                int idRiesgoNuevo = _CRUDSql.insert(tabla, listaValues, "riesgos");
            } else {
                int idRiesgoUpdateSwitch = _CRUDSql_Switch.update(tabla, listaValues, "riesgos", "IdRiesgo=" + datosFormulario.IdRiesgo);
                int idRiesgoUpdate = _CRUDSql.update(tabla, listaValues, "riesgos", "IdRiesgo=" + datosFormulario.IdRiesgo);
            }
        }




        [HttpPost]
        public JsonResult deleteRiesgo(int id)
        {
            if (false) {
                _CRUDSql.delete("tRiesgos", "IdRiesgo = " + id);
                _CRUDSql_Switch.delete("tRiesgos", "riesgos","IdRiesgo = " + id);
            }

            try {
                bool flag = BD_Riesgos.deleteRiesgo(id);
                return Json(Url.Action("Risks", "Assign"));
            } catch (Exception) { return null; }
           
            
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

        public void guardaEvaluacion(int idEvaluacion)
        {

        }

    }
}