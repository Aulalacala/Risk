using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Models
{
    public class DropDownModel
    {
        private Riesgos_BDDataContext riesgosBD = new Riesgos_BDDataContext();
        private Dictionary<int, string> _dicCategorias;
        private Dictionary<int, string> _dicClasificacion1;
        private Dictionary<int, string> _dicClasificacion2;
        private Dictionary<int, string> _dicClasificacion3;
        private Dictionary<int, string> _datosOportunidad;
        private Dictionary<int, string> _datosEfectividad;

        public Dictionary<int, string> dicCategorias
        {
            get
            {
                return riesgosBD.tRiesgos_Categorias.ToDictionary(r => r.IdCategoria, r => r.Categoria);
            }
            set
            {
                _dicCategorias = value;
            }
        }

        public Dictionary<int, string> dicClasificacion1
        {
            get
            {
                return riesgosBD.tRiesgos_Clasificaciones.Where(r => r.Nivel == 2).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
            }
            set
            {
                _dicClasificacion1 = value;
            }
        }

        public Dictionary<int, string> dicClasificacion2 { get; set;}
        public Dictionary<int, string> dicClasificacion3 { get; set; }

        public Dictionary<int, string> datosOportunidad
        {
            get
            {
                return riesgosBD.tRiesgos_ControlOportunidad.ToDictionary(r => r.IdControlOportunidad, r => r.Oportunidad);
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
                return riesgosBD.tRiesgos_ControlEfectividad.ToDictionary(r => r.IdControlEfectividad, r => r.Efectividad);
            }
            set
            {
                _datosEfectividad = value;
            }
        }


        //METODO CARGA DINÁMICA
        public Dictionary<int, string> listadoClasifDinamic(int idEstructura)
        {
            Dictionary<int, string> dicClasif2 = new Dictionary<int, string>();
            try
            {
                dicClasif2 = riesgosBD.tRiesgos_Clasificaciones.Where(r => r.idPadre == idEstructura).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
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