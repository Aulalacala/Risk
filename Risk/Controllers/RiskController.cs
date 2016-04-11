using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risk.Controllers;
using Risk.Models;

namespace Risk.Controllers
{
    public class RiskController : Controller
    {
        BD_Riesgos BD_Riesgos = new BD_Riesgos();

        // GET: Risk
        public ActionResult RiskFicha(int id)
        {
            qRiesgosNombre riesgoRecup=  BD_Riesgos.recuperarRiesgo(id);
            ViewBag.riesgoRecup = riesgoRecup;
            return View();
        }
    }
}