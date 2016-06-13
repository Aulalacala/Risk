using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risk.ViewModels;

namespace Risk.Controllers
{
    public class PlanController : Controller
    {
        BD_Planes BD_Planes = new BD_Planes();
        
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
        public ActionResult PlanFicha(int id)
        {
            qPlanes planRecuperado = BD_Planes.recuperaPlan(id);
            return View(planRecuperado);
        }

        public ActionResult Main(int id)
        {
            FichaPlanesVM vM = new FichaPlanesVM();
            vM.qPlanes = BD_Planes.recuperaPlan(id);
            vM.dropDowns = new DropDownModel();       
            return PartialView(vM);           
        }

        public ActionResult Scoope(int id)
        {
            return PartialView();
        }
        #endregion
    }
}