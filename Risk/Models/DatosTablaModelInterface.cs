using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk.Models
{
    interface DatosTablaModelInterface
    {
         string titulo { get; set; }
         Dictionary<string, string> datosTHead { get; set; }
         Dictionary<int, List<Tuple<string, string>>> datosTBody { get; set; }
         string vistaProcedencia { get; set; }
         bool editable { get; set; }
         bool borrar { get; set; }
         Tuple<string, string> urlActionEditar { get; set; }
         Tuple<string, string> urlActionBorrar { get; set; }

         string nombreTablaBD { get; set; }
         string colVer { get; set; }
         string colTitulo { get; set; }
         Dictionary<string, object> filtros { get; set; }

        Dictionary<int, object> todosLosDatosDeLaBaseDeDatos();
        Dictionary<string, string> datosDeLaCabecera(string nombreTabla, string colVer, string colTitulo);

    }
}
