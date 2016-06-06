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
            qRiesgosNombres riesgoRecup = BD_Riesgos.recuperarQriesgoNombre(id);
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


        public ActionResult OperationalImpact(int id)
        {
            OperationalAndReputationalVM datosOperational = new OperationalAndReputationalVM();

            DatosTablaModel datosTablaSuperior = new DatosTablaModel();
            DatosTablaModel datosTablaInferior = new DatosTablaModel();

            string colVerOpe = "Activa,Fecha,NombreFrecAntes";
            string colTitulosOpe = "Activa,Fecha,FrecuenciaA";

            datosTablaSuperior.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosEvalVal", colVerOpe, colTitulosOpe);
            datosTablaSuperior.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosEvalVal", colVerOpe, colTitulosOpe, null, 0, 0, 0, 0, Convert.ToInt32(id));
            datosTablaSuperior.vistaProcedencia = "Operational Impact";
            datosTablaSuperior.editable = false;
            datosTablaSuperior.borrar = false;

            datosTablaInferior.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosEvalVal", colVerOpe, colTitulosOpe);
            datosTablaInferior.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosEvalVal", colVerOpe, colTitulosOpe, null, 0, 0, 0, 0, Convert.ToInt32(id));
            datosTablaInferior.vistaProcedencia = "Operational Impact";
            datosTablaInferior.editable = false;
            datosTablaInferior.borrar = false;

            datosOperational.datosTablaSuperior = datosTablaSuperior;
            datosOperational.datosTablaInferior = datosTablaInferior;

            return PartialView(datosOperational);
        }


        public ActionResult ReputationalImpact(int id) {
            OperationalAndReputationalVM datosOperational = new OperationalAndReputationalVM();

            DatosTablaModel datosTablaSuperior = new DatosTablaModel();
            DatosTablaModel datosTablaInferior = new DatosTablaModel();

            string colVerOpe = "Activa,Fecha,NombreFrecAntes";
            string colTitulosOpe = "Activa,Fecha,FrecuenciaA";

            datosTablaSuperior.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosEvalVal", colVerOpe, colTitulosOpe);
            datosTablaSuperior.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosEvalVal", colVerOpe, colTitulosOpe, null, 0, 0, 0, 0, Convert.ToInt32(id));
            datosTablaSuperior.vistaProcedencia = "Operational Impact";
            datosTablaSuperior.editable = false;
            datosTablaSuperior.borrar = false;

            datosTablaInferior.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosEvalVal", colVerOpe, colTitulosOpe);
            datosTablaInferior.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosEvalVal", colVerOpe, colTitulosOpe, null, 0, 0, 0, 0, Convert.ToInt32(id));
            datosTablaInferior.vistaProcedencia = "Operational Impact";
            datosTablaInferior.editable = false;
            datosTablaInferior.borrar = false;

            datosOperational.datosTablaSuperior = datosTablaSuperior;
            datosOperational.datosTablaInferior = datosTablaInferior;

            return PartialView(datosOperational);
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


        public ActionResult Scoope()
        {

            string colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
            string colTitulos = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";

            DatosTablaModel datosTabla = new DatosTablaModel();
            datosTabla.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosNombres", colVer, colTitulos);

            Dictionary<int, List<Tuple<string, string>>> dicBody = new Dictionary<int, List<Tuple<string, string>>>();
            Dictionary<int, List<Tuple<string, string>>> dic = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos);

            //TODO: Unicamente se pasaría el diccionario que devuelve => BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos);
            //Se ha hecho ahora, de esta manera porque aun no existe esta tabla, y asi visualizar datos
            //Asi : ↓↓↓
            // datosTabla.datosTBody =  BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos);

            foreach (var item in dic.Take(3))
            {
                dicBody.Add(item.Key, item.Value);
            }

            datosTabla.datosTBody = dicBody;
            datosTabla.vistaProcedencia = "Scoopes";
            datosTabla.editable = false;
            datosTabla.borrar = false;

            return PartialView(datosTabla);
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
            }
            else
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

        public ActionResult ActiveEfectivityDate(int id, int idEvaluacion = 0)
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

        public FichaRiesgoVM montaVM(int id, int idEvaluacion = 0)
        {
            FichaRiesgoVM fichaRiesgoVM = new FichaRiesgoVM();

            qRiesgosNombres riesgoRecup = new qRiesgosNombres();


            if (id != 0)
            {
                riesgoRecup = BD_Riesgos.recuperarQriesgoNombre(id);
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
                evaluacion.idEfectividad = 0;
                //evaluacion.Efectividad = 0;
                evaluacion.IdFrecPlanDespues = 0;
                evaluacion.IdSevePlanDespues = 0;
                evaluacion.IdSevePeorPlanDespues = 0;

                dicEvaluaciones.Add(0, evaluacion);
                fichaRiesgoVM.qRiesgosEvalVal_Dic_VM = dicEvaluaciones;
                fichaRiesgoVM.idEvaluacion = 0;
            }
            else
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

        #region RIESGOS Form General

        [HttpPost]
        public ActionResult formGeneral(FormGeneralModel datosFormulario)
        {
            tRiesgos riesgoUpdate = new tRiesgos();

            if(!datosFormulario.IdRiesgo.Equals("0")) {
                riesgoUpdate = BD_Riesgos.recuperarTRiesgo(Convert.ToInt32(datosFormulario.IdRiesgo));
            }

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


            // Si el idRiesgo es igual a 0 => insertar
            if (datosFormulario.IdRiesgo.Equals("0")) {
                BD_Riesgos.insertarNuevoRiesgo(riesgoUpdate);

                if (datosFormulario.idEstructura != null) {
                    // Crear tRelEstructuraRiesgos
                    tRelEstructuraRiesgos estructuraNuevo = new tRelEstructuraRiesgos();
                    estructuraNuevo.IdRiesgo = riesgoUpdate.IdRiesgo;
                    estructuraNuevo.IdEstructura = int.Parse(datosFormulario.idEstructura);
                }
            }

            // idRiesgo != 0 => update
            else {
                BD_Riesgos.updateRiesgo(riesgoUpdate);
            }

            return Json(Url.Action("RiskFicha", "Risk", new { id = riesgoUpdate.IdRiesgo }));
        }


        /// <summary>
        /// Método para hacer insert o update de un riesgo a través de las clases CRUDSql y CRUDSql_Switch con sentencias SQL tradicionales
        /// Entran los datos desde JQuery en botoneraPartials.js al método formGeneral que bifurca a insert o update
        /// </summary>
        /// <param name="datosFormulario"></param>
        private void insertOrUpdateCRUD(FormGeneralModel datosFormulario)
        {
            PropertyInfo[] props = datosFormulario.GetType().GetProperties();
            List<Tuple<string, string>> listaValues = new List<Tuple<string, string>>();

            foreach (PropertyInfo item in props)
            {
                if (item.GetValue(datosFormulario, null) != null)
                {
                    if (item.Name == "IdRiesgo" || item.Name == "idEstructura") { continue; }
                    else
                    {
                        Tuple<string, string> tupla = Tuple.Create(item.Name, item.GetValue(datosFormulario, null).ToString());
                        listaValues.Add(tupla);
                    }
                }
            }

            string tabla = "tRiesgos";

            if (datosFormulario.IdRiesgo.Equals("0"))
            {
                int idRiesgoNuevoSwitch = _CRUDSql_Switch.insert(tabla, listaValues, "riesgos");
                int idRiesgoNuevo = _CRUDSql.insert(tabla, listaValues, "riesgos");
            }
            else
            {
                int idRiesgoUpdateSwitch = _CRUDSql_Switch.update(tabla, listaValues, "riesgos", "IdRiesgo=" + datosFormulario.IdRiesgo);
                int idRiesgoUpdate = _CRUDSql.update(tabla, listaValues, "riesgos", "IdRiesgo=" + datosFormulario.IdRiesgo);
            }
        }

        [HttpPost]
        public JsonResult deleteRiesgo(int id)
        {
            if (false)
            {
                _CRUDSql.delete("tRiesgos", "IdRiesgo = " + id);
                _CRUDSql_Switch.delete("tRiesgos", "riesgos", "IdRiesgo = " + id);
            }
            try
            {
                bool flag = BD_Riesgos.deleteRiesgo(id);
                return Json(Url.Action("Risks", "Assign"));
            }
            catch (Exception) { return null; }
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
        #endregion

        #region HISTORICAL

        [HttpPost]
        public string guardaEvaluacion(tRiesgosEvaluaciones evaluacion)
        {

            tRiesgosEvaluaciones evaluacionRecuperada = new tRiesgosEvaluaciones();
            if (evaluacion.IdEvaluacion != 0)
            {
                evaluacionRecuperada = BD_Riesgos.recuperaTRiesgosEvaluacion(evaluacion.IdEvaluacion);

            }
            updateEvaluacion(evaluacionRecuperada, evaluacion);

            return JsonConvert.SerializeObject(evaluacion.IdRiesgo);
        }



        public bool updateEvaluacion(tRiesgosEvaluaciones evaluacionRecuperada, tRiesgosEvaluaciones evaluacion)
        {
            evaluacionRecuperada.IdRiesgo = evaluacion.IdRiesgo;
            evaluacionRecuperada.IdNivel = evaluacion.IdNivel != null ? evaluacion.IdNivel : evaluacionRecuperada.IdNivel;
            evaluacionRecuperada.Fecha = evaluacion.Fecha != null ? evaluacion.Fecha : evaluacionRecuperada.Fecha;
            evaluacionRecuperada.Activa = evaluacion.Activa != evaluacionRecuperada.Activa ? evaluacion.Activa : evaluacionRecuperada.Activa;
            evaluacionRecuperada.Ultima = evaluacion.Ultima != evaluacionRecuperada.Ultima ? evaluacion.Ultima : evaluacionRecuperada.Ultima;
            evaluacionRecuperada.IdFrecAntes = evaluacion.IdFrecAntes != null ? evaluacion.IdFrecAntes : evaluacionRecuperada.IdFrecAntes;
            evaluacionRecuperada.IdSeveAntes = evaluacion.IdSeveAntes != null ? evaluacion.IdSeveAntes : evaluacionRecuperada.IdSeveAntes;
            evaluacionRecuperada.IdFrecDespues = evaluacion.IdFrecDespues != null ? evaluacion.IdFrecDespues : evaluacionRecuperada.IdFrecDespues;
            evaluacionRecuperada.IdSeveDespues = evaluacion.IdSeveDespues != null ? evaluacion.IdSeveDespues : evaluacionRecuperada.IdSeveDespues;
            evaluacionRecuperada.IdSevePeorAntes = evaluacion.IdSevePeorAntes != null ? evaluacion.IdSevePeorAntes : evaluacionRecuperada.IdSevePeorAntes;
            evaluacionRecuperada.IdSevePeorDespues = evaluacion.IdSevePeorDespues != null ? evaluacion.IdSevePeorDespues : evaluacionRecuperada.IdSevePeorDespues;
            evaluacionRecuperada.idEfectividad = evaluacion.idEfectividad != null ? evaluacion.idEfectividad : evaluacionRecuperada.idEfectividad;
            evaluacionRecuperada.Efectividad = evaluacion.Efectividad != null ? evaluacion.Efectividad : evaluacionRecuperada.Efectividad;
            evaluacionRecuperada.IdFrecPlanDespues = evaluacion.IdFrecPlanDespues != null ? evaluacion.IdFrecPlanDespues : evaluacionRecuperada.IdFrecPlanDespues;
            evaluacionRecuperada.IdSevePlanDespues = evaluacion.IdSevePlanDespues != null ? evaluacion.IdSevePlanDespues : evaluacionRecuperada.IdSevePlanDespues;
            evaluacionRecuperada.IdSevePeorPlanDespues = evaluacion.IdSevePeorPlanDespues != null ? evaluacion.IdSevePeorPlanDespues : evaluacionRecuperada.IdSevePeorPlanDespues;

            try
            {

                if (evaluacionRecuperada.IdEvaluacion != 0)
                {
                    BD_Riesgos.updateTRiesgsEvaluaciones(evaluacionRecuperada);

                }
                else
                {
                    evaluacionRecuperada.Ultima = true;
                    bool cambioUltimas = BD_Riesgos.cambiaUltimasAFalseEvaluaciones(Convert.ToInt32(evaluacion.IdRiesgo));
                    if (cambioUltimas)
                    {
                        BD_Riesgos.insertarTRiesgosEvaluaciones(evaluacionRecuperada);
                    }

                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }

        [HttpPost]
        public string recuperaIdUltimaEvaluacion(int id)
        {
            int idRecuerado = BD_Riesgos.recuperaIdUltimaEvaluacion(id);
            return JsonConvert.SerializeObject(idRecuerado);
        }

        [HttpPost]
        public string deleteEvaluacion(int idRiesgo, int idEvaluacion)
        {
            bool delete = BD_Riesgos.deleteEvaluacion(idRiesgo, idEvaluacion);
            return JsonConvert.SerializeObject(delete); ;
        }
        #endregion
    }
}