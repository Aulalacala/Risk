﻿using System;
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
        //Este controlador hace referencia a la parte del menu lateral llamada Assign
        //El controlador se divide en tantas regiones como items hay dentro del submenu de Assign

        //Inicialización de variables que harán falta en este controlador
        BD_Riesgos BD_Riesgos = new BD_Riesgos();
        BD_MontaDatosTabledata BD_MontaDatosTabledata = new BD_MontaDatosTabledata();
        Riesgos_BDDataContext ConexionRiesgos = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();

        #region View Structure
        // Vista inicial Structure GET 
        //En esta vista se carga el TreeView donde esta definida la estructura
        public ActionResult Structure()
        {
            var locations = TreeviewClass.GetLocations(0);
            return View(locations);
        }

        //Partial View Structure Description GET
        //Esta partialView se llama a través de jquery => scripts2. A través del método load(), se carga esta partial en el div seleccionado
        //Muestra la descripción del nodo de árbol seleccionado
        public ActionResult Description(string id)
        {
            DescriptionStructureModel description = new DescriptionStructureModel();

            if (!string.IsNullOrEmpty(id))
            {
                description = BD_Riesgos.recuperaConteDefEstructura(Int32.Parse(id));
            }
            return PartialView(description);
        }

        //Partial View Structure TablaDatos GET
        //Esta partialView se llama a través de jquery => scripts2. A través del método load(), se carga esta partial en el div seleccionado
        //Muestra en una tabla los riesgos hijos del nodo seleccionado 
        public ActionResult TablaDatos(string id)
        {
            TablaRiesgos_Risks tabla = new TablaRiesgos_Risks();
            DatosTablaModel tablafiltrada = tabla.dameTablaPorIdEstructura(Convert.ToInt32(id));
            return PartialView("~/Views/PartialViews/TablaDatos.cshtml", tablafiltrada);
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
            //Uso del ViewModel, para pasar varios modelos/objetos a una misma vista
            AssignMultipleRiskVM datosTablas = new AssignMultipleRiskVM();

            TablaRiesgos_Risks tablaAsignados = new TablaRiesgos_Risks();
            DatosTablaModel datosTablaAsignados = tablaAsignados.dameTablaPorIdEstructura(Convert.ToInt32(idEstructura));

            TablaRiesgos_Risks tablaSinAsignar = new TablaRiesgos_Risks();
            Dictionary<string, object> filtro = new Dictionary<string, object>();
            filtro.Add("CodRiesgo", null);
            DatosTablaModel datosTablaSinAsignar = tablaSinAsignar.dameTabla(filtro);

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
            return View();
        }

        //Este método se invoca desde jquery en la vista KRISIndicators.cshtml
        public ActionResult BusquedaKRIS(string filtro = null)
        {
            
            TablaIndicadores_KRIS tabla = new TablaIndicadores_KRIS();
            Dictionary<string, object> filtros = new Dictionary<string, object>();

            if (filtro != null)
            {
                filtros.Add("Indicador", filtro);
            }
          
            DatosTablaModel tablafiltrada = tabla.dameTabla(filtros);

            return PartialView("~/Views/PartialViews/TablaDatos.cshtml", tablafiltrada);
        }
        #endregion

        #region View Risk
        // Vista inicial GET Risk ---------------------------------------------------
        [HttpGet]
        public ActionResult Risks()
        {
            DropDownModel dropdowns = new DropDownModel();
            return View(dropdowns);
        }


        //Este método se invoca desde jquery en la vista Risks.cshtml
        public ActionResult TablaEnRisk(string Nombre = null, int IdCategoria = 0, int IdClasificacion1 = 0, int IdClasificacion2 = 0, int IdClasificacion3 = 0)
        {
            Dictionary<string, object> dictionaryFiltros = new Dictionary<string, object>();

            if (Nombre != null)
            {
                dictionaryFiltros.Add("Nombre", Nombre);
            }

            if (IdCategoria != 0)
            {
                dictionaryFiltros.Add("IdCategoria", IdCategoria);
            }

            if (IdClasificacion1 != 0)
            {
                dictionaryFiltros.Add("IdClasificacion1", IdClasificacion1);
            }

            if (IdClasificacion2 != 0)
            {
                dictionaryFiltros.Add("IdClasificacion2", IdClasificacion2);
            }

            if (IdClasificacion3 != 0)
            {
                dictionaryFiltros.Add("IdClasificacion3", IdClasificacion3);
            }

            TablaRiesgos_Risks tabla = new TablaRiesgos_Risks();
            DatosTablaModel tablafiltrada = tabla.dameTabla(dictionaryFiltros);
            
            return PartialView("~/Views/PartialViews/TablaDatos.cshtml", tablafiltrada);
        }

        // Recuperar clasificaciones segun idEstructura, llamada desde metodo jquery en fichero Scripts2.js ---------------
        public string recuperaListClasif(int idEstructura)
        {
            DropDownModel dropdown = new DropDownModel();
            Dictionary<int, string> dicClasificacion2 = dropdown.listadoClasifDinamic(idEstructura);
            var j = JsonConvert.SerializeObject(dicClasificacion2);
            return j;
        }

          #endregion
    }
}