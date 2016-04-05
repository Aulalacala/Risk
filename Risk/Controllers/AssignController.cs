using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risk.Controllers;
using Risk.Models;
using System.Web.Script.Serialization;

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
            List<tRiesgos_Categoria> listCategorias = BD_Riesgos.listadoCategorias();
            ViewBag.listCategorias = listCategorias;

            List<tRiesgos_Clasificaciones> listClasif1 = BD_Riesgos.listadoClasif1();
            ViewBag.listClasif1 = listClasif1;

            return View();
        }

        public ActionResult KRISIndicators()
        {
            return View();
        }

        public string recuperaListClasif(int idEstructura)
        {
            List<tRiesgos_Clasificaciones> listClasi2 = BD_Riesgos.listadoClasif2(idEstructura);

            var json = new JavaScriptSerializer().Serialize(listClasi2);
            return json;
        }
        
    }
}