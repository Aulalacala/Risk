using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Risk.Models;

namespace Risk.ViewModels
{
    public class FichaRiesgoVM
    {
        public qRiesgosNombres qRiesgosNombre_VM { get; set; }
        //public qRiesgosEvalVal qRiesgosEvalVal_VM { get; set; }

        public Dictionary<int, qRiesgosEvalVal> qRiesgosEvalVal_Dic_VM { get; set; }
        public DropDownModel dropDowns { get; set; }
        public DatosTablaModel datosTabla_VM { get; set; }
        public int referencia { get; set; }

        public qRiesgosEvalVal qRiesgosEvalValNuevo
        {
            get
            {
                qRiesgosEvalVal evaluacionNueva = new qRiesgosEvalVal();
                return evaluacionNueva;
            }
            set
            {
                qRiesgosEvalValNuevo = value;
            }
        }
        public int idEvaluacion { get; set; }
    }
}