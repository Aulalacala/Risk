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
        #region Vistas
        //Vista Ppal
        public ActionResult Plans()
        {
            return View();
        }

        //Método invocado desde jQuery en Plans.cshtml
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

        //Especificación del plan elegido
        public ActionResult PlanFicha(string id)
        {
            //TODO:PlanFicha Buscar plan en la base de datos Pasarselo a la vista
            return View();
        }
        #endregion
    }
}