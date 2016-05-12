﻿using Newtonsoft.Json;
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
        private Dictionary<int, List<string>> _datosEvaSeveridad;
        private Dictionary<int, List<string>> _datosEvaFrecuencia;

        // Dropdowns de estructura para los nuevos riesgos --------------------------------------------------
        private Dictionary<int, string> _structureCode;
        //----------------------------------------------------------------------------------------------------

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

        public Dictionary<int, string> datosClasificacion2 { get; set; }
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

        public Dictionary<int, List<string>> datosEvaSeveridad
        {
            get
            {
                Dictionary<int, List<string>> dicSeveridad = new Dictionary<int, List<string>>();
               
                dicSeveridad = riesgosBD.DB.tEva_Severidad
                                    .GroupBy(x => x.IdEvaSeveridad)
                                    .ToDictionary(r => r.Key, r => r.Select(x => new List<string>
                                    {
                                        x.Color,
                                        x.Severidad
                                    }
                                    ).Single());
                dicSeveridad.Add(0, new List<string> { "#ffffff", "" });
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
              

                dicFrecuencia =  riesgosBD.DB.tEva_Frecuencia
                                    .GroupBy(x => x.IdEvaFrecuencia)
                                    .ToDictionary(r => r.Key, r => r.Select(x => new List<string>
                                    {
                                        x.Color,
                                        x.Frecuencia
                                    }
                                    ).Single());
                dicFrecuencia.Add(0, new List<string> { "#ffffff", "" });
                return dicFrecuencia;
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




        // METODOS PARA LA CARGAR DE LOS DROPDOWS STRUCTURE CODE(todos los valores) Y PARTICLE CODE(ultimo disponible segun structure code seleccionado) EN UN RIESGO NUEVO
        public Dictionary<int, string> structureCode {
            get {
                return riesgosBD.DB.tEstructura.ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
            }
            set {
                _structureCode = value;
            }
        }
    }
}