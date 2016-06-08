using Risk.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Models
{
    public class TablaEvaluaciones_Historical
    {
        Riesgos_BDDataContext ConexionRiesgos = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();
        DatosTablaModel datosTabla = new DatosTablaModel();
        BD_MontaDatosTabledata BD_MontaDatosTabledata = new BD_MontaDatosTabledata();

        public TablaEvaluaciones_Historical()
        {
            datosTabla.colTitulo = "Activa,Ultima,Fecha,FrecuenciaA,SeveridadA,FrecuenciaD,SeveridadD";
            datosTabla.colVer = "Activa,Ultima,Fecha,NombreFrecAntes,NombreSeveAntes,NombreFrecDespues,NombreSeveDespues";
            datosTabla.editable = false;
            datosTabla.borrar = false;
            datosTabla.vistaProcedencia = "Historicals";
            datosTabla.datosTHead = BD_MontaDatosTabledata.nombresColTabla("qRiesgosEvalVal", datosTabla.colVer, datosTabla.colTitulo);
        }

        public DatosTablaModel dameTablaPorIdRiesgo(int idRiesgo)
        {
            Dictionary<int, object> result = new Dictionary<int, object>();
            result = ConexionRiesgos.qRiesgosEvalVal.Where(r => r.IdRiesgo == idRiesgo).ToDictionary(r => Convert.ToInt32(r.IdEvaluacion), r => (object)r);

            var transformacionTBody = BD_MontaDatosTabledata.cargaTBody(result, datosTabla.datosTHead);
            datosTabla.datosTBody = transformacionTBody;

            return datosTabla;
        }
    }
}