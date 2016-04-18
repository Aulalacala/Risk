using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Risk.Models;

namespace Risk.ViewModels
{
    public class FichaRiesgoVM
    {
        public qRiesgosNombre qRiesgosNombre_VM { get; set; }
        public tRiesgosEvaluaciones tRiesgosEvaluaciones_VM { get; set; }
        public tEva_Frecuencia tEva_Frecuencia_VM { get; set; }
        public tEva_Severidad tEva_Severidad_VM { get; set; }
    }
}