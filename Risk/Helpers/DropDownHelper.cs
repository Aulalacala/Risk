using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Risk.Helpers
{
    public static class DropDownHelper
    {

        /// <summary>
        /// Retorna un dropDown cargado con opciones coloreadas
        /// </summary>
        /// <param name="helper">Helper</param>
        /// <param name="datos">Dictionary con las opciones del select</param>
        /// <param name="htmlAtributes">Atributos de la etiqueta select</param>
        /// <returns></returns>


        public static MvcHtmlString DropDown(this HtmlHelper helper, Dictionary<int, List<string>> datos, string idCombo, string tabla, string idColor, string css = null, string cboSiguiente = "")
        {
            var combo = new TagBuilder("select");

            if (css == null)
            {
                combo.AddCssClass("form-control w100");
            }
            else
            {
                combo.AddCssClass(css);
            }

            combo.GenerateId(idCombo);
            combo.MergeAttribute("tabla", tabla);
            combo.MergeAttribute("campos", "campos");
            combo.MergeAttribute("combo", "true");
            //combo.MergeAttribute("style", "color:#000000");
            combo.MergeAttribute("sig", cboSiguiente);

            /*Añade las opciones del tag select
            El dictionary está cargado de la siquiente manera 
                => {
                    "1" : ["#color", "Nombre"],
                    "2" : ["#color", "Nombre"]
                    }
            */

            foreach (var op in datos)
            {
                var option = new TagBuilder("option");
                if (op.Key.ToString().Equals(idColor))
                {
                    option.MergeAttribute("selected", "selected");
                    combo.MergeAttribute("style", "color:#000000; background-color:" + op.Value[0]);
                }

                option.MergeAttribute("value", op.Key.ToString());
                option.MergeAttribute("style", "color:#000000; background-color:" + op.Value[0]);
                option.InnerHtml = op.Value[1];

                combo.InnerHtml += option.ToString();
            }


            return MvcHtmlString.Create(combo.ToString(TagRenderMode.Normal));
        }
    }
}