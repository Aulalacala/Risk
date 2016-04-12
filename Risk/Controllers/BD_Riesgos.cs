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







        public Dictionary<string, string> nombresColTabla(string nombreTabla, string colVer, string colTitulos)
        {
            List<string> nombreColumnas = new List<string>();
            Dictionary<string, string> nombreColumnasModif = new Dictionary<string, string>();

           
            try
            {
                //DataContext DataContext = new DataContext("Data Source=SONY-PC\\SQL2008EXPR;Initial Catalog=Risk;Persist Security Info=True;User ID=sa;Password=aula1111");
                MetaTable TablaDBO = riesgosBD.Mapping.GetTables().Where(t => t.TableName == nombreTabla).Select(t => t).SingleOrDefault();

                if (string.IsNullOrEmpty(colVer)) //como estan TODOS en la BD
                {
                    foreach (var item in TablaDBO.RowType.DataMembers)
                    {
                        if (!item.Name.Contains("Id"))
                        {
                            nombreColumnas.Add(item.Name);
                            nombreColumnasModif.Add(item.Name, item.Name);
                        }
                    }
                }
                else if(string.IsNullOrEmpty(colTitulos)) //como estan en la BD=> los que quiere el USUARIO
                {
                    List<string> ver = colVer.Split(',').ToList();

                    foreach (var item in TablaDBO.RowType.DataMembers)
                    {
                        foreach (var colVerItem in ver)
                        {
                            if (item.Name.Equals(colVerItem))
                            {
                                nombreColumnas.Add(item.Name);
                                nombreColumnasModif.Add(item.Name, item.Name);
                            }
                        }                       
                    }
                }
                else //los que quiere el USUARIO modificado el nombre
                {
                    List<string> ver = colVer.Split(',').ToList();
                    List<string> titulos = colTitulos.Split(',').ToList();

                    foreach (var item in TablaDBO.RowType.DataMembers)
                    {
                        for (int i = 0; i < ver.Count; i++)
                        {
                            if (item.Name.Equals(ver[i]))
                            {
                                nombreColumnasModif.Add(item.Name, titulos[i]);

                            }
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

        public Dictionary<int, List<object>> datosQRiesgosNombre(string colVer, string colTitulos)
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
                    Dictionary<string,string> nombreCols = nombresColTabla("dbo.qRiesgosNombres", colVer,colTitulos);

                    // Lista con los riesgos por IdRiesgo desde el dictionary

                    list = riesgosBD.qRiesgosNombres.Where(r => r.IdRiesgo == riesgo.Key).ToList();

                    foreach (var col in nombreCols)
                    {                     
                        foreach (var attr in list)
                        {
                            // Lista con las propiedad de cada riesgo

                            string name;
                            System.Reflection.PropertyInfo x = attr.GetType().GetProperty(col.Key);

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

        public Dictionary<int, List<object>> datosQRiesgosNombre(string filtro, int categoria, int clasificacion1, int clasificacion2, int clasificacion3, string colVer, string colTitulos)
        {
            Dictionary<int, qRiesgosNombre> datosQRiesgosNombre = new Dictionary<int, qRiesgosNombre>();
            List<qRiesgosNombre> list = new List<qRiesgosNombre>();

            Dictionary<int, List<object>> listaDatosFinal = new Dictionary<int, List<object>>();

            try
            {
                // Filtrado por parametro filtro
                datosQRiesgosNombre = !string.IsNullOrEmpty(filtro) ?  riesgosBD.qRiesgosNombres.Where(r => r.Nombre.Contains(filtro)).ToDictionary(r => r.IdRiesgo, r => r) : riesgosBD.qRiesgosNombres.ToDictionary(r => r.IdRiesgo, r => r);
                // Filtrado por parametro categoria
                datosQRiesgosNombre = (categoria != 0) ? datosQRiesgosNombre.Where(r => r.Value.IdCategoria == categoria).ToDictionary(r => r.Value.IdRiesgo, r => r.Value) : datosQRiesgosNombre;
                // Filtrado por parametro Clasificacion1
                datosQRiesgosNombre = (clasificacion1 != 0) ? datosQRiesgosNombre.Where(r => r.Value.IdClasificacion1 == clasificacion1).ToDictionary(r => r.Value.IdRiesgo, r => r.Value) : datosQRiesgosNombre;
                // Filtrado por parametro Clasificacion2
                datosQRiesgosNombre = (clasificacion2 != 0) ? datosQRiesgosNombre.Where(r => r.Value.IdClasificacion2 == clasificacion2).ToDictionary(r => r.Value.IdRiesgo, r => r.Value) : datosQRiesgosNombre;
                // Filtrado por parametro Clasificacion3
                datosQRiesgosNombre = (clasificacion3 != 0) ? datosQRiesgosNombre.Where(r => r.Value.IdClasificacion3 == clasificacion3).ToDictionary(r => r.Value.IdRiesgo, r => r.Value) : datosQRiesgosNombre;

                // Recorrer el dictionary 
                foreach (var riesgo in datosQRiesgosNombre)
                {
                    List<object> camposRiesgo = new List<object>();
                    Dictionary<string, string> nombreCols = nombresColTabla("dbo.qRiesgosNombres", colVer, colTitulos);

                    // Lista con los riesgos por IdRiesgo desde el dictionary
                    list = riesgosBD.qRiesgosNombres.Where(r => r.IdRiesgo == riesgo.Key).ToList();

                    foreach (var col in nombreCols)
                    {
                        foreach (var attr in list)
                        {
                            // Lista con las propiedad de cada riesgo

                            string name;
                            System.Reflection.PropertyInfo x = attr.GetType().GetProperty(col.Key);

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