using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using Risk.Controllers;

namespace Risk.Models
{
    public class TablaRiesgos2
    {

        Riesgos_BDDataContext ConexionRiesgos = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();
        DatosTablaModel datosTabla = new DatosTablaModel();
        BD_MontaDatosTabledata BD_MontaDatosTabledata = new BD_MontaDatosTabledata();

        public TablaRiesgos2()
        {
            datosTabla.titulo = "INSTANCES SEARCH";
            datosTabla.colTitulo = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";
            datosTabla.colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
            datosTabla.editable = true;
            datosTabla.urlActionEditar = new Tuple<string, string>("RiskFicha", "Risk");
            datosTabla.borrar = false;
            datosTabla.vistaProcedencia = "Risks";

           
            datosTabla.datosTHead = BD_MontaDatosTabledata.nombresColTabla("qRiesgosNombres", datosTabla.colVer, datosTabla.colTitulo);

        }



        public DatosTablaModel dameTabla(Dictionary<string, object> filtros)
        {
            var filtros4 = filtrosRiesgos(filtros);
            var transformacionTBody = BD_MontaDatosTabledata.cargaTBody(filtros4, datosTabla.datosTHead);

            datosTabla.datosTBody = transformacionTBody;


            return datosTabla;
        }



        public Dictionary<int, object> filtrosRiesgos(Dictionary<string, object> filtros)
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
            return resultadosBusqueda;
        }





    }



    public class TablaRiesgos3
    {

        Riesgos_BDDataContext ConexionRiesgos = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();
        DatosTablaModel datosTabla = new DatosTablaModel();
        BD_MontaDatosTabledata BD_MontaDatosTabledata = new BD_MontaDatosTabledata();

        public TablaRiesgos3()
        {
            datosTabla.titulo = "INSTANCES SEARCH";
            datosTabla.colTitulo = "Código Riesgo,Nombre,Categoría,Clasificación1,Clasificación2,Clasificación3,Código Localizado";
            datosTabla.colVer = "CodRiesgo,Nombre,Categoria,Clasif1,Clasif2,Clasif3,CodRiesgoLocalizado";
            datosTabla.editable = true;
            datosTabla.urlActionEditar = new Tuple<string, string>("RiskFicha", "Risk");
            datosTabla.borrar = false;
            datosTabla.vistaProcedencia = "Risks";
            datosTabla.datosTHead = BD_MontaDatosTabledata.nombresColTabla("qRiesgosNombres", datosTabla.colVer, datosTabla.colTitulo);
        }

        public DatosTablaModel dameTabla(Dictionary<string, object> filtros)
        {
            var filtros4 = filtrosRiesgos(filtros);
            var transformacionTBody = BD_MontaDatosTabledata.cargaTBody(filtros4, datosTabla.datosTHead);

            datosTabla.datosTBody = transformacionTBody;


            return datosTabla;
        }

        public Dictionary<int, object> filtrosRiesgos(Dictionary<string, object> filtros)
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
            return resultadosBusqueda;
        }
    }

}