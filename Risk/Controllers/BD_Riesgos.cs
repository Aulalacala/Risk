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
        private object context;

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

        public List<string> nombresColTabla(string nombreTabla)
        {
            List<string> nombreColumnas = new List<string>();

            try
            {
                var schema = riesgosBD.Mapping.GetTables();

                foreach (var tabla in riesgosBD.Mapping.GetTables())
                {
                    if (tabla.TableName.Equals(nombreTabla))
                    {
                        foreach (var item in tabla.RowType.DataMembers)
                        {
                            if (!item.Name.Contains("Id"))
                            {
                                nombreColumnas.Add(item.Name);
                            }                        
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
            return nombreColumnas;
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



        //Recuperar TBODY tabla Datos Risk  || CON TABLA DINAMICA ------------------------

        //      public Dictionary<int, List<object>> datosTabla(string nombreTabla)
        //      {
        //          Dictionary<int, object> todosLosObjetos = new Dictionary<int, object>();
        //          List<object> list = new List<object>();

        //          Dictionary<int, List<object>> listaDatosFinal = new Dictionary<int, List<object>>();

        //          try
        //          {
        //              // Carga de todos los riesgos <IdRiesgo, riesgo>
        //              MetaTable nombreTablaDBO = riesgosBD.Mapping.GetTables().Where(t => t.TableName == nombreTabla).Select(t => t).SingleOrDefault();

        //nombreTablaDBO.RowType.DataMembers


        //              //Tipo del objeto de la tabla
        //              //Rellenar el dictionary "todoslosObjetos" key=id, value=objeto=tupla
        //              //Pasar el objeto a la lista de camposRiesgo
        //              //Montar el dictionary listadatosFinal key=id.value value=lista camposRiesgo







        //              //    // Recorrer el dictionary 
        //              //        foreach (var riesgo in todosLosObjetos)
        //              //    {
        //              //        List<object> camposRiesgo = new List<object>();
        //              //        List<string> nombreCols = nombresColTabla(nombreTabla);

        //              //        // Lista con los riesgos por IdRiesgo desde el dictionary
        //              //        list = riesgosBD.qRiesgosNombres.Where(r => r.IdRiesgo == riesgo.Key).ToList();

        //              //        foreach (var col in nombreCols)
        //              //        {
        //              //            foreach (var attr in list)
        //              //            {
        //              //                // Lista con las propiedad de cada riesgo

        //              //                string name;
        //              //                System.Reflection.PropertyInfo x = attr.GetType().GetProperty(col);

        //              //                if (x.GetValue(attr, null) == null)
        //              //                {
        //              //                    name = "null";
        //              //                }
        //              //                else
        //              //                {
        //              //                    name = (string)((x.GetValue(attr, null))).ToString();
        //              //                }


        //              //                camposRiesgo.Add(name);
        //              //            }
        //              //        }
        //              //        listaDatosFinal.Add(riesgo.Key, camposRiesgo);
        //              //    }
        //              //}
        //          }
        //          catch (Exception)
        //          {

        //              return null;
        //          }

        //          return listaDatosFinal;
        //      }




        //Recuperar TBODY tabla Datos Risk  || CON TABLA DEFINIDA ------------------------



        //Recuperar TBODY tabla Datos Risk  || CON TABLA DEFINIDA ------------------------

        public Dictionary<int, List<object>> datosQRiesgosNombre()
        {
            Dictionary<int, qRiesgosNombre> datosQRiesgosNombre = new Dictionary<int, qRiesgosNombre>();
            List<qRiesgosNombre> list = new List<qRiesgosNombre>();

            Dictionary<int, List<object>> listaDatosFinal = new Dictionary<int, List<object>>();

            try
            {
                // Carga de todos los riesgos <IdRiesgo, riesgo>
                datosQRiesgosNombre = riesgosBD.qRiesgosNombres.ToDictionary(r => r.IdRiesgo, r => r);

                // Recorrer el dictionary 
                foreach (var riesgo in datosQRiesgosNombre)
                {
                    List<object> camposRiesgo = new List<object>();
                    List<string> nombreCols = nombresColTabla("dbo.qRiesgosNombres");

                    // Lista con los riesgos por IdRiesgo desde el dictionary

                    list = riesgosBD.qRiesgosNombres.Where(r => r.IdRiesgo == riesgo.Key).ToList();

                    foreach (var col in nombreCols)
                    {
                        foreach (var attr in list)
                        {
                            // Lista con las propiedad de cada riesgo

                            string name;
                            System.Reflection.PropertyInfo x = attr.GetType().GetProperty(col);

                            if (x.GetValue(attr, null) == null)
                            {
                                name = "null";
                            }
                            else
                            {
                                name = (string)((x.GetValue(attr, null))).ToString();
                            }
                            camposRiesgo.Add(name);
                        }
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

        public Dictionary<int, List<object>> datosQRiesgosNombre(string filtro, int categoria, int clasificacion1, int clasificacion2, int clasificacion3)
        {
            Dictionary<int, qRiesgosNombre> datosQRiesgosNombre = new Dictionary<int, qRiesgosNombre>();
            List<qRiesgosNombre> list = new List<qRiesgosNombre>();

            Dictionary<int, List<object>> listaDatosFinal = new Dictionary<int, List<object>>();

            try
            {
                // Carga de todos los riesgos <IdRiesgo, riesgo>
                datosQRiesgosNombre = riesgosBD.qRiesgosNombres.Where(r => r.Nombre.Contains(filtro) && r.IdCategoria == categoria && r.IdClasificacion1 == clasificacion1 && r.IdClasificacion2 == clasificacion2 && r.IdClasificacion3 == clasificacion3).ToDictionary(r => r.IdRiesgo, r => r);

                // Recorrer el dictionary 
                foreach (var riesgo in datosQRiesgosNombre)
                {
                    List<object> camposRiesgo = new List<object>();
                    List<string> nombreCols = nombresColTabla("dbo.qRiesgosNombres");

                    // Lista con los riesgos por IdRiesgo desde el dictionary

                    list = riesgosBD.qRiesgosNombres.Where(r => r.IdRiesgo == riesgo.Key).ToList();

                    foreach (var col in nombreCols)
                    {
                        foreach (var attr in list)
                        {
                            // Lista con las propiedad de cada riesgo

                            string name;
                            System.Reflection.PropertyInfo x = attr.GetType().GetProperty(col);

                            if (x.GetValue(attr, null) == null)
                            {
                                name = "null";
                            }
                            else
                            {
                                name = (string)((x.GetValue(attr, null))).ToString();
                            }
                            camposRiesgo.Add(name);
                        }
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
        public qRiesgosNombre recuperarRiesgo(int id)
        {
            qRiesgosNombre riesgoRecup = new qRiesgosNombre();
            riesgoRecup = riesgosBD.qRiesgosNombres.Where(r => r.IdRiesgo == id).SingleOrDefault();
            return riesgoRecup;
        }
    }

    #endregion
}