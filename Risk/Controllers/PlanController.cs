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

        BD_Riesgos BD_Riesgos = new BD_Riesgos();
        string colVer = "CodPlanAccion,Nombre,Medidas,Activa,FechaFinTeorica,FechaFinReal,Responsable";
        string colTitulos = "CodPlanAccion,Nombre,Medidas,Activa,FechaFinTeorica,FechaFinReal,Responsable";

        private DatosTablaModel _datosTablaGeneral = new DatosTablaModel();

        public DatosTablaModel datosTablaGeneral
        {
            get
            {
                _datosTablaGeneral.datosTHead = BD_Riesgos.nombresColTabla("qPlanes", colVer, colTitulos);
                _datosTablaGeneral.datosTBody = null;
                _datosTablaGeneral.titulo = null;
                return this._datosTablaGeneral;
            }
            set
            {
                _datosTablaGeneral = value;
            }
        }




        // GET: Plan
        public ActionResult Plans()
        {
            DatosTablaModel datosTabla = new DatosTablaModel();

            datosTabla.titulo = "ACTION PLANS";

            if (TempData["datosTablaGeneralBusqueda"] != null)
            {
                DatosTablaModel datosTablaGeneralBusqueda = (DatosTablaModel)TempData["datosTablaGeneralBusqueda"];
                datosTabla.datosTBody = datosTablaGeneralBusqueda.datosTBody;
            }
            else
            {
               datosTabla.datosTBody = BD_Riesgos.cargaTablaDatos("qPlanes", colVer, colTitulos);
            }

            datosTabla.editable = true;
            datosTabla.urlActionEditar = new Tuple<string, string>("PlanFicha", "Plan");
            datosTabla.borrar = false;          
            datosTabla.vistaProcedencia = "Plans";

            return View();
        }
    }
}