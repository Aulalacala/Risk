using Risk.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using System.Data.Linq;

namespace Risk.Controllers
{




    public class BD_MontaDatosTabledata
    {
        Riesgos_BDDataContext ConexionRiesgos = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();
        Consultas_BDDataContext ConexionConsultas = (Consultas_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralConsultas();

        BD_Riesgos BD_Riesgos = new BD_Riesgos();

        //Recuperar THEAD tabla Datos Risk-----------------
        public Dictionary<string, string> nombresColTabla(string nombreTabla, string colVer, string colTitulos)
        {
            Dictionary<string, string> nombreColumnasModif = new Dictionary<string, string>();

            try
            {
                MetaTable TablaDBO;

                if (nombreTabla.Contains("Indicadores"))
                {
                    TablaDBO = ConexionConsultas.Mapping.GetTables().Where(t => t.TableName == "dbo." + nombreTabla).Select(t => t).SingleOrDefault();
                }
                else
                {
                    TablaDBO = ConexionRiesgos.Mapping.GetTables().Where(t => t.TableName == "dbo." + nombreTabla).Select(t => t).SingleOrDefault();
                }
              

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
            catch (Exception e)
            {
                return null;
            }
            return nombreColumnasModif;
        }


        public Dictionary<int, List<Tuple<string, string>>> cargaTBody(Dictionary<int, object> dicTbody, Dictionary<string, string> dicThead)
        {
            Dictionary<int, List<Tuple<string, string>>> listaDatos = new Dictionary<int, List<Tuple<string, string>>>();

            try
            {
                //string query = "select * from " + nombreTabla;

                //switch (nombreTabla)
                //{
                //    case "qRiesgosNombres":
                //        Dictionary<int, qRiesgosNombres> dicRiesgos = ConexionRiesgos.ExecuteQuery<qRiesgosNombres>(query).ToDictionary(r => r.IdRiesgo, r => r);
                //        Dictionary<int, qRiesgosNombres> dicFiltrado = busquedasQRiesgosNombres(dicRiesgos, filtro, categoria, clasificacion1, clasificacion2, clasificacion3, idEstructura, riesgoSinAsignar);
                //        dic = dicFiltrado.ToDictionary(r => r.Key, r => (object)r.Value);
                //        break;

                //    case "qRiesgosEvalVal":
                //        Dictionary<int, qRiesgosEvalVal> dicEvaluaciones = ConexionRiesgos.ExecuteQuery<qRiesgosEvalVal>(query).Where(r => r.IdRiesgo == idEstructura).ToDictionary(r => Convert.ToInt32(r.IdEvaluacion), r => r);
                //        dic = dicEvaluaciones.ToDictionary(r => r.Key, r => (object)r.Value);
                //        break;

                //    case "qPlanes":
                //        Dictionary<int, qPlanes> dicPlanes = ConexionConsultas.ExecuteQuery<qPlanes>(query).ToDictionary(r => Convert.ToInt32(r.IdPlanAccion), r => r);
                //        dic = dicPlanes.ToDictionary(r => r.Key, r => (object)r.Value);
                //        break;
                //}

                // Cargar de las columnas a mostrar
                Dictionary<string, string> nombreCols = dicThead;

                foreach (var riesgo in dicTbody)
                {
                    if (riesgo.Value != null)
                    {
                        List<Tuple<string, string>> camposTabla = new List<Tuple<string, string>>();

                        foreach (var col in nombreCols)
                        {

                            string name;
                            System.Reflection.PropertyInfo x = riesgo.Value.GetType().GetProperty(col.Key);
                            var tipo = "";

                            if (x.GetValue(riesgo.Value, null) == null)
                            {

                                name = " ";
                            }
                            else
                            {
                                tipo = x.GetValue(riesgo.Value, null).GetType().Name;
                                name = (string)((x.GetValue(riesgo.Value, null))).ToString();
                            }
                            camposTabla.Add(new Tuple<string, string>(tipo, name));
                        }
                        listaDatos.Add(riesgo.Key, camposTabla);
                    }
                }
            }
            catch (Exception e)
            {
                var exception = e;
                return null;
            }
            return listaDatos;
        }


        //public Dictionary<int, qRiesgosNombres> busquedasQRiesgosNombres(Dictionary<int, qRiesgosNombres> dicDato, string filtro = null, int categoria = 0, int clasificacion1 = 0, int clasificacion2 = 0, int clasificacion3 = 0, int idEstructura = 0, bool riesgoSinAsignar = false)
        //{
        //    if (!string.IsNullOrEmpty(filtro))
        //    {
        //        dicDato = dicDato.Where(r => r.Value.Nombre.Contains(filtro)).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
        //    }

        //    if (categoria != 0)
        //    {
        //        dicDato = dicDato.Where(r => r.Value.IdCategoria == categoria).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
        //    }

        //    if (clasificacion1 != 0)
        //    {
        //        dicDato = dicDato.Where(r => r.Value.IdClasificacion1 == clasificacion1).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
        //    }

        //    if (clasificacion2 != 0)
        //    {
        //        dicDato = dicDato.Where(r => r.Value.IdClasificacion2 == clasificacion2).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
        //    }

        //    if (clasificacion3 != 0)
        //    {
        //        dicDato = dicDato.Where(r => r.Value.IdClasificacion3 == clasificacion3).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
        //    }

        //    if (idEstructura != 0)
        //    {
        //        dicDato = BD_Riesgos.riesgosDescendientes(idEstructura);
        //    }
        //    if (riesgoSinAsignar == true)
        //    {
        //        dicDato = dicDato.Where(r => r.Value.CodRiesgo == null).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
        //    }

        //    return dicDato;
        //}

        public Dictionary<int, object> filtrosRiesgos(Dictionary<string, object> filtros)
        {

            Dictionary<int, qRiesgosNombres> result = new Dictionary<int, qRiesgosNombres>();

            Dictionary<int, object> resultadosBusqueda = new Dictionary<int, object>();

            if (filtros.Count == 0)
            {
                result = ConexionRiesgos.qRiesgosNombres.ToDictionary(r => r.IdRiesgo, r => r);
            }
            else
            {
                for (int i = 0; i < filtros.Count(); i++)
                {
                    var item = filtros.ElementAt(i);

                    if (i == 0)
                    {
                        var tipo = "== @0";
                        if (item.Value.GetType().Name == "String")
                        {
                            tipo = ".Contains(@0)";
                        }

                        result = ConexionRiesgos.qRiesgosNombres
                    .Where(item.Key + tipo, item.Value)
                    .ToDictionary(r => r.IdRiesgo, r => r);

                    }
                    else
                    {
                        result = result.Values
                        .Where(item.Key + "== @0", item.Value)
                        .ToDictionary(r => r.IdRiesgo, r => r);
                    }

                }
            }

            resultadosBusqueda = result.ToDictionary(r => r.Key, r => (object)r.Value);
            return resultadosBusqueda;
        }

    }
}