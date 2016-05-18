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
        //Riesgos_BDDataContext riesgosBD = new Riesgos_BDDataContext();

        ConnectionDB.connectionRiesgos riesgosBD = new ConnectionDB.connectionRiesgos();

        public Dictionary<int, qRiesgosNombres> datosQ = new Dictionary<int, qRiesgosNombres>();

        #region AssignController
        //Recuperar THEAD tabla Datos Risk-----------------
        public Dictionary<string, string> nombresColTabla(string nombreTabla, string colVer, string colTitulos)
        {
            Dictionary<string, string> nombreColumnasModif = new Dictionary<string, string>();

            try
            {
                MetaTable TablaDBO = riesgosBD.DB.Mapping.GetTables().Where(t => t.TableName == "dbo."+nombreTabla).Select(t => t).SingleOrDefault();

                List<string> ver = new List<string>();
                List<string> titulos = new List<string>();

                if (string.IsNullOrEmpty(colVer) && string.IsNullOrEmpty(colTitulos)) //como estan TODOS en la BD
                {
                    foreach (var item in TablaDBO.RowType.DataMembers)
                    {
                        if (!item.Name.Contains("Id"))
                        {
                            nombreColumnasModif.Add(item.Name, item.Name);
                        }
                    }
                }
                else
                {
                    ver = colVer.Split(',').ToList();

                    if (string.IsNullOrEmpty(colTitulos))
                    {
                        titulos = ver;
                    }
                    else
                    {
                        titulos = colTitulos.Split(',').ToList();
                    }

                    foreach (var columnVer in ver)
                    {
                        if (TablaDBO.RowType.DataMembers.Where(r => r.Name.Equals(columnVer)).Select(r => r).SingleOrDefault() != null)
                        {
                            nombreColumnasModif.Add(columnVer, titulos[ver.IndexOf(columnVer)]);
                        }

                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return nombreColumnasModif;
        }

        public Dictionary<int, List<object>> cargaTablaDatos(string nombreTabla , string colVer, string colTitulos, string filtro = null, int categoria = 0, int clasificacion1 = 0, int clasificacion2 = 0, int clasificacion3 = 0, int idEstructura = 0)
        {
            Dictionary<int, object> dic = new Dictionary<int, object>();
            Dictionary<int, List<object>> listaDatosFinal = new Dictionary<int, List<object>>();

            try
            {
                string query = "select * from " + nombreTabla;
                switch (nombreTabla)
                {
                    case "qRiesgosNombres":
                        Dictionary<int, qRiesgosNombres> dicRiesgos = riesgosBD.DB.ExecuteQuery<qRiesgosNombres>(query).ToDictionary(r => r.IdRiesgo, r => r);

                        Dictionary<int, qRiesgosNombres> dicFiltrado = busquedasQRiesgosNombres(dicRiesgos, filtro, categoria, clasificacion1, clasificacion2, clasificacion3, idEstructura);
                        dic = dicFiltrado.ToDictionary(r => r.Key, r => (object)r.Value);
                        break;
                    case "qRiesgos_Evaluaciones_Valores":
                        Dictionary<int, qRiesgos_Evaluaciones_Valores> dicEvaluaciones = riesgosBD.DB.ExecuteQuery<qRiesgos_Evaluaciones_Valores>(query).ToDictionary(r => Convert.ToInt32(r.IdRiesgo), r => r);
                        dic = dicEvaluaciones.ToDictionary(r => r.Key, r => (object)r.Value);
                        break;
                }

                // Cargar de las columnas a mostrar
                Dictionary<string, string> nombreCols = nombresColTabla(nombreTabla, colVer, colTitulos);

                foreach (var riesgo in dic)
                {
                    if (riesgo.Value != null)
                    {
                        List<object> camposRiesgo = new List<object>();

                        foreach (var col in nombreCols)
                        {

                            string name;
                            System.Reflection.PropertyInfo x = riesgo.Value.GetType().GetProperty(col.Key);

                            if (x.GetValue(riesgo.Value, null) == null)
                            {
                                name = "null";
                            }
                            else
                            {
                                name = (string)((x.GetValue(riesgo.Value, null))).ToString();
                            }
                            camposRiesgo.Add(name);
                        }
                        listaDatosFinal.Add(riesgo.Key, camposRiesgo);
                    }
                }
            }
            catch (Exception e)
            {
                var exception = e;
                return null;
            }
            return listaDatosFinal;
        }


        public Dictionary<int, qRiesgosNombres> busquedasQRiesgosNombres(Dictionary<int, qRiesgosNombres> dicDato, string filtro = null, int categoria = 0, int clasificacion1 = 0, int clasificacion2 = 0, int clasificacion3 = 0, int idEstructura = 0)
        {
            if (!string.IsNullOrEmpty(filtro))
            {

                dicDato = dicDato.Where(r => r.Value.Nombre.Contains(filtro) || r.Value.CodRiesgo.Contains(filtro)).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            if (categoria != 0)
            {
                dicDato = dicDato.Where(r => r.Value.IdCategoria == categoria).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            if (clasificacion1 != 0)
            {
                dicDato = dicDato.Where(r => r.Value.IdClasificacion1 == clasificacion1).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            if (clasificacion2 != 0)
            {
                dicDato = dicDato.Where(r => r.Value.IdClasificacion2 == clasificacion2).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            if (clasificacion3 != 0)
            {
                dicDato = dicDato.Where(r => r.Value.IdClasificacion3 == clasificacion3).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
            }

            if (idEstructura != 0)
            {
                dicDato = riesgosDescendientes(idEstructura);
            }

            return dicDato;
        }

        public DescriptionStructureModel recuperaConteDefEstructura(int id)
        {
            DescriptionStructureModel description = new DescriptionStructureModel();

            riesgosBD.DB.qEstructura_Contenidos_Def.Where(r => r.IdEstructura == id).ToList().ForEach(x =>
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

        public Dictionary<int, qRiesgosNombres> riesgosDescendientes(int id)
        {
            List<tEstructura> cuantosHay = riesgosBD.DB.tEstructura.Where(r => r.idPadre == id).OrderBy(r => r.Orden).ToList();
            List<int> idRiesgos = new List<int>();


            if (cuantosHay.Count() != 0)
            {
                idRiesgos = riesgosBD.DB.tEstructura.Where(x => x.idPadre == Convert.ToInt32(id)).Select(x => Convert.ToInt32(x.IdEstructura)).ToList();
                foreach (var idR in idRiesgos)
                {
                    riesgosDescendientes(idR);
                }
            }
            else
            {
                idRiesgos = riesgosBD.DB.tRelEstructuraRiesgos.Where(x => x.IdEstructura == Convert.ToInt32(id)).Select(x => Convert.ToInt32(x.IdRiesgo)).ToList();

                try
                {
                    foreach (var idR in idRiesgos)
                    {
                        datosQ.Add(riesgosBD.DB.qRiesgosNombres.Where(r => r.IdRiesgo == idR).Select(r => r.IdRiesgo).SingleOrDefault(), riesgosBD.DB.qRiesgosNombres.Where(r => r.IdRiesgo == idR).Select(r => r).SingleOrDefault());
                    }
                }
                catch (Exception)
                {
                }

            }

            return datosQ;

        }

        #endregion

        #region RiskController
        public qRiesgosNombres recuperarRiesgo(int id)
        {
            qRiesgosNombres riesgoRecup = new qRiesgosNombres();
            if (id != 0)
            {
                riesgoRecup = riesgosBD.DB.qRiesgosNombres.Where(r => r.IdRiesgo == id).SingleOrDefault();
            }
            return riesgoRecup;
        }


        public qRiesgos_Evaluaciones_Valores recuperaEvaluaciones(int id)
        {
            qRiesgos_Evaluaciones_Valores datosEvaluaciones = new qRiesgos_Evaluaciones_Valores();
            datosEvaluaciones = riesgosBD.DB.qRiesgos_Evaluaciones_Valores.Where(r => r.IdRiesgo == id && r.Ultima == true).SingleOrDefault();
            return datosEvaluaciones;
        }


        public int actualizaQRiesgosNombre(List<string> datosQRiesgosNombre, int IdRiesgo)
        {
            try
            {
                qRiesgosNombres riesgo = riesgosBD.DB.qRiesgosNombres.Where(r => r.IdRiesgo == IdRiesgo).SingleOrDefault();

                string query = "UPDATE qRiesgosNombres SET ";


                foreach (string datos in datosQRiesgosNombre)
                {
                    string propiedad = datos.Split(':')[0];
                    string valor = datos.Split(':')[1];

                    query += propiedad + " = '" + valor + "',";

                }

                query = query.Substring(0, query.Length - 1);
                query += " WHERE IdRiesgo = " + IdRiesgo;

                riesgosBD.DB.ExecuteQuery<qRiesgosNombres>(query);


                qRiesgosNombres riesgo2 = riesgosBD.DB.qRiesgosNombres.Where(r => r.IdRiesgo == IdRiesgo).SingleOrDefault();
                return riesgo2.IdRiesgo;

            }
            catch (Exception) { return 0; }

        }


        public tRiesgos insertarNuevoRiesgo(tRiesgos riesgoNuevo)
        {

            try
            {
                riesgosBD.DB.tRiesgos.InsertOnSubmit(riesgoNuevo);
                riesgosBD.DB.SubmitChanges();
                return riesgosBD.DB.tRiesgos.Where(r => r.IdRiesgo == riesgoNuevo.IdRiesgo).SingleOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public bool insertarTRelEstructuraRiesgoNuevo(tRelEstructuraRiesgos estructuraNuevo)
        {
            try
            {
                riesgosBD.DB.tRelEstructuraRiesgos.InsertOnSubmit(estructuraNuevo);
                riesgosBD.DB.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public bool insertarTRiesgosEvaluaciones(int idRiesgo)
        {
            try
            {
                tRiesgosEvaluaciones evaluacion = new tRiesgosEvaluaciones();
                evaluacion.IdRiesgo = idRiesgo;
                evaluacion.IdNivel = 0;
                evaluacion.Fecha = DateTime.Now;
                evaluacion.Activa = true;
                evaluacion.Ultima = true;
                evaluacion.IdFrecAntes = 0;
                evaluacion.IdSeveAntes = 0;
                evaluacion.IdFrecDespues = 0;
                evaluacion.IdSeveDespues = 0;
                evaluacion.IdSevePeorAntes = 0;
                evaluacion.IdSevePeorDespues = 0;
                evaluacion.idEfectividad = 0;
                evaluacion.Efectividad = 0;
                evaluacion.IdFrecPlanDespues = 0;
                evaluacion.IdSevePlanDespues = 0;
                evaluacion.IdSevePeorPlanDespues = 0;

                riesgosBD.DB.tRiesgosEvaluaciones.InsertOnSubmit(evaluacion);
                riesgosBD.DB.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool deleteRiesgo(int idRiesgo)
        {
            try
            {
                var riesgo = riesgosBD.DB.tRiesgos.Where(r => r.IdRiesgo == idRiesgo).Select(r => r).FirstOrDefault();
                riesgosBD.DB.tRiesgos.DeleteOnSubmit(riesgo);
                riesgosBD.DB.SubmitChanges();

                var riesgoTrel = riesgosBD.DB.tRelEstructuraRiesgos.Where(r => r.IdRiesgo == idRiesgo).Select(r => r).FirstOrDefault();
                riesgosBD.DB.tRelEstructuraRiesgos.DeleteOnSubmit(riesgoTrel);
                riesgosBD.DB.SubmitChanges();

                var riesgoTEval = riesgosBD.DB.tRiesgosEvaluaciones.Where(r => r.IdRiesgo == idRiesgo).Select(r => r).FirstOrDefault();
                riesgosBD.DB.tRiesgosEvaluaciones.DeleteOnSubmit(riesgoTEval);
                riesgosBD.DB.SubmitChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        // metodo que devuelve un string con el ultimo codigo disponible de un idEstructura
        public string ultimoRiesgoDisponible(string idEstructura)
        {
            int cuantosRiesgosTiene = riesgosBD.DB.tRelEstructuraRiesgos.Where(r => r.IdEstructura == Convert.ToInt32(idEstructura)).Count();
            string ultimoCodigoRiesgo = (cuantosRiesgosTiene + 1).ToString();

            if (cuantosRiesgosTiene.ToString().Length == 1)
            {
                ultimoCodigoRiesgo = "0" + (cuantosRiesgosTiene + 1).ToString();
            }

            return ultimoCodigoRiesgo;
        }




        #endregion
    }
}