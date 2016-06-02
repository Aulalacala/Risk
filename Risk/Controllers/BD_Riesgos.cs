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

namespace Risk.Controllers {
    public class BD_Riesgos {

        Riesgos_BDDataContext Conexion = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();


        public Dictionary<int, qRiesgosNombres> datosQ = new Dictionary<int, qRiesgosNombres>();


        #region AssignController
        //Recuperar THEAD tabla Datos Risk-----------------
        public Dictionary<string, string> nombresColTabla(string nombreTabla, string colVer, string colTitulos) {
            Dictionary<string, string> nombreColumnasModif = new Dictionary<string, string>();

            try {
                MetaTable TablaDBO = Conexion.Mapping.GetTables().Where(t => t.TableName == "dbo." + nombreTabla).Select(t => t).SingleOrDefault();

                List<string> ver = new List<string>();
                List<string> titulos = new List<string>();

                if (string.IsNullOrEmpty(colVer) && string.IsNullOrEmpty(colTitulos)) //como estan TODOS en la BD
                {
                    foreach (var item in TablaDBO.RowType.DataMembers) {
                        if (!item.Name.Contains("Id")) {
                            nombreColumnasModif.Add(item.Name, item.Name);
                        }
                    }
                } else {
                    ver = colVer.Split(',').ToList();

                    if (string.IsNullOrEmpty(colTitulos)) {
                        titulos = ver;
                    } else {
                        titulos = colTitulos.Split(',').ToList();
                    }

                    foreach (var columnVer in ver) {
                        if (TablaDBO.RowType.DataMembers.Where(r => r.Name.Equals(columnVer)).Select(r => r).SingleOrDefault() != null) {
                            nombreColumnasModif.Add(columnVer, titulos[ver.IndexOf(columnVer)]);
                        }

                    }
                }
            } catch (Exception e) {
                return null;
            }
            return nombreColumnasModif;
        }

        public Dictionary<int, List<Tuple<string, string>>> cargaTablaDatos(string nombreTabla, string colVer, string colTitulos, string filtro = null, int categoria = 0, int clasificacion1 = 0, int clasificacion2 = 0, int clasificacion3 = 0, int idEstructura = 0, bool riesgoSinAsignar = false) {
            Dictionary<int, object> dic = new Dictionary<int, object>();


            Dictionary<int, List<Tuple<string, string>>> listaDatos = new Dictionary<int, List<Tuple<string, string>>>();

            try {
                string query = "select * from " + nombreTabla;
                switch (nombreTabla) {
                    case "qRiesgosNombres":
                        Dictionary<int, qRiesgosNombres> dicRiesgos = Conexion.ExecuteQuery<qRiesgosNombres>(query).ToDictionary(r => r.IdRiesgo, r => r);

                        Dictionary<int, qRiesgosNombres> dicFiltrado = busquedasQRiesgosNombres(dicRiesgos, filtro, categoria, clasificacion1, clasificacion2, clasificacion3, idEstructura, riesgoSinAsignar);
                        dic = dicFiltrado.ToDictionary(r => r.Key, r => (object)r.Value);
                        break;
                    case "qRiesgosEvalVal":
                        Dictionary<int, qRiesgosEvalVal> dicEvaluaciones = Conexion.ExecuteQuery<qRiesgosEvalVal>(query).Where(r => r.IdRiesgo == idEstructura).ToDictionary(r => Convert.ToInt32(r.IdEvaluacion), r => r);
                        dic = dicEvaluaciones.ToDictionary(r => r.Key, r => (object)r.Value);
                        break;
                }

                // Cargar de las columnas a mostrar
                Dictionary<string, string> nombreCols = nombresColTabla(nombreTabla, colVer, colTitulos);


                foreach (var riesgo in dic) {
                    if (riesgo.Value != null) {
                        List<Tuple<string, string>> camposTabla = new List<Tuple<string, string>>();

                         var tiposColumnas = Conexion.Mapping.GetTables().Where(y => y.TableName == "dbo."+ nombreTabla).Single().RowType.DataMembers;

                        foreach (var col in nombreCols) {

                            string name;
                            System.Reflection.PropertyInfo x = riesgo.Value.GetType().GetProperty(col.Key);
                            var tipo = ""; 

                            if (x.GetValue(riesgo.Value, null) == null) {
     
                                name = " ";
                            } else {
                                tipo = x.GetValue(riesgo.Value, null).GetType().Name;
                                name = (string)((x.GetValue(riesgo.Value, null))).ToString();
                            }
                            camposTabla.Add(new Tuple<string, string>(tipo, name));
                        }
                        listaDatos.Add(riesgo.Key, camposTabla);
                    }
                }
            } catch (Exception e) {
                var exception = e;
                return null;
            }
            return listaDatos;
        }


        public Dictionary<int, qRiesgosNombres> busquedasQRiesgosNombres(Dictionary<int, qRiesgosNombres> dicDato, string filtro = null, int categoria = 0, int clasificacion1 = 0, int clasificacion2 = 0, int clasificacion3 = 0, int idEstructura = 0, bool riesgoSinAsignar = false) {
            if (!string.IsNullOrEmpty(filtro)) {

                dicDato = dicDato.Where(r => r.Value.Nombre.Contains(filtro)).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            if (categoria != 0) {
                dicDato = dicDato.Where(r => r.Value.IdCategoria == categoria).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            if (clasificacion1 != 0) {
                dicDato = dicDato.Where(r => r.Value.IdClasificacion1 == clasificacion1).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            if (clasificacion2 != 0) {
                dicDato = dicDato.Where(r => r.Value.IdClasificacion2 == clasificacion2).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            if (clasificacion3 != 0) {
                dicDato = dicDato.Where(r => r.Value.IdClasificacion3 == clasificacion3).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            if (idEstructura != 0) {
                dicDato = riesgosDescendientes(idEstructura);
            }
            if (riesgoSinAsignar == true) {
                dicDato = dicDato.Where(r => r.Value.CodRiesgo == null).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            return dicDato;
        }

        public DescriptionStructureModel recuperaConteDefEstructura(int id) {
            DescriptionStructureModel description = new DescriptionStructureModel();

            Conexion.qEstructura_Contenidos_Def.Where(r => r.IdEstructura == id).ToList().ForEach(x => {
                switch (x.Titulo) {
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

        public Dictionary<int, qRiesgosNombres> riesgosDescendientes(int id) {
            List<tEstructura> cuantosHay = Conexion.tEstructura.Where(r => r.idPadre == id).OrderBy(r => r.Orden).ToList();
            List<int> idRiesgos = new List<int>();


            if (cuantosHay.Count() != 0) {
                idRiesgos = Conexion.tEstructura.Where(x => x.idPadre == Convert.ToInt32(id)).Select(x => Convert.ToInt32(x.IdEstructura)).ToList();
                foreach (var idR in idRiesgos) {
                    riesgosDescendientes(idR);
                }
            } else {
                idRiesgos = Conexion.tRelEstructuraRiesgos.Where(x => x.IdEstructura == Convert.ToInt32(id)).Select(x => Convert.ToInt32(x.IdRiesgo)).ToList();

                try {
                    foreach (var idR in idRiesgos) {
                        datosQ.Add(Conexion.qRiesgosNombres.Where(r => r.IdRiesgo == idR).Select(r => r.IdRiesgo).SingleOrDefault(), Conexion.qRiesgosNombres.Where(r => r.IdRiesgo == idR).Select(r => r).SingleOrDefault());
                    }
                } catch (Exception) {
                }

            }

            return datosQ;

        }

        #endregion

        #region RiskController

        #region CRUD relativos a Riesgos
        public void insertarNuevoRiesgo(tRiesgos riesgoNuevo)
        {
            try
            {
                Conexion.tRiesgos.InsertOnSubmit(riesgoNuevo);
                Conexion.SubmitChanges();
                //return Conexion.tRiesgos.Where(r => r.IdRiesgo == riesgoNuevo.IdRiesgo).SingleOrDefault();

            }
            catch (Exception e)
            {
                //return null;
            }
        }

        public void updateRiesgo(tRiesgos riesgo)
        {
            try
            {
                Conexion.SubmitChanges();
                //return riesgo.IdRiesgo;
            }
            catch (Exception)
            {

                //return 0;
            }         
        }

        public bool deleteRiesgo(int idRiesgo)
        {
            try
            {
                var riesgo = Conexion.tRiesgos.Where(r => r.IdRiesgo == idRiesgo).Select(r => r).FirstOrDefault();
                Conexion.tRiesgos.DeleteOnSubmit(riesgo);
                Conexion.SubmitChanges();

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
            return Conexion.tRiesgos.Where(r => r.IdRiesgo == idRiesgo).SingleOrDefault();
        }


        public qRiesgosNombres recuperarQriesgoNombre(int id)
        {
            qRiesgosNombres riesgoRecup = new qRiesgosNombres();
            if (id != 0)
            {
                riesgoRecup = Conexion.qRiesgosNombres.Where(r => r.IdRiesgo == id).SingleOrDefault();
            }
            return riesgoRecup;
        }

        // metodo que devuelve un string con el ultimo codigo disponible de un idEstructura
        public string ultimoRiesgoDisponible(string idEstructura)
        {
            int cuantosRiesgosTiene = Conexion.tRelEstructuraRiesgos.Where(r => r.IdEstructura == Convert.ToInt32(idEstructura)).Count();
            string ultimoCodigoRiesgo = (cuantosRiesgosTiene + 1).ToString();

            if (cuantosRiesgosTiene.ToString().Length == 1)
            {
                ultimoCodigoRiesgo = "0" + (cuantosRiesgosTiene + 1).ToString();
            }

            string codCompleto = Conexion.tEstructura.Where(r => r.IdEstructura == Convert.ToInt32(idEstructura)).Select(r => r.CodCompleto).SingleOrDefault();

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
                Conexion.tRelEstructuraRiesgos.InsertOnSubmit(estructuraNuevo);
                Conexion.SubmitChanges();
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
                var riesgoTrel = Conexion.tRelEstructuraRiesgos.Where(r => r.IdRiesgo == idRiesgo).Select(r => r).FirstOrDefault();
                Conexion.tRelEstructuraRiesgos.DeleteOnSubmit(riesgoTrel);
                Conexion.SubmitChanges();
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
                Conexion.tRiesgosEvaluaciones.InsertOnSubmit(evaluacion);
                Conexion.SubmitChanges();
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
                Conexion.SubmitChanges();
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
                var riesgoTEval = Conexion.tRiesgosEvaluaciones.Where(r => r.IdRiesgo == idRiesgo).Select(r => r).FirstOrDefault();
                Conexion.tRiesgosEvaluaciones.DeleteOnSubmit(riesgoTEval);
                Conexion.SubmitChanges();
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
                var evaluacion= Conexion.tRiesgosEvaluaciones.Where(r => r.IdRiesgo == idRiesgo && r.IdEvaluacion == idEvaluacion).Select(r => r).FirstOrDefault();
                Conexion.tRiesgosEvaluaciones.DeleteOnSubmit(evaluacion);
                Conexion.SubmitChanges();
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
                dicEvaluacionesRiesgo = Conexion.qRiesgosEvalVal.Where(r => r.IdRiesgo == id && r.IdEvaluacion == idEvaluacion).ToDictionary(r => Convert.ToInt32(r.IdEvaluacion), r => r);
            }
            else
            {
                dicEvaluacionesRiesgo = Conexion.qRiesgosEvalVal.Where(r => r.IdRiesgo == id && r.Ultima == true).ToDictionary(r => Convert.ToInt32(r.IdEvaluacion), r => r);
            }

            return dicEvaluacionesRiesgo;
        }

        public int recuperaIdUltimaEvaluacion(int id)
        {
            return Conexion.qRiesgosEvalVal.Where(r => r.IdRiesgo == id && r.Ultima == true).Select(r => Convert.ToInt32(r.IdEvaluacion)).SingleOrDefault();
        }

        public  tRiesgosEvaluaciones recuperaTRiesgosEvaluacion(int idEvaluacion)
        {
            return Conexion.tRiesgosEvaluaciones.Where(r => r.IdEvaluacion == idEvaluacion).SingleOrDefault();
        }

        public List<tRiesgosEvaluaciones> recuperaEvaluaciones(int idRiesgo)
        {
            return Conexion.tRiesgosEvaluaciones.Where(r => r.IdRiesgo == idRiesgo).ToList();
        }


        public bool cambiaUltimasAFalseEvaluaciones(int idRiesgo)
        {
            bool cambios = true;
            List<tRiesgosEvaluaciones> evaluacionesPorRiesgo = recuperaEvaluaciones(idRiesgo);

            if(evaluacionesPorRiesgo.Count != 0)
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