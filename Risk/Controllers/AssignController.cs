using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risk.Controllers;
using Risk.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;
using Risk.ViewModels;
using System.Web.Routing;

namespace Risk.Controllers {
    public class AssignController : Controller {

        BD_Riesgos BD_Riesgos = new BD_Riesgos();
        string colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
        string colTitulos = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";



        /// <summary>
        /// Atributo para cargar inicialmente los datos de la tabla con los riesgos y así no tener que cargar constantemente la cabecera que siempre es igual en este controlador
        /// </summary>
        private DatosTablaModel _datosTablaGeneral = new DatosTablaModel();

        public DatosTablaModel datosTablaGeneral {
            get {
                _datosTablaGeneral.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosNombres", colVer, colTitulos);
                _datosTablaGeneral.datosTBody = null;
                _datosTablaGeneral.titulo = null;
                return this._datosTablaGeneral;
            }
            set {
                _datosTablaGeneral = value;
            }
        }


        #region View Structure
        // Vista inicial Structure GET -----------------------------------------------------------
        public ActionResult Structure() {
            var locations = TreeviewClass.GetLocations(0);
            return View(locations);
        }

        //Partial View Structure GET
        public ActionResult Description(string id) {
            DescriptionStructureModel description = new DescriptionStructureModel();

            if (!string.IsNullOrEmpty(id)) {
                description = BD_Riesgos.recuperaConteDefEstructura(Int32.Parse(id));
            }
            return PartialView(description);
        }


        public ActionResult TablaDatos(string id) {

            DatosTablaModel datosTabla = datosTablaGeneral;
            datosTabla.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos, null, 0, 0, 0, 0, Convert.ToInt32(id));

            return PartialView("~/Views/PartialViews/TablaDatos.cshtml", datosTabla);
        }

        #endregion


        #region View AssignMultpleRisks

        /// <summary>
        /// Vista inicial GET AssignMultipleRisks (Llamada desde Structure) ----------------------------------------------
        /// Carga de la tabla con sus correspondientes riesgos
        /// y carga de la tabla con riesgos que no están asignados a ninguna estructura
        /// </summary>
        /// <param name="idEstructura">Desde el btn Assign Multiple Risk en la vista Structure le entra el idEstructura para buscar los riesgos correspondientes</param>
        /// <returns>AssignMultipleRisk.cshtml con los datos de las dos tablas cargados</returns>

        public ActionResult AssignMultipleRisks(int idEstructura) {

            AssignMultipleRiskVM datosTablas = new AssignMultipleRiskVM();

            DatosTablaModel datosTablaAsignados = new DatosTablaModel();
            //Cargar tbody con los riesgos asignados
            datosTablaAsignados.datosTHead = datosTablaGeneral.datosTHead;
            datosTablaAsignados.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos, null, 0, 0, 0, 0, Convert.ToInt32(idEstructura));
            datosTablaAsignados.titulo = "Riesgos de la estructura";


            DatosTablaModel datosTablaSinAsignar = new DatosTablaModel();
            //Cargar tbody con riesgos sin asignar
            datosTablaSinAsignar.datosTHead = datosTablaGeneral.datosTHead;
            datosTablaSinAsignar.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos, null, 0, 0, 0, 0,0, true);
            datosTablaSinAsignar.titulo = "Riesgos sin asignación de estructura";

            datosTablas.datosTablaAsignados = datosTablaAsignados;
            datosTablas.datosTablaSinAsignar = datosTablaSinAsignar;

            DropDownModel dropDownStructure = new DropDownModel();
            datosTablas.dropDownStructure = dropDownStructure;
            datosTablas.idEstructura = idEstructura;

            return View(datosTablas);
        }


        /// <summary>
        ///  idEstructura==0 --> Borrado de CodRiesgo y CodRiesgoLocalizado (null) de tabla tRiesgos y borrado de la tupla con ese idRiesgo en la tabla tRelEstructuraRiesgos
        ///  idEstructura!=0 --> Asignacion del riesgo a la estructura
        ///  Viene de llamada ajax desde vista AssignMultipleRisks
        /// </summary>
        /// <param name="riesgos"></param>
        /// <param name="idEstructura">Si es 0 --> los riesgos que entran se quieren desvincular de la estructura // Si es !0 --> es para vincular riesgos a una estructura </param>
        /// <returns>Despues de guardar vuelve redirige a la vista actualizada</returns>
        public ActionResult guardarCambiosMultipleRisk (List<int> riesgos, int idEstructura) {

            string CodRiesgo = idEstructura != 0 ? "" : null;
            string CodRiesgoLocalizado = idEstructura != 0 ? "" : null;


            return Json(Url.Action("AssignMultipleRisks", "Assign", new { idEstructura = idEstructura }));
        }
        #endregion

        #region View KrisIndicators
        // Vista inicial GET KrisIndicators ----------------------------------------------
        public ActionResult KRISIndicators() {
            return View();
        }
        #endregion

        #region View Risk
        // Vista inicial GET Risk ---------------------------------------------------
        [HttpGet]
        public ActionResult Risks() {



            ViewModelRisk modelVistaRisk = new ViewModelRisk();
            DropDownModel dropdowns = new DropDownModel();

            DatosTablaModel datosTabla = datosTablaGeneral;

            modelVistaRisk.datosTabla = datosTabla;
            modelVistaRisk.dropDownModel = dropdowns;

            modelVistaRisk.datosTabla.titulo = "INSTANCES SEARCH";

            if (TempData["datosTablaGeneralBusqueda"] != null) {
                DatosTablaModel datosTablaGeneralBusqueda = (DatosTablaModel)TempData["datosTablaGeneralBusqueda"];
                modelVistaRisk.datosTabla.datosTBody = datosTablaGeneralBusqueda.datosTBody;
            } else {
                modelVistaRisk.datosTabla.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos);
            }

            modelVistaRisk.datosTabla.editable = true;
            modelVistaRisk.datosTabla.borrar = false;
            modelVistaRisk.datosTabla.urlActionEditar = new Tuple<string, string>("RiskFicha", "Risk");
            modelVistaRisk.datosTabla.vistaProcedencia = "Risks";
            
            return View(modelVistaRisk);
        }

     

        // Recuperar clasificaciones segun idEstructura, llamada desde metodo jquery en fichero Scripts2.js ---------------
        public string recuperaListClasif(int idEstructura) {
            DropDownModel dropdown = new DropDownModel();
            Dictionary<int, string> dicClasificacion2 = dropdown.listadoClasifDinamic(idEstructura);
            var j = JsonConvert.SerializeObject(dicClasificacion2);
            return j;
        }


        public ActionResult BusquedaRiks(string filtro, int categoria, int clasificacion1, int clasificacion2, int clasificacion3) {
            DatosTablaModel datosTablaGeneralBusqueda = datosTablaGeneral;
            datosTablaGeneralBusqueda.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos, filtro, categoria, clasificacion1, clasificacion2, clasificacion3);
            TempData["datosTablaGeneralBusqueda"] = datosTablaGeneralBusqueda;
            return RedirectToAction("Risks", "Assign");

        }


        #endregion

    }
}