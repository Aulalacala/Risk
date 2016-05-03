using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risk.Controllers;
using Risk.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;

namespace Risk.Controllers
{
    public class AssignController : Controller
    {

        BD_Riesgos BD_Riesgos = new BD_Riesgos();
        string colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
        string colTitulos = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";

        #region View Structure
        // Vista inicial Structure GET -----------------------------------------------------------
        public ActionResult Structure()
        {        
            var locations = TreeviewClass.GetLocations(0);

            ViewBag.datosthead = BD_Riesgos.nombresColTabla("dbo.qRiesgosNombres", colVer, colTitulos);
            if (TempData["datostbody"] == null)
            {
                TempData["datostbody"] = null;
            }
            return View(locations);
        }

        //Partial View Structure GET
        public ActionResult Description(string id)
        {
            DescriptionStructureModel description = new DescriptionStructureModel();

            if (!string.IsNullOrEmpty(id))
            {
                description = BD_Riesgos.recuperaConteDefEstructura(Int32.Parse(id));
                      
            }

            return PartialView(description);

        }


        public ActionResult recuperaRiesgos(int idEstructura, string name)
        {
            TempData["titulo"] = name;
            ViewBag.datosthead = BD_Riesgos.nombresColTabla("dbo.qRiesgosNombres", colVer, colTitulos);
            TempData["datostbody"] = BD_Riesgos.datosQRiesgosNombre(colVer, colTitulos, null, 0,0,0,0,idEstructura);
            
            return PartialView();
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
            TempData["titulo"] = "INSTANCES SEARCH";
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


        #endregion

    }
}