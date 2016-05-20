using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Risk.Models;

namespace Risk.ViewModels {
    public class AssignMultipleRiskVM {

        public DatosTablaModel datosTablaSinAsignar { get; set; }
        public DatosTablaModel datosTablaAsignados { get; set; }
    }
}