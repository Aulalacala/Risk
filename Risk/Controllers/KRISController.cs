using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Risk.Controllers
{
    public class KRISController : Controller
    {

        BD_Riesgos BD_Riesgos = new BD_Riesgos();

        // GET: KRIS
        public ActionResult KrisFicha(int id)
        {
            qRiesgosNombres riesgoRecup = BD_Riesgos.recuperarQriesgoNombre(id);
            return View(riesgoRecup);
        }
    }
}