using Risk.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace Risk.Models
{
    public class TablaRiesgos_Risks
    {
        //Variables
        Riesgos_BDDataContext ConexionRiesgos = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();
        DatosTablaModel datosTabla = new DatosTablaModel();
        BD_MontaDatosTabledata BD_MontaDatosTabledata = new BD_MontaDatosTabledata();

        //Constructor donde se definen los atributos fijos del DatosTablaModel
        public TablaRiesgos_Risks()
        {
            datosTabla.titulo = "INSTANCES SEARCH";
            datosTabla.colTitulo = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";
            datosTabla.colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
            datosTabla.editable = true;
            datosTabla.urlActionEditar = new Tuple<string, string>("RiskFicha", "Risk");
            datosTabla.borrar = false;
            datosTabla.vistaProcedencia = "Risks";
            datosTabla.datosTHead = BD_MontaDatosTabledata.cargaTHead("qRiesgosNombres", datosTabla.colVer, datosTabla.colTitulo);
        }

        public DatosTablaModel dameTabla(Dictionary<string, object> filtros)
        {
            var filtros4 = filtrosRiesgos(filtros);
            var transformacionTBody = BD_MontaDatosTabledata.cargaTBody(filtros4, datosTabla.datosTHead);

            datosTabla.datosTBody = transformacionTBody;
            return datosTabla;
        }

        public DatosTablaModel dameTablaPorIdEstructura(int idEstructura)
        {
            Dictionary<int, object> result = new Dictionary<int, object>();
            result = riesgosDescendientes(idEstructura).ToDictionary(r => r.Key, r => (object)r.Value);

            var transformacionTBody = BD_MontaDatosTabledata.cargaTBody(result, datosTabla.datosTHead);
            datosTabla.datosTBody = transformacionTBody;

            return datosTabla;
        }

        public DatosTablaModel dameTablaPorIdRiesgo(int idRiesgo)
        {
            Dictionary<int, object> result = new Dictionary<int, object>();
            result = ConexionRiesgos.qRiesgosNombres.Where(x => x.IdRiesgo == idRiesgo).Select(r => r).ToDictionary(r => r.IdRiesgo, r => (object)r);

            var transformacionTBody = BD_MontaDatosTabledata.cargaTBody(result, datosTabla.datosTHead);
            datosTabla.datosTBody = transformacionTBody;

            return datosTabla;
        }

        //Método donde se realiza el filtrado de los datos de la base, dependiendo de si el dictionary recibido está lleno o no
        public Dictionary<int, object> filtrosRiesgos(Dictionary<string, object> filtros)
        {

            Dictionary<int, qRiesgosNombres> result = new Dictionary<int, qRiesgosNombres>();

            Dictionary<int, object> resultadosBusqueda = new Dictionary<int, object>();

            //Si el dictionary está vacío se cogen todos los datos de la base de datos
            if (filtros.Count == 0)
            {
                result = ConexionRiesgos.qRiesgosNombres.ToDictionary(r => r.IdRiesgo, r => r);
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
                        if(item.Value != null)
                        {
                            //Tenemos en cuenta que si el tipo del elemento es un String, para su búsqueda haremos un Contains, no un ==
                            if (item.Value.GetType().Name == "String")
                            {
                                tipo = ".Contains(@0)";
                            }
                        }

                        //lambda con Dynamic Linq
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
            //Volcado del dictionary obtenido a uno nuevo, cuyo Value es de tipo object, para que sea genérico
            resultadosBusqueda = result.ToDictionary(r => r.Key, r => (object)r.Value);
            return resultadosBusqueda;
        }

        public Dictionary<int, qRiesgosNombres> riesgosDescendientes(int id)
        {
            Dictionary<int, qRiesgosNombres> datosBusqueda = new Dictionary<int, qRiesgosNombres>();
            List<tEstructura> cuantosHay = ConexionRiesgos.tEstructura.Where(r => r.idPadre == id).OrderBy(r => r.Orden).ToList();
            List<int> idRiesgos = new List<int>();


            if (cuantosHay.Count() != 0)
            {
                idRiesgos = ConexionRiesgos.tEstructura.Where(x => x.idPadre == Convert.ToInt32(id)).Select(x => Convert.ToInt32(x.IdEstructura)).ToList();
                foreach (var idR in idRiesgos)
                {
                    riesgosDescendientes(idR);
                }
            }
            else
            {
                idRiesgos = ConexionRiesgos.tRelEstructuraRiesgos.Where(x => x.IdEstructura == Convert.ToInt32(id)).Select(x => Convert.ToInt32(x.IdRiesgo)).ToList();

                try
                {
                    foreach (var idR in idRiesgos)
                    {
                        datosBusqueda.Add(ConexionRiesgos.qRiesgosNombres.Where(r => r.IdRiesgo == idR).Select(r => r.IdRiesgo).SingleOrDefault(), ConexionRiesgos.qRiesgosNombres.Where(r => r.IdRiesgo == idR).Select(r => r).SingleOrDefault());
                    }
                }
                catch (Exception)
                {
                }
            }
            return datosBusqueda;
        }


    }
}