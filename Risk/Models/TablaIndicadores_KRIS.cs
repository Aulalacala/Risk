﻿using Risk.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace Risk.Models
{
    public class TablaIndicadores_KRIS
    {
        Consultas_BDDataContext ConexionConsultas = (Consultas_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralConsultas();

        DatosTablaModel datosTabla = new DatosTablaModel();
        BD_MontaDatosTabledata BD_MontaDatosTabledata = new BD_MontaDatosTabledata();

        public TablaIndicadores_KRIS()
        {
            datosTabla.titulo = "KRIS INDICATORS";
            datosTabla.colTitulo = "Código Indicador,Indicador,Fórmula Cálculo,Último Valor,Última Fecha,Nivel Alarma,Nivel Precaución,Nivel Alarma Acción,Código Estado,Estado,Responsable";
            datosTabla.colVer = "CodIndicador,Indicador,FormulaCalculo,UltimoValor,UltimaFecha,NivelAlarma,NivelPrecaucion,NivelAlarmaAccion,CodEstado,Estado,Nombre";
            datosTabla.editable = true;
            datosTabla.urlActionEditar = new Tuple<string, string>("KRISFicha", "KRIS");
            datosTabla.borrar = false;
            datosTabla.vistaProcedencia = "Kris";
            datosTabla.datosTHead = BD_MontaDatosTabledata.nombresColTabla("qIndicadores", datosTabla.colVer, datosTabla.colTitulo);
        }

        public DatosTablaModel dameTabla(Dictionary<string, object> filtros)
        {
            var filtrosVar = filtrosRiesgos(filtros);
            var transformacionTBody = BD_MontaDatosTabledata.cargaTBody(filtrosVar, datosTabla.datosTHead);

            datosTabla.datosTBody = transformacionTBody;
            return datosTabla;
        }

        public Dictionary<int, object> filtrosRiesgos(Dictionary<string, object> filtros)
        {

            Dictionary<int, qIndicadores> result = new Dictionary<int, qIndicadores>();

            Dictionary<int, object> resultadosBusqueda = new Dictionary<int, object>();

            if (filtros.Count == 0)
            {
                result = ConexionConsultas.qIndicadores.ToDictionary(r => Convert.ToInt32(r.IdIndicador), r => r);
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

            resultadosBusqueda = result.ToDictionary(r => r.Key, r => (object)r.Value);
            return resultadosBusqueda;
        }
    }
}