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
        
        // GET: Assign
        public ActionResult Structure()
        {
            return View();
        }

        public ActionResult Risks()
        {
            Dictionary<int, string> dicCategorias = BD_Riesgos.listadoCategorias();
            ViewBag.dicCategorias = dicCategorias;

            Dictionary<int, string> dicClasificacion1 = BD_Riesgos.listadoClasif1();
            ViewBag.dicClasificacion1 = dicClasificacion1;   

            return View();
        }

        public ActionResult KRISIndicators()
        {
            return View();
        }

        public string recuperaListClasif(int idEstructura)
        {
            Dictionary<int, string> dicClasificacion2 = BD_Riesgos.listadoClasifDinamic(idEstructura);
            var j = JsonConvert.SerializeObject(dicClasificacion2);
            return j;
        }
        
    }
}