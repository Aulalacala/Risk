using Risk.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace Risk.Models
{
    public class TablaIndicadores_KRIS
    {
        //Variables
        Consultas_BDDataContext ConexionConsultas = (Consultas_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralConsultas();
        DatosTablaModel datosTabla = new DatosTablaModel();
        BD_MontaDatosTabledata BD_MontaDatosTabledata = new BD_MontaDatosTabledata();

        //Constructor donde se definen los atributos fijos del DatosTablaModel
        public TablaIndicadores_KRIS()
        {
            datosTabla.titulo = "KRIS INDICATORS";
            datosTabla.colTitulo = "Código Indicador,Indicador,Fórmula Cálculo,Último Valor,Última Fecha,Nivel Alarma,Nivel Precaución,Nivel Alarma Acción,Código Estado,Estado,Responsable";
            datosTabla.colVer = "CodIndicador,Indicador,FormulaCalculo,UltimoValor,UltimaFecha,NivelAlarma,NivelPrecaucion,NivelAlarmaAccion,CodEstado,Estado,Nombre";
            datosTabla.editable = true;
            datosTabla.urlActionEditar = new Tuple<string, string>("KRISFicha", "KRIS");
            datosTabla.borrar = false;
            datosTabla.vistaProcedencia = "Kris";
            datosTabla.datosTHead = BD_MontaDatosTabledata.cargaTHead("qIndicadores", datosTabla.colVer, datosTabla.colTitulo);
            datosTabla.color = dameColoresTuplas();

        }
      
        //Constructor sobrecargado
        //Misma tabla en la base de datos, pero en la aplicación necesitan verse otras columnas diferentes a las especificadas en el constuctor general
        public TablaIndicadores_KRIS(string colVer, string colTitulo) {
            datosTabla.titulo = "KRIS INDICATORS";
            datosTabla.colTitulo = colTitulo;
            datosTabla.colVer = colVer;
            datosTabla.editable = false;
            datosTabla.borrar = false;
            datosTabla.vistaProcedencia = "Kris";
            datosTabla.datosTHead = BD_MontaDatosTabledata.cargaTHead("qIndicadores", datosTabla.colVer, datosTabla.colTitulo);
        }

        private List<string> dameColoresTuplas() {
            List<string> colores = new List<string>();

            colores = ConexionConsultas.qIndicadores.Select(i => i.Color.Replace(';', ',')).ToList();

            return colores;
        }

        public DatosTablaModel dameTabla(Dictionary<string, object> filtros)
        {
            var filtrosVar = filtrosIndicadores(filtros);
            var transformacionTBody = BD_MontaDatosTabledata.cargaTBody(filtrosVar, datosTabla.datosTHead);

            datosTabla.datosTBody = transformacionTBody;
            return datosTabla;
        }

        public DatosTablaModel dameTablaPorIdIndicador(int idIndicador) {
            Dictionary<int, object> result = new Dictionary<int, object>();
            result = ConexionConsultas.qIndicadores.Where(x => x.IdIndicador == idIndicador).Select(r => r).ToDictionary(r => r.IdIndicador, r => (object)r);
            var transformacionTBody = BD_MontaDatosTabledata.cargaTBody(result, datosTabla.datosTHead);
            datosTabla.datosTBody = transformacionTBody;

            return datosTabla;
        }

        //Método donde se realiza el filtrado de los datos de la base, dependiendo de si el dictionary recibido está lleno o no
        public Dictionary<int, object> filtrosIndicadores(Dictionary<string, object> filtros)
        {
            Dictionary<int, qIndicadores> result = new Dictionary<int, qIndicadores>();

            Dictionary<int, object> resultadosBusqueda = new Dictionary<int, object>();

            //Si el dictionary está vacío se cogen todos los datos de la base de datos
            if (filtros.Count == 0)
            {
                result = ConexionConsultas.qIndicadores.ToDictionary(r => Convert.ToInt32(r.IdIndicador), r => r);
            }
            else
            {
                //Si no, nos recorremos el Dictionary
                for (int i = 0; i < filtros.Count(); i++)
                {
                    var item = filtros.ElementAt(i);

                    if (i == 0)
                    {
                        var tipo = "== @0";
                        if (item.Value != null)
                        {
                            //Tenemos en cuenta que si el tipo del elemento es un String, para su búsqueda haremos un Contains, no un ==
                            if (item.Value.GetType().Name == "String")
                            {
                                tipo = ".Contains(@0)";
                            }
                        }

                        //lambda con Dynamic Linq
                        result = ConexionConsultas.qIndicadores
                    .Where(item.Key + tipo, item.Value)
                    .ToDictionary(r =>Convert.ToInt32(r.IdIndicador), r => r);

                    }
                    else
                    {
                        result = result.Values
                        .Where(item.Key + "== @0", item.Value)
                        .ToDictionary(r => Convert.ToInt32(r.IdIndicador), r => r);
                    }

                }
            }
            //Volcado del dictionary obtenido a uno nuevo, cuyo Value es de tipo object, para que sea genérico
            resultadosBusqueda = result.ToDictionary(r => r.Key, r => (object)r.Value);
            return resultadosBusqueda;
        }
    }
}