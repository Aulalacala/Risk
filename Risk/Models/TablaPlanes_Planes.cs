using Risk.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;


namespace Risk.Models
{
    public class TablaPlanes_Planes
    {
        Consultas_BDDataContext ConexionConsultas = (Consultas_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralConsultas();
        DatosTablaModel datosTabla = new DatosTablaModel();
        BD_MontaDatosTabledata BD_MontaDatosTabledata = new BD_MontaDatosTabledata();

        public TablaPlanes_Planes()
        {
            datosTabla.colTitulo = "Coódigo Plan Acción,Nombre,Medidas,Activa,Fecha Fin Teórica,Fecha Fin Real,Responsable";
            datosTabla.colVer = "CodPlanAccion,Nombre,Medidas,Activa,FechaFinTeorica,FechaFinReal,Responsable";
            datosTabla.editable = true;
            datosTabla.urlActionEditar = new Tuple<string, string>("PlanFicha", "Plan");
            datosTabla.borrar = false;
            datosTabla.vistaProcedencia = "Plans";
            datosTabla.datosTHead = BD_MontaDatosTabledata.cargaTHead("qPlanes", datosTabla.colVer, datosTabla.colTitulo);
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

            Dictionary<int, qPlanes> result = new Dictionary<int, qPlanes>();

            Dictionary<int, object> resultadosBusqueda = new Dictionary<int, object>();

            if (filtros.Count == 0)
            {
                result = ConexionConsultas.qPlanes.ToDictionary(r => r.IdPlanAccion, r => r);
            }
            else
            {
                for (int i = 0; i < filtros.Count(); i++)
                {
                    var item = filtros.ElementAt(i);

                    if (i == 0)
                    {
                        var tipo = "== @0";
                        if (item.Value != null)
                        {
                            if (item.Value.GetType().Name == "String")
                            {
                                tipo = ".Contains(@0)";
                            }
                        }


                        result = ConexionConsultas.qPlanes
                    .Where(item.Key + tipo, item.Value)
                    .ToDictionary(r => r.IdPlanAccion, r => r);

                    }
                    else
                    {
                        result = result.Values
                        .Where(item.Key + "== @0", item.Value)
                        .ToDictionary(r => r.IdPlanAccion, r => r);
                    }

                }
            }

            resultadosBusqueda = result.ToDictionary(r => r.Key, r => (object)r.Value);
            return resultadosBusqueda;
        }
    }
}