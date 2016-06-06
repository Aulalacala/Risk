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

        public Riesgos_BDDataContext Conexion = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();

        private BD_Riesgos BD_Riesgos = new BD_Riesgos();
       
        private Dictionary<int, string> _datosClasificacion2;
        private Dictionary<int, string> _datosClasificacion3;
        private Dictionary<string, string> _htmlAttributes;

       
        public Dictionary<int, string> cboCategorias (){
            return Conexion.tRiesgos_Categorias.ToDictionary(r => r.IdCategoria, r => r.Categoria);
        }

        public Dictionary<int, string> cboClasificacion1() {
            return Conexion.tRiesgos_Clasificaciones.Where(r => r.Nivel == 2).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
        }


        public Dictionary<int, string> datosClasificacion2 { get; set; }
        public Dictionary<int, string> datosClasificacion3 { get; set; }

        public Dictionary<int, string> cboOportunidad() {
            return Conexion.tRiesgos_ControlOportunidad.ToDictionary(r => r.IdControlOportunidad, r => r.Oportunidad);
        }

        public Dictionary<int, string> cboEfectividad() {
            return Conexion.tRiesgos_ControlEfectividad.ToDictionary(r => r.IdControlEfectividad, r => r.Efectividad);
        }

        public Dictionary<int, string> cboResponsables() {
            return Conexion.tResponsables.ToDictionary(r => r.IdResponsable, r => r.Nombre);
        }

        public Dictionary<int, string> cboSegmentacion() {
            return Conexion.tRiesgos_Segmentacion1.ToDictionary(r => r.IdSegmenta1, r => r.Segmentacion);
        }



        public Dictionary<int, List<string>> cboEvaSeveridad ()
        {
          
                Dictionary<int, List<string>> dicSeveridad = new Dictionary<int, List<string>>();
                dicSeveridad.Add(0, new List<string> { "#ffffff", "" });

                var dic = Conexion.tEva_Severidad
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
           
        


        public Dictionary<int, List<string>> cboEvaFrecuencia ()
        {
           
                Dictionary<int, List<string>> dicFrecuencia = new Dictionary<int, List<string>>();
                dicFrecuencia.Add(0, new List<string> { "#ffffff", "" });

                var dic =  Conexion.tEva_Frecuencia
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

        public Dictionary<int, List<string>> cboEvaEfectividad ()
        {
            
                Dictionary<int, List<string>> dicEfectividad = new Dictionary<int, List<string>>();
                dicEfectividad.Add(0, new List<string> { "#ffffff", "" });

                var dic = Conexion.tEva_Efectividad
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


        //METODO CARGA DINÁMICA
        public Dictionary<int, string> listadoClasifDinamic(int idEstructura)
        {
            Dictionary<int, string> dicClasif2 = new Dictionary<int, string>();
            try
            {
                dicClasif2 = Conexion.tRiesgos_Clasificaciones.Where(r => r.idPadre == idEstructura).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
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
        public Dictionary<int, string> cboStructureCode (){
           
                return Conexion.tEstructura.Where(r=>r.Nivel==3).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
           
        }
    }
}