using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Risk.Models;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Risk.Controllers
{


    public class BD_Riesgos
    {
        Riesgos_BDDataContext riesgosBD = new Riesgos_BDDataContext();


        #region AssignController
        // Cargar todas las categorias en un dictionary ----------------------------------------------
        public Dictionary<int, string> listadoCategorias()
        {

            Dictionary<int, string> dicCategorias = new Dictionary<int, string>();
            try
            {
                dicCategorias = riesgosBD.tRiesgos_Categorias.ToDictionary(r => r.IdCategoria, r => r.Categoria);
            }
            catch (Exception)
            {

                return null;
            }

            return dicCategorias;
        }


        // Cargar clasificaciones de nivel 2 (A,B,C,D) -------------------------------------------------
        public Dictionary<int, string> listadoClasif1()
        {

            Dictionary<int, string> dicClasif1 = new Dictionary<int, string>();
            try
            {
                dicClasif1 = riesgosBD.tRiesgos_Clasificaciones.Where(r => r.Nivel == 2).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
            }
            catch (Exception)
            {

                return null;
            }

            return dicClasif1;
        }

        // Cargar clasificaciones de riesgos de nivel 3 y 4 segun idEstructura del padre ---------------------
        // Uso del metodo para carga dinamica mediante jquery ------------------------------------------------
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

        //Recuperar THEAD tabla Datos Risk-----------------
        public Dictionary<string, string> nombresColTabla(string nombreTabla, string colVer, string colTitulos)
        {
            Dictionary<string, string> nombreColumnasModif = new Dictionary<string, string>();

            try
            {
                MetaTable TablaDBO = riesgosBD.Mapping.GetTables().Where(t => t.TableName == nombreTabla).Select(t => t).SingleOrDefault();

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

        public string recuperaNombreColPK(string nombreTabla)
        {
            string nombrePK = "";

            foreach (var tabla in riesgosBD.Mapping.GetTables())
            {
                if (tabla.TableName.Equals(nombreTabla))
                {
                    foreach (var col in tabla.RowType.DataMembers)
                    {
                        if (col.IsPrimaryKey == true)
                        {
                            nombrePK = col.Name;
                        }


                    }
                }
            }

            return nombrePK;
        }



        //Recuperar TBODY tabla Datos Risk  || CON TABLA DEFINIDA ------------------------

        public Dictionary<int, List<object>> datosQRiesgosNombre(string nombreTabla, string colVer, string colTitulos)
        {

            Dictionary<int, List<object>> listaDatosFinal = new Dictionary<int, List<object>>();

            MetaTable TablaDBO = riesgosBD.Mapping.GetTables().Where(t => t.TableName == nombreTabla).Select(t => t).SingleOrDefault();

            MetaModel model = TablaDBO.Model;

            try
            {
                Dictionary<string, string> nombreCols = nombresColTabla(nombreTabla, colVer, colTitulos);

                foreach (var riesgo in riesgosBD.qRiesgosNombres.ToList())
                {

                    List<object> camposRiesgo = new List<object>();

                    foreach (var col in nombreCols)
                    {

                        string name;
                        System.Reflection.PropertyInfo x = riesgo.GetType().GetProperty(col.Key);

                        if (x.GetValue(riesgo, null) == null)
                        {
                            name = "null";
                        }
                        else
                        {
                            name = (string)((x.GetValue(riesgo, null))).ToString();
                        }
                        camposRiesgo.Add(name);

                    }
                    listaDatosFinal.Add(riesgo.IdRiesgo, camposRiesgo);
                }
            }
            catch (Exception)
            {

                return null;
            }

            return listaDatosFinal;
        }



        //Recuperar TBODY tabla Datos Risk  || CON TABLA DEFINIDA ------------------------

        public Dictionary<int, List<object>> datosQRiesgosNombre(string colVer, string colTitulos, string filtro = null, int categoria = 0, int clasificacion1 = 0, int clasificacion2 = 0, int clasificacion3 = 0)
        {

            Dictionary<int, qRiesgosNombres> datosQRiesgosNombre = new Dictionary<int, qRiesgosNombres>();
            Dictionary<int, List<object>> listaDatosFinal = new Dictionary<int, List<object>>();

            //MetaTable TablaDBO = riesgosBD.Mapping.GetTables().Where(t => t.TableName == "dbo.qRiesgosNombres").Select(t => t).SingleOrDefault();

            //MetaModel model = TablaDBO.Model;

            Type tipo = typeof(qRiesgosNombres);

            try
            {

                datosQRiesgosNombre = riesgosBD.qRiesgosNombres.ToDictionary(r => r.IdRiesgo, r => r);

                if (!string.IsNullOrEmpty(filtro))
                {
                    datosQRiesgosNombre = datosQRiesgosNombre.Where(r => r.Value.Nombre.Contains(filtro)).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
                }

                if (categoria != 0)
                {
                    datosQRiesgosNombre = datosQRiesgosNombre.Where(r => r.Value.IdCategoria == categoria).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
                }

                if (clasificacion1 != 0)
                {
                    datosQRiesgosNombre.Where(r => r.Value.IdClasificacion1 == clasificacion1).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
                }

                if (clasificacion2 != 0)
                {
                    datosQRiesgosNombre.Where(r => r.Value.IdClasificacion2 == clasificacion2).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
                }

                if (clasificacion3 != 0)
                {
                    datosQRiesgosNombre.Where(r => r.Value.IdClasificacion3 == clasificacion3).ToDictionary(r => r.Value.IdRiesgo, r => r.Value);
                }

                // Cargar de las columnas a mostrar
                Dictionary<string, string> nombreCols = nombresColTabla("dbo.qRiesgosNombres", colVer, colTitulos);

                foreach (var riesgo in datosQRiesgosNombre)
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
            catch (Exception)
            {

                return null;
            }
            return listaDatosFinal;
        }
        #endregion



        #region RiskController
        public qRiesgosNombres recuperarRiesgo(int id)
        {
            qRiesgosNombres riesgoRecup = new qRiesgosNombres();
            riesgoRecup = riesgosBD.qRiesgosNombres.Where(r => r.IdRiesgo == id).SingleOrDefault();
            return riesgoRecup;
        }


        public qRiesgos_Evaluaciones__Valores recuperaEvaluaciones(int id)
        {
            qRiesgos_Evaluaciones__Valores datosEvaluaciones = new qRiesgos_Evaluaciones__Valores();
            datosEvaluaciones = riesgosBD.qRiesgos_Evaluaciones__Valores.Where(r => r.IdRiesgo == id && r.Ultima == true).SingleOrDefault();
            return datosEvaluaciones;
        }


    }

    #endregion
}