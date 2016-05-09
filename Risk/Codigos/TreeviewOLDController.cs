using Risk.Controllers;
using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Risk.Codigos
{
    public class TreeviewOLDController : Controller
    {
        BD_Riesgos BD_Riesgos = new BD_Riesgos();
        //Riesgos_BDDataContext riesgosBD = new Riesgos_BDDataContext();

        ConnectionDB.connectionRiesgos riesgosBD = new ConnectionDB.connectionRiesgos();

        string colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
        string colTitulos = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";

        public List<tEstructura> _datosEstructuraOrdenados = new List<tEstructura>();




        #region View Structure
        // Vista inicial Structure GET -----------------------------------------------------------
        public ActionResult Structure()
        {
            numeroFilasStructure(0);
            List<tEstructura> datosEstructuraOrdenados = _datosEstructuraOrdenados;
            ViewBag.datosEstructura = pintaLista(datosEstructuraOrdenados);

            ViewBag.datosthead = BD_Riesgos.nombresColTabla("dbo.qRiesgosNombres", colVer, colTitulos);

            if (TempData["datostbody"] == null)
            {
                TempData["datostbody"] = null;
            }

            return View();
        }

        //Metodos de Structure
        public string pintaLista(List<tEstructura> listaOrdenada)
        {
            StringBuilder listaString = new StringBuilder();
            for (int i = 0; i < listaOrdenada.Count(); i++)
            {

                string tieneHijos = compruebaTieneHijos(listaOrdenada[i].CodCompleto);

                if (listaOrdenada[i].Nivel == 1)
                {
                    listaString.Append("<ul id='miEstructura'>"
                                       + "<li>"
                                       + "<input type='checkbox'" + tieneHijos + "/>"
                                       + "<input type='submit' class='buttonStructure' name='codigo' id='" + listaOrdenada[i].IdEstructura + "' value='" + listaOrdenada[i].CodCompleto + " " + listaOrdenada[i].Nombre + "' />"
                                       + "<ul>");
                }

                if (listaOrdenada[i].Nivel == 2)
                {
                    listaString.Append("<li>"
                                       + "<input type='checkbox'" + tieneHijos + "/>"
                                       + "<input type='submit' class='buttonStructure' name='codigo'  value='" + listaOrdenada[i].CodCompleto + " " + listaOrdenada[i].Nombre + "'/>"
                                       + "<ul>");
                }

                if (listaOrdenada.Count() - 1 != i)
                {
                    if (listaOrdenada[i].Nivel == 3 && listaOrdenada[i + 1].Nivel == 3)
                    {

                        listaString.Append("<li>"
                                          + "<input type='checkbox'" + tieneHijos + "/>"
                                           + "<input type='submit' class='buttonStructure' name='codigo'  value='" + listaOrdenada[i].CodCompleto + " " + listaOrdenada[i].Nombre + "'/>"
                                           + "</li>");
                    }

                    else if (listaOrdenada[i].Nivel == 3 && listaOrdenada[i + 1].Nivel == 2)
                    {
                        listaString.Append("<li>"
                                          + "<input type='checkbox'" + tieneHijos + "/>"
                                          + "<input type='submit' class='buttonStructure' name='codigo'  value='" + listaOrdenada[i].CodCompleto + " " + listaOrdenada[i].Nombre + "'/>"
                                           + "</li></ul></li>");
                    }
                }
                else
                {
                    listaString.Append("<li>"
                                       + "<input type='checkbox'" + tieneHijos + "/>"
                                       + "<input type='submit' class='buttonStructure' name='codigo'  value='" + listaOrdenada[i].CodCompleto + " " + listaOrdenada[i].Nombre + "'/>"
                                       + "</li></ul></li>");
                }
            }
            listaString.Append("</ul></li></ul>");
            return listaString.ToString();
        }




        // ---------- METODOS EN EL CONTROLADOR DE LA BASE DE DATOS A LOS QUE SE LLAMAN DESDE EL CONTROLADOR PARA CONSTRUIR EL TREEVIEW ------------- 


        //

        public void numeroFilasStructure(int id)
        {
            List<tEstructura> cuantosHay = riesgosBD.DB.tEstructura.Where(r => r.idPadre == id).OrderBy(r => r.Orden).ToList();
            if (cuantosHay.Count != 0)
            {
                //ul
                for (int i = 0; i < cuantosHay.Count; i++)
                {
                    _datosEstructuraOrdenados.Add(cuantosHay[i]);
                    numeroFilasStructure(cuantosHay[i].IdEstructura);
                }
            }
        }



        public string compruebaTieneHijos(string codCompleto)
        {
            string tieneHijos = "";
            return tieneHijos = riesgosBD.DB.tRiesgos.Where(r => r.CodRiesgo.Contains(codCompleto)).Any() ? tieneHijos = "checked" : tieneHijos = "";
        }
    }
}
#endregion





// -------------------- MOSTRAR EL TREEVIEW EN LA VISTA --------------------------------------------------

//<div class="col-lg-4 col-md-4 pull-left" style="margin:0;padding:0">
//                    @using(Html.BeginForm("recuperaRiesgos", "Assign", FormMethod.Post))
//                    {
//                        @Html.AntiForgeryToken()
//                        <div class="col-lg-12 col-md-12"  style="width:100%">
//                            @Html.Raw(ViewBag.datosEstructura)
//                        </div>

//                    }
// </div>