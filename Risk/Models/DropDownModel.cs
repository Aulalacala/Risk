using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Models
{
    public class DropDownModel
    {
        private ConnectionDB.connectionRiesgos riesgosBD = new ConnectionDB.connectionRiesgos();
        private Dictionary<int, string> _datosCategorias;
        private Dictionary<int, string> _datosClasificacion1;
        private Dictionary<int, string> _datosClasificacion2;
        private Dictionary<int, string> _datosClasificacion3;
        private Dictionary<int, string> _datosOportunidad;
        private Dictionary<int, string> _datosEfectividad;
        private Dictionary<int, string> _datosResponsables;
        private Dictionary<int, string> _datosSegmentacion;

        public Dictionary<int, string> datosCategorias
        {
            get
            {
                return riesgosBD.DB.tRiesgos_Categorias.ToDictionary(r => r.IdCategoria, r => r.Categoria);
            }
            set
            {
                _datosCategorias = value;
            }
        }

        public Dictionary<int, string> datosClasificacion1
        {
            get
            {
                return riesgosBD.DB.tRiesgos_Clasificaciones.Where(r => r.Nivel == 2).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
            }
            set
            {
                _datosClasificacion1 = value;
            }
        }

        public Dictionary<int, string> datosClasificacion2 { get; set;}
        public Dictionary<int, string> datosClasificacion3 { get; set; }

        public Dictionary<int, string> datosOportunidad
        {
            get
            {
                return riesgosBD.DB.tRiesgos_ControlOportunidad.ToDictionary(r => r.IdControlOportunidad, r => r.Oportunidad);
            }
            set
            {
                _datosOportunidad = value;
            }
        }

        public Dictionary<int, string> datosEfectividad
        {
            get
            {
                return riesgosBD.DB.tRiesgos_ControlEfectividad.ToDictionary(r => r.IdControlEfectividad, r => r.Efectividad);
            }
            set
            {
                _datosEfectividad = value;
            }
        }

        public Dictionary<int, string> datosResponsables
        {
            get
            {
                return riesgosBD.DB.tResponsables.ToDictionary(r => r.IdResponsable, r => r.Nombre);
            }
            set
            {
                _datosResponsables = value;
            }
        }

        public Dictionary<int, string> datosSegmentacion
        {
            get
            {
                return riesgosBD.DB.tRiesgos_Segmentacion1.ToDictionary(r => r.IdSegmenta1, r => r.Segmentacion);
            }
            set
            {
                _datosSegmentacion = value;
            }
        }

        //METODO CARGA DINÁMICA
        public Dictionary<int, string> listadoClasifDinamic(int idEstructura)
        {
            Dictionary<int, string> dicClasif2 = new Dictionary<int, string>();
            try
            {
                dicClasif2 = riesgosBD.DB.tRiesgos_Clasificaciones.Where(r => r.idPadre == idEstructura).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
            }
            catch (Exception)
            {

                return null;
            }
            return dicClasif2;
        }

        // Recuperar clasificaciones segun idEstructura, llamada desde metodo jquery en fichero Scripts2.js ---------------
        public string recuperaListClasif(int idEstructura)
        {
            Dictionary<int, string> dicClasificacion2 = listadoClasifDinamic(idEstructura);
            var j = JsonConvert.SerializeObject(dicClasificacion2);
            return j;
        }
    }
}