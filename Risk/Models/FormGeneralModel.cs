using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Models
{
    public class FormGeneralModel
    {
        public string IdRiesgo { get; set; }
        public string Ejemplo { get; set; }
        public string Descripcion { get; set; }
        public string Justificacion { get; set; }
        public string IdResponsable { get; set; }
        public string IdResponsable2 { get; set; }
        public string IdCategoria { get; set; }
        public string IdSegmentacion1 { get; set; }
        public string IdClasificacion1 { get; set; }
        public string IdClasificacion2 { get; set; }
        public string IdClasificacion3 { get; set; }
        public string NombreFrecAntes { get; set; }
        public string NombreFrecDespues { get; set; }
        public string NombreFrecPlanDespues { get; set; }
        public string NombreSeveAntes { get; set; }
        public string NombreSeveDespues { get; set; }
        public string NombreSevePlanDespues { get; set; }
        public string NombreSevePeorAntes{ get; set; }
        public string NombreSevePeorDespues { get; set; }
        public string NombreSevePeorPlanDespues { get; set; }
        public string IdControlesOportunidad { get; set; }
        public string IdControlesEfectividad { get; set; }


        // Atributos para un riesgo nuevo
        public int idEstructura { get; set; }
        public string CodRiesgo { get; set; }

    }
}