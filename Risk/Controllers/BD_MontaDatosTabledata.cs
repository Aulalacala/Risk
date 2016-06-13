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

        //HEAD DE LA TABLA
        public Dictionary<string, string> cargaTHead(string nombreTabla, string colVer, string colTitulos)
        {
            Dictionary<string, string> nombreColumnasModif = new Dictionary<string, string>();
            try
            {
                MetaTable TablaDBO;

                //Manera de bifurcar la conexión. Depende de en que archivo .dbml esté la tabla o vista
                if (nombreTabla.Contains("Indicadores") || nombreTabla.Contains("Planes"))
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
    }
}