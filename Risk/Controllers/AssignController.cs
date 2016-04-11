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

        #region View Structure
        // Vista inicial Structure GET -----------------------------------------------------------
        public ActionResult Structure()
        {
            return View();
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
            Dictionary<int, string> dicCategorias = BD_Riesgos.listadoCategorias();
            ViewBag.dicCategorias = dicCategorias;

            Dictionary<int, string> dicClasificacion1 = BD_Riesgos.listadoClasif1();
            ViewBag.dicClasificacion1 = dicClasificacion1;

            ViewBag.datosthead = BD_Riesgos.nombresColTabla("dbo.qRiesgosNombres");

            if (TempData["datostbody"] == null)
            {
                TempData["datostbody"] = BD_Riesgos.datosQRiesgosNombre();

            }

            return View();
        }


        // Recuperar clasificaciones segun idEstructura, llamada desde metodo jquery en fichero Scripts2.js ---------------
        public string recuperaListClasif(int idEstructura)
        {
            Dictionary<int, string> dicClasificacion2 = BD_Riesgos.listadoClasifDinamic(idEstructura);
            var j = JsonConvert.SerializeObject(dicClasificacion2);
            return j;
        }



        public ActionResult BusquedaRiks(string filtro, int categoria, int clasificacion1, int clasificacion2, int clasificacion3)
        {
            TempData["datostbody"] = BD_Riesgos.datosQRiesgosNombre(filtro, categoria, clasificacion1, clasificacion2, clasificacion3);

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