using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risk.Controllers;
using Risk.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Risk.Controllers
{
    public class AssignController : Controller
    {

        BD_Riesgos BD_Riesgos = new BD_Riesgos();
        string colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3";
        string colTitulos = "Código Riesgo,Nombre, Categoría,Clasificación1,Clasificación2,Clasificación3";

        #region View Structure
        // Vista inicial Structure GET -----------------------------------------------------------
        public ActionResult Structure()
        {
            BD_Riesgos.numeroFilasStructure(0);
            List<tEstructura> datosEstructuraOrdenados = BD_Riesgos.datosEstructuraOrdenados;

            ViewBag.datosEstructura = datosEstructuraOrdenados;

            return View(datosEstructuraOrdenados);
        }



        #endregion

        #region View KrisIndicators
        // Vista inicial GET KrisIndicators ----------------------------------------------
        public ActionResult KRISIndicators()
        {
            return View();
        }
        #endregion

        #region View Risk
        // Vista inicial GET Risk ---------------------------------------------------
        public ActionResult Risks()
        {

            DropDownModel dropdowns = new DropDownModel();

            ViewBag.datosthead = BD_Riesgos.nombresColTabla("dbo.qRiesgosNombres", colVer, colTitulos);

            if (TempData["datostbody"] == null)
            {
                TempData["datostbody"] = BD_Riesgos.datosQRiesgosNombre(colVer, colTitulos);
            }

            return View(dropdowns);
        }


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
            TempData["datostbody"] = BD_Riesgos.datosQRiesgosNombre(colVer, colTitulos, filtro, categoria, clasificacion1, clasificacion2, clasificacion3);
            return RedirectToAction("Risks", "Assign");
        }


        //Ordenar columnas
        public ActionResult SortColumn(int columna)
        {
            Dictionary<int, List<object>> filasActuales = (Dictionary<int, List<object>>)TempData["datostbody"];
            Dictionary<int, List<object>> filasOrdenadas = new Dictionary<int, List<object>>();

            foreach (var riesgo in filasActuales)
            {
                //riesgo.Value[columna];
            }

            return RedirectToAction("Risks", "Assign");
        }

        #endregion

    }
}