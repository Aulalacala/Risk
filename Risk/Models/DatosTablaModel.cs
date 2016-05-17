using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Models
{
    public class DatosTablaModel
    {

        public Dictionary<string, string> datosTHead { get; set; }

        public Dictionary<int, List<object>> datosTBody { get; set; }
       
    }
}