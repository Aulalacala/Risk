using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Models
{
    public class DatosTablaModel
    {
        public string titulo { get; set; }
        public Dictionary<string, string> datosTHead { get; set; }
        public Dictionary<int, List<Tuple<string,string>>> datosTBody { get; set; }
        public string vistaProcedencia { get; set; }
        public bool editable { get; set; }
        public bool borrar { get; set; }
        public Tuple<string,string> urlActionEditar { get; set; }
        public Tuple<string, string> urlActionBorrar { get; set; }
        //La tupla se montaria => item1 = vista // item2 = controlador
    }
}