using Newtonsoft.Json;
using Risk.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Models
{
    public class DropDownModel
    {
        //private ConnectionDB.connectionRiesgos riesgosBD = new ConnectionDB.connectionRiesgos();
        private BD_Riesgos BD_Riesgos = new BD_Riesgos();
        private Dictionary<int, string> _datosCategorias;
        private Dictionary<int, string> _datosClasificacion1;
        private Dictionary<int, string> _datosClasificacion2;
        private Dictionary<int, string> _datosClasificacion3;
        private Dictionary<int, string> _datosOportunidad;
        private Dictionary<int, string> _datosEfectividad;
        private Dictionary<int, string> _datosResponsables;
        private Dictionary<int, string> _datosSegmentacion;
        private Dictionary<int, List<string>> _datosEvaSeveridad;
        private Dictionary<int, List<string>> _datosEvaFrecuencia;
        private Dictionary<int, List<string>> _datosEvaEfectividad;

        private Dictionary<string, string> _htmlAttributes;

        // Dropdowns de estructura para los nuevos riesgos --------------------------------------------------
        private Dictionary<int, string> _structureCode;
        //----------------------------------------------------------------------------------------------------

        public Dictionary<int, string> datosCategorias
        {
            get
            {
                return BD_Riesgos.Conexion.tRiesgos_Categorias.ToDictionary(r => r.IdCategoria, r => r.Categoria);
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
                return BD_Riesgos.Conexion.tRiesgos_Clasificaciones.Where(r => r.Nivel == 2).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
            }
            set
            {
                _datosClasificacion1 = value;
            }
        }

        public Dictionary<int, string> datosClasificacion2 { get; set; }
        public Dictionary<int, string> datosClasificacion3 { get; set; }

        public Dictionary<int, string> datosOportunidad
        {
            get
            {
                return BD_Riesgos.Conexion.tRiesgos_ControlOportunidad.ToDictionary(r => r.IdControlOportunidad, r => r.Oportunidad);
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
                return BD_Riesgos.Conexion.tRiesgos_ControlEfectividad.ToDictionary(r => r.IdControlEfectividad, r => r.Efectividad);
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
                return BD_Riesgos.Conexion.tResponsables.ToDictionary(r => r.IdResponsable, r => r.Nombre);
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
                return BD_Riesgos.Conexion.tRiesgos_Segmentacion1.ToDictionary(r => r.IdSegmenta1, r => r.Segmentacion);
            }
            set
            {
                _datosSegmentacion = value;
            }
        }

        public Dictionary<int, List<string>> datosEvaSeveridad
        {
            get
            {
                Dictionary<int, List<string>> dicSeveridad = new Dictionary<int, List<string>>();
                dicSeveridad.Add(0, new List<string> { "#ffffff", "" });

                var dic = BD_Riesgos.Conexion.tEva_Severidad
                                    .GroupBy(x => x.IdEvaSeveridad)
                                    .ToDictionary(r => r.Key, r => r.Select(x => new List<string>
                                    {
                                        x.Color,
                                        x.Severidad
                                    }
                                    ).Single());

                foreach (var item in dic)
                {
                    dicSeveridad.Add(item.Key, item.Value);
                }

                return dicSeveridad;
            }
            set
            {
                _datosEvaSeveridad = value;
            }
        }


        public Dictionary<int, List<string>> datosEvaFrecuencia
        {
            get
            {
                Dictionary<int, List<string>> dicFrecuencia = new Dictionary<int, List<string>>();
                dicFrecuencia.Add(0, new List<string> { "#ffffff", "" });

                var dic =  BD_Riesgos.Conexion.tEva_Frecuencia
                                    .GroupBy(x => x.IdEvaFrecuencia)
                                    .ToDictionary(r => r.Key, r => r.Select(x => new List<string>
                                    {
                                        x.Color,
                                        x.Frecuencia
                                    }
                                    ).Single());

                foreach (var item in dic)
                {
                    dicFrecuencia.Add(item.Key, item.Value);
                }
              
                return dicFrecuencia;
            }
            set
            {
                _datosEvaSeveridad = value;
            }
        }

        public Dictionary<int, List<string>> datosEvaEfectividad
        {
            get
            {
                Dictionary<int, List<string>> dicEfectividad = new Dictionary<int, List<string>>();
                dicEfectividad.Add(0, new List<string> { "#ffffff", "" });

                var dic = BD_Riesgos.Conexion.tEva_Efectividad
                                    .GroupBy(x => x.IdEfectividad)
                                    .ToDictionary(r => r.Key, r => r.Select(x => new List<string>
                                    {
                                        x.Color,
                                        x.Efectividad
                                    }
                                    ).Single());

                foreach (var item in dic)
                {
                    dicEfectividad.Add(item.Key, item.Value);
                }

                return dicEfectividad;
            }
            set
            {
                _datosEvaSeveridad = value;
            }
        }


        //METODO CARGA DINÁMICA
        public Dictionary<int, string> listadoClasifDinamic(int idEstructura)
        {
            Dictionary<int, string> dicClasif2 = new Dictionary<int, string>();
            try
            {
                dicClasif2 = BD_Riesgos.Conexion.tRiesgos_Clasificaciones.Where(r => r.idPadre == idEstructura).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
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




        // METODOS PARA LA CARGAR DE LOS DROPDOWS STRUCTURE CODE(valores de Nivel 3) 
        public Dictionary<int, string> structureCode {
            get {
                return BD_Riesgos.Conexion.tEstructura.Where(r=>r.Nivel==3).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
            }
            set {
                _structureCode = value;
            }
        }
    }
}