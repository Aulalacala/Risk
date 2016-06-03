using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Risk.Models;

namespace Risk.ViewModels {
    public class OperationalAndReputationalVM {
        public DatosTablaModel datosTablaSuperior { get; set; }
        public DatosTablaModel datosTablaInferior { get; set; }
        public string textArea { get; set; }
    }
}