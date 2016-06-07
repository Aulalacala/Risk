using Risk.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace Risk.Models
{
    public class TablaRiesgos : DatosTablaModelInterface
    {

        Riesgos_BDDataContext ConexionRiesgos = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();
        BD_MontaDatosTabledata montaTabledata = new BD_MontaDatosTabledata();

        

        public string colVer
        {
            get
            {
                return "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";
            }

            set
            {
                colVer = value;
            }
        }

        public Dictionary<int, List<Tuple<string, string>>> datosTBody
        {
            get
            {
                return filtrosRiesgos(null);
            }

            set
            {
                datosTBody = value;
            }
        }

        public Dictionary<string, string> datosTHead
        {
            get
            {
                return montaTabledata.nombresColTabla("qRiesgosNombre", colVer, colTitulo);
            }

            set
            {
                datosTHead = value;
            }
        }
        public bool editable
        {
            get
            {
                return true;
            }

            set
            {
                editable = value;
            }
        }

        public bool borrar
        {
            get
            {
                return false;
            }

            set
            {
                borrar = value;
            }
        }

        public Dictionary<string, object> filtros
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string nombreTablaBD
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string titulo
        {
            get
            {
                return "INSTANCES SEARCH";
            }

            set
            {
                titulo = value;
            }
        }

        public Tuple<string, string> urlActionBorrar
        {
            get
            {
                return new Tuple<string, string>("deleteRiesgo", "Risk");
            }

            set
            {
                urlActionBorrar = value;
            }
        }

        public Tuple<string, string> urlActionEditar
        {
            get
            {
                return new Tuple<string, string>("RiskFicha", "Risk");
            }

            set
            {
                urlActionEditar = value;
            }
        }

        public string vistaProcedencia
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string colTitulo
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Dictionary<int, object> todosLosDatosDeLaBaseDeDatos()
        {
            return ConexionRiesgos.qRiesgosNombres.ToDictionary(r => r.IdRiesgo, r => (object)r);
        }


        public Dictionary<int, List<Tuple<string, string>>> filtrosRiesgos(Dictionary<string, object> filtros)
        {

            Dictionary<int, qRiesgosNombres> result = new Dictionary<int, qRiesgosNombres>();

            Dictionary<int, object> resultadosBusqueda = new Dictionary<int, object>();

            if (filtros == null)
            {
                result = ConexionRiesgos.qRiesgosNombres.ToDictionary(r => r.IdRiesgo, r => r);
            }
            else
            {
                for (int i = 0; i < filtros.Count(); i++)
                {
                    var item = filtros.ElementAt(i);

                    if (i == 0)
                    {
                        var tipo = "== @0";
                        if (item.Value.GetType().Name == "String")
                        {
                            tipo = ".Contains(@0)";
                        }

                        result = ConexionRiesgos.qRiesgosNombres
                    .Where(item.Key + tipo, item.Value)
                    .ToDictionary(r => r.IdRiesgo, r => r);

                    }
                    else
                    {
                        result = result.Values
                        .Where(item.Key + "== @0", item.Value)
                        .ToDictionary(r => r.IdRiesgo, r => r);
                    }

                }
            }

            resultadosBusqueda = result.ToDictionary(r => r.Key, r => (object)r.Value);

            var dictionaryTBody = montaTabledata.cargaTBody(resultadosBusqueda, this.datosTHead);
            return dictionaryTBody;
        }

        public Dictionary<string, string> datosDeLaCabecera(string nombreTabla, string colVer, string colTitulo)
        {
            return montaTabledata.nombresColTabla(nombreTabla, this.colVer, this.colTitulo);
        }
    }
}