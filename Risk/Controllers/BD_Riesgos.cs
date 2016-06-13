using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Risk.Models;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Risk.Controllers
{
    public class BD_Riesgos
    {

        Riesgos_BDDataContext ConexionRiesgos = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();
        Consultas_BDDataContext ConexionConsultas = (Consultas_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralConsultas();

        #region AssignController
        public DescriptionStructureModel recuperaConteDefEstructura(int id)
        {
            DescriptionStructureModel description = new DescriptionStructureModel();

            ConexionRiesgos.qEstructura_Contenidos_Def.Where(r => r.IdEstructura == id).ToList().ForEach(x =>
            {
                switch (x.Titulo)
                {
                    case "Department":
                        description.department = x.Contenido;
                        break;
                    case "Responsible":
                        description.responsible = x.Contenido;
                        break;
                    case "Activities":
                        description.activities = x.Contenido;
                        break;
                    case "IT Assets":
                        description.ITassets = x.Contenido;
                        break;
                    case "Controls":
                        description.controls = x.Contenido;
                        break;
                    case "Inputs":
                        description.inputs = x.Contenido;
                        break;
                    case "Outputs":
                        description.outputs = x.Contenido;
                        break;
                }
            });

            return description;
        }    
        #endregion

        //Esta región tiene las operaciones necesarias para hacer los Create,Read,Update,Delete necesarios para que las vistas que nacen de RiskController tengan sentido
        //A su vez está subdividida en regiones, cada una de las cuales se refiera a una tabla esecífica
        #region RiskController

        #region CRUD relativo a Riesgos
        public void insertarNuevoRiesgo(tRiesgos riesgoNuevo)
        {
            try
            {
                ConexionRiesgos.tRiesgos.InsertOnSubmit(riesgoNuevo);
                ConexionRiesgos.SubmitChanges();
            }
            catch (Exception e) { }
        }

        public void updateRiesgo(tRiesgos riesgo)
        {
            try
            {
                ConexionRiesgos.SubmitChanges();
            }
            catch (Exception) { }
        }

        public bool deleteRiesgo(int idRiesgo)
        {
            try
            {
                var riesgo = ConexionRiesgos.tRiesgos.Where(r => r.IdRiesgo == idRiesgo).Select(r => r).FirstOrDefault();
                ConexionRiesgos.tRiesgos.DeleteOnSubmit(riesgo);
                ConexionRiesgos.SubmitChanges();

                deleteTRelEstructuraRiesgos(idRiesgo);

                deleteTRiesgosEvaluaciones(idRiesgo);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #region consultas relativas a Riesgos

        public tRiesgos recuperarTRiesgo(int idRiesgo)
        {
            return ConexionRiesgos.tRiesgos.Where(r => r.IdRiesgo == idRiesgo).SingleOrDefault();
        }


        public qRiesgosNombres recuperarQriesgoNombre(int id)
        {
            qRiesgosNombres riesgoRecup = new qRiesgosNombres();
            if (id != 0)
            {
                riesgoRecup = ConexionRiesgos.qRiesgosNombres.Where(r => r.IdRiesgo == id).SingleOrDefault();
            }
            return riesgoRecup;
        }

        // metodo que devuelve un string con el ultimo codigo disponible de un idEstructura
        public string ultimoRiesgoDisponible(string idEstructura)
        {
            int cuantosRiesgosTiene = ConexionRiesgos.tRelEstructuraRiesgos.Where(r => r.IdEstructura == Convert.ToInt32(idEstructura)).Count();
            string ultimoCodigoRiesgo = (cuantosRiesgosTiene + 1).ToString();

            if (cuantosRiesgosTiene.ToString().Length == 1)
            {
                ultimoCodigoRiesgo = "0" + (cuantosRiesgosTiene + 1).ToString();
            }

            string codCompleto = ConexionRiesgos.tEstructura.Where(r => r.IdEstructura == Convert.ToInt32(idEstructura)).Select(r => r.CodCompleto).SingleOrDefault();

            codCompleto += "." + ultimoCodigoRiesgo;

            return codCompleto;
        }

        #endregion
        #endregion

        #region CRUD relativo a Relacion Estructura-Riesgos
        public bool insertarTRelEstructuraRiesgoNuevo(tRelEstructuraRiesgos estructuraNuevo)
        {
            try
            {
                ConexionRiesgos.tRelEstructuraRiesgos.InsertOnSubmit(estructuraNuevo);
                ConexionRiesgos.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool deleteTRelEstructuraRiesgos(int idRiesgo)
        {
            try
            {
                var riesgoTrel = ConexionRiesgos.tRelEstructuraRiesgos.Where(r => r.IdRiesgo == idRiesgo).Select(r => r).FirstOrDefault();
                ConexionRiesgos.tRelEstructuraRiesgos.DeleteOnSubmit(riesgoTrel);
                ConexionRiesgos.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region CRUD relativo a Evaluaciones

        public bool insertarTRiesgosEvaluaciones(tRiesgosEvaluaciones evaluacion)
        {
            try
            {
                ConexionRiesgos.tRiesgosEvaluaciones.InsertOnSubmit(evaluacion);
                ConexionRiesgos.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool updateTRiesgsEvaluaciones(tRiesgosEvaluaciones evaluacion)
        {
            try
            {
                ConexionRiesgos.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool deleteTRiesgosEvaluaciones(int idRiesgo)
        {
            try
            {
                var riesgoTEval = ConexionRiesgos.tRiesgosEvaluaciones.Where(r => r.IdRiesgo == idRiesgo).Select(r => r).FirstOrDefault();
                ConexionRiesgos.tRiesgosEvaluaciones.DeleteOnSubmit(riesgoTEval);
                ConexionRiesgos.SubmitChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool deleteEvaluacion(int idRiesgo, int idEvaluacion)
        {
            try
            {
                var evaluacion = ConexionRiesgos.tRiesgosEvaluaciones.Where(r => r.IdRiesgo == idRiesgo && r.IdEvaluacion == idEvaluacion).Select(r => r).FirstOrDefault();
                ConexionRiesgos.tRiesgosEvaluaciones.DeleteOnSubmit(evaluacion);
                ConexionRiesgos.SubmitChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #region consultas relativas a Evaluaciones
        public Dictionary<int, qRiesgosEvalVal> recuperaEvaluaciones(int id, int idEvaluacion = 0)
        {
            Dictionary<int, qRiesgosEvalVal> dicEvaluacionesRiesgo = new Dictionary<int, qRiesgosEvalVal>();

            if (idEvaluacion != 0)
            {
                dicEvaluacionesRiesgo = ConexionRiesgos.qRiesgosEvalVal.Where(r => r.IdRiesgo == id && r.IdEvaluacion == idEvaluacion).ToDictionary(r => Convert.ToInt32(r.IdEvaluacion), r => r);
            }
            else
            {
                dicEvaluacionesRiesgo = ConexionRiesgos.qRiesgosEvalVal.Where(r => r.IdRiesgo == id && r.Ultima == true).ToDictionary(r => Convert.ToInt32(r.IdEvaluacion), r => r);
            }

            return dicEvaluacionesRiesgo;
        }

        public int recuperaIdUltimaEvaluacion(int id)
        {
            return ConexionRiesgos.qRiesgosEvalVal.Where(r => r.IdRiesgo == id && r.Ultima == true).Select(r => Convert.ToInt32(r.IdEvaluacion)).SingleOrDefault();
        }

        public tRiesgosEvaluaciones recuperaTRiesgosEvaluacion(int idEvaluacion)
        {
            return ConexionRiesgos.tRiesgosEvaluaciones.Where(r => r.IdEvaluacion == idEvaluacion).SingleOrDefault();
        }

        public List<tRiesgosEvaluaciones> recuperaEvaluaciones(int idRiesgo)
        {
            return ConexionRiesgos.tRiesgosEvaluaciones.Where(r => r.IdRiesgo == idRiesgo).ToList();
        }


        public bool cambiaUltimasAFalseEvaluaciones(int idRiesgo)
        {
            bool cambios = true;
            List<tRiesgosEvaluaciones> evaluacionesPorRiesgo = recuperaEvaluaciones(idRiesgo);

            if (evaluacionesPorRiesgo.Count != 0)
            {
                foreach (var evaluacion in evaluacionesPorRiesgo)
                {
                    evaluacion.Ultima = false;
                    try
                    {
                        updateTRiesgsEvaluaciones(evaluacion);
                        cambios = true;
                    }
                    catch (Exception)
                    {
                        cambios = false;
                    }
                }
            }

            return cambios;
        }

        #endregion

        #endregion

        #endregion

        

        
    }
}