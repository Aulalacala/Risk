using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Risk.Controllers
{
    public class PlanController : Controller
    {

        public ActionResult Plans()
        {
            return View();
        }

        public ActionResult BusquedaPlanes(string filtro = null)
        {
            TablaPlanes_Planes tabla = new TablaPlanes_Planes();
            Dictionary<string, object> filtros = new Dictionary<string, object>();

            if (filtro != null)
            {
                filtros.Add("Nombre", filtro);
            }

            DatosTablaModel tablafiltrada = tabla.dameTabla(filtros);

            return PartialView("~/Views/PartialViews/TablaDatos.cshtml", tablafiltrada);
        }

        public ActionResult PlanFicha(string id)
        {
            //Buscar plan en la base de datos
            //Pasarselo a la vista
            return View();
        }
    }
}