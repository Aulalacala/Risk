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
        public qRiesgos_Evaluaciones__Valores qRiesgos_Evaluaciones_Valores_VM { get; set; }
    
    }
}