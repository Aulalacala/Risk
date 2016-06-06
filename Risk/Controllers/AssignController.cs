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

namespace Risk.Controllers
{
    public class AssignController : Controller
    {

        BD_Riesgos BD_Riesgos = new BD_Riesgos();
        BD_MontaDatosTabledata BD_MontaDatosTabledata = new BD_MontaDatosTabledata();

        string colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
        string colTitulos = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";

        Riesgos_BDDataContext ConexionRiesgos = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();

        /// <summary>
        /// Atributo para cargar inicialmente los datos de la tabla con los riesgos y así no tener que cargar constantemente la cabecera que siempre es igual en este controlador
        /// </summary>
        private DatosTablaModel _datosTablaGeneral = new DatosTablaModel();

        public DatosTablaModel datosTablaGeneral
        {
            get
            {
                _datosTablaGeneral.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosNombres", colVer, colTitulos);
                _datosTablaGeneral.colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
                _datosTablaGeneral.colTitulo = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";
                _datosTablaGeneral.nombreTablaBD = "qRiesgosNombres";
                return this._datosTablaGeneral;
            }
            set
            {
                _datosTablaGeneral = value;
            }
        }

        #region View Structure
        // Vista inicial Structure GET -----------------------------------------------------------
        public ActionResult Structure()
        {
            var locations = TreeviewClass.GetLocations(0);
            return View(locations);
        }

        //Partial View Structure GET
        public ActionResult Description(string id)
        {
            DescriptionStructureModel description = new DescriptionStructureModel();

            if (!string.IsNullOrEmpty(id))
            {
                description = BD_Riesgos.recuperaConteDefEstructura(Int32.Parse(id));
            }
            return PartialView(description);
        }


        public ActionResult TablaDatos(string id)
        {

            DatosTablaModel datosTabla = datosTablaGeneral;
            datosTabla.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos, null, 0, 0, 0, 0, Convert.ToInt32(id));
            datosTabla.editable = true;
            datosTabla.borrar = true;
            datosTabla.urlActionEditar = new Tuple<string, string>("RiskFicha", "Risk");
            datosTabla.urlActionBorrar = new Tuple<string, string>("RiskFicha", "Risk"); // Crear action para borrar fila

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

        public ActionResult AssignMultipleRisks(int idEstructura)
        {

            AssignMultipleRiskVM datosTablas = new AssignMultipleRiskVM();

            DatosTablaModel datosTablaAsignados = new DatosTablaModel();
            //CARGAR TBODY CON LOS RIESGOS ASIGNADOS
            datosTablaAsignados.datosTHead = datosTablaGeneral.datosTHead;
            datosTablaAsignados.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos, null, 0, 0, 0, 0, Convert.ToInt32(idEstructura));
            datosTablaAsignados.titulo = "Riesgos de la estructura";
            datosTablaAsignados.editable = true;
            datosTablaAsignados.borrar = false;
            datosTablaAsignados.urlActionEditar = new Tuple<string, string>("RiskFicha", "Risk");


            DatosTablaModel datosTablaSinAsignar = new DatosTablaModel();
            //CARGAR TBODY CON RIESGOS SIN ASIGNAR
            datosTablaSinAsignar.datosTHead = datosTablaGeneral.datosTHead;
            datosTablaSinAsignar.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos, null, 0, 0, 0, 0, 0, true);
            datosTablaSinAsignar.titulo = "Riesgos sin asignación de estructura";
            datosTablaSinAsignar.editable = true;
            datosTablaSinAsignar.borrar = false;
            datosTablaSinAsignar.urlActionEditar = new Tuple<string, string>("RiskFicha", "Risk");

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
        /// <param name="listaRiesgos"></param> Key = idEstructura, Value = lista de idRiesgos a cambiar
        /// Si es 0 --> los riesgos que entran se quieren desvincular de la estructura // Si es !0 --> es para vincular riesgos a una estructura </param>
        /// <returns>Despues de guardar vuelve redirige a la vista actualizada</returns>
        public ActionResult guardarCambiosMultipleRisk(Dictionary<string, List<string>> listaRiesgos, int idEstructura)
        {

            foreach (var riesgo in listaRiesgos)
            {

                tRiesgos riesgoActualizar = new tRiesgos();

                if (riesgo.Key == "0")
                { // RIESGOS A LOS QUE QUEREMOS QUITAR LA ESTRUCTURA
                    foreach (var id in riesgo.Value)
                    {
                        riesgoActualizar = BD_Riesgos.recuperarTRiesgo(Convert.ToInt32(id));
                        riesgoActualizar.CodRiesgo = null;
                        riesgoActualizar.CodRiesgoLocalizado = null;

                        // Borrar la relación estructura-riesgo. 
                        BD_Riesgos.deleteTRelEstructuraRiesgos(Convert.ToInt32(id));
                    }


                }

                else
                {  // RIESGOS CON IDESTRUCTURA EN LA KEY 
                    foreach (var id in riesgo.Value)
                    {
                        riesgoActualizar = BD_Riesgos.recuperarTRiesgo(Convert.ToInt32(id));
                        riesgoActualizar.CodRiesgo = BD_Riesgos.ultimoRiesgoDisponible(riesgo.Key);
                        riesgoActualizar.CodRiesgoLocalizado = riesgoActualizar.CodRiesgo.Substring(0, 8);

                        // Crear la relación estructura-riesgo. 
                        tRelEstructuraRiesgos relEstructuraRiesgo = new tRelEstructuraRiesgos();
                        relEstructuraRiesgo.IdEstructura = Convert.ToInt32(riesgo.Key);
                        relEstructuraRiesgo.IdRiesgo = Convert.ToInt32(id);
                        BD_Riesgos.insertarTRelEstructuraRiesgoNuevo(relEstructuraRiesgo);
                    }
                }


                BD_Riesgos.updateRiesgo(riesgoActualizar);

            }

            return Json(Url.Action("AssignMultipleRisks", "Assign", new { idEstructura = idEstructura }));
        }
        #endregion

        #region View KrisIndicators
        // Vista inicial GET KrisIndicators ----------------------------------------------
        public ActionResult KRISIndicators()
        {
            DatosTablaModel datosTabla = new DatosTablaModel();
            string colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
            string colTitulos = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";

            Dictionary<int, List<Tuple<string, string>>> dicBody = new Dictionary<int, List<Tuple<string, string>>>();

            if (TempData["datosTablaGeneralBusqueda"] != null)
            {
                DatosTablaModel datosTablaGeneralBusqueda = (DatosTablaModel)TempData["datosTablaGeneralBusqueda"];
                datosTabla.datosTBody = datosTablaGeneralBusqueda.datosTBody;
            }
            else
            {
                //TODO: Unicamente se pasaría el diccionario que devuelve => BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos);
                //Se ha hecho ahora, de esta manera porque aun no existe esta tabla, y asi visualizar datos
                //Asi : ↓↓↓
                //datosTabla.datosTBody =  BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos);

                Dictionary<int, List<Tuple<string, string>>> dic = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos);

                foreach (var item in dic.Take(17))
                {
                    dicBody.Add(item.Key, item.Value);
                }
                datosTabla.datosTBody = dicBody;
            }

            datosTabla.datosTHead = BD_Riesgos.nombresColTabla("qRiesgosNombres", colVer, colTitulos);

            datosTabla.vistaProcedencia = "Scoopes";
            datosTabla.editable = true;
            datosTabla.urlActionEditar = new Tuple<string, string>("KrisFicha", "KRIS");
            datosTabla.borrar = true;
            datosTabla.urlActionBorrar = new Tuple<string, string>("DeleteKris", "KRIS");


            return View(datosTabla);
        }


        public ActionResult BusquedaKRIS(string filtro)
        {
            DatosTablaModel datosTablaGeneralBusqueda = datosTablaGeneral;
            datosTablaGeneralBusqueda.datosTBody = BD_Riesgos.cargaTablaDatos("qRiesgosNombres", colVer, colTitulos, filtro);
            TempData["datosTablaGeneralBusqueda"] = datosTablaGeneralBusqueda;
            return RedirectToAction("KRISIndicators", "Assign");

        }
        #endregion

        #region View Risk
        // Vista inicial GET Risk ---------------------------------------------------
        [HttpGet]
        public ActionResult Risks()
        {
            DropDownModel dropdowns = new DropDownModel();
            DatosTablaModel datosTabla = datosTablaGeneral;

            ViewModelRisk modelVistaRisk = new ViewModelRisk();
            modelVistaRisk.dropDownModel = dropdowns;

            modelVistaRisk.datosTabla = datosTabla;

            modelVistaRisk.datosTabla.titulo = "INSTANCES SEARCH";

            //Si es nulo es xq se acaba de inicializar arriba. Si no lo es, es xq viene cargado desde la busqueda

            Dictionary<int, List<Tuple<string, string>>> datosTbody = new Dictionary<int, List<Tuple<string, string>>>();
            Dictionary < int, List < Tuple < string, string>>> datosDesdeBuscador = (Dictionary<int, List<Tuple<string, string>>>)TempData["datosTBody"];

            if (datosDesdeBuscador == null)
            {
                Dictionary<int, object> datos = BD_MontaDatosTabledata.filtrosRiesgos(null);
                datosTbody = BD_MontaDatosTabledata.cargaTBody(datos, datosTabla.datosTHead);
            }
            else
            {
                datosTbody = datosDesdeBuscador;
            }

            datosTabla.datosTBody = new Dictionary<int, List<Tuple<string, string>>>(datosTbody);
            modelVistaRisk.datosTabla.datosTBody = datosTabla.datosTBody;
            modelVistaRisk.datosTabla.editable = true;
            modelVistaRisk.datosTabla.urlActionEditar = new Tuple<string, string>("RiskFicha", "Risk");
            modelVistaRisk.datosTabla.borrar = false;
            modelVistaRisk.datosTabla.vistaProcedencia = "Risks";

            return View(modelVistaRisk);
        }


        //public Dictionary<int, object> creaDictionaryDatosRiesgos(Dictionary<string, object> filtros)
        //{
        //    Dictionary<int, object> dicDevolver = new Dictionary<int, object>();

        //    if (filtros != null)
        //    {
        //        dicDevolver = BD_MontaDatosTabledata.filtrosRiesgos(filtros, datosTablaGeneral.nombreTablaBD);
        //    }
        //    else
        //    {
        //        dicDevolver = ConexionRiesgos.qRiesgosNombres.ToDictionary(r => r.IdRiesgo, r => (object)r);
        //    }

        //    return dicDevolver;
        //}


        // Recuperar clasificaciones segun idEstructura, llamada desde metodo jquery en fichero Scripts2.js ---------------
        public string recuperaListClasif(int idEstructura)
        {
            DropDownModel dropdown = new DropDownModel();
            Dictionary<int, string> dicClasificacion2 = dropdown.listadoClasifDinamic(idEstructura);
            var j = JsonConvert.SerializeObject(dicClasificacion2);
            return j;
        }


        public ActionResult BusquedaRiks(string filtro, int categoria, int clasificacion1, int clasificacion2, int clasificacion3)
        {


            DatosTablaModel datosTablaGeneralBusqueda = datosTablaGeneral;
            datosTablaGeneral.filtros = new Dictionary<string, object>();

            if (filtro != null)
            {
                datosTablaGeneralBusqueda.filtros.Add("Nombre", filtro);
            }

            if (categoria != 0)
            {
                datosTablaGeneralBusqueda.filtros.Add("IdCategoria", categoria);
            }

            if (clasificacion1 != 0)
            {
                datosTablaGeneralBusqueda.filtros.Add("IdClasificacion1", clasificacion1);
            }

            if (clasificacion2 != 0)
            {
                datosTablaGeneralBusqueda.filtros.Add("IdClasificacion2", clasificacion2);
            }

            if (clasificacion3 != 0)
            {
                datosTablaGeneralBusqueda.filtros.Add("IdClasificacion3", clasificacion3);
            }

            Dictionary<int, object> dicDevolver = BD_MontaDatosTabledata.filtrosRiesgos(datosTablaGeneralBusqueda.filtros);
            TempData["datosTBody"] = BD_MontaDatosTabledata.cargaTBody(dicDevolver, datosTablaGeneral.datosTHead);

            return RedirectToAction("Risks", "Assign");
        }


        #endregion

    }
}