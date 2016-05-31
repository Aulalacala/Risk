using Risk.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;


// --------------------------------- CRUD SQL CON DBMLS ------------------------------------------------------------

namespace Risk.Controllers {
    public class CRUDSql_Switch {

        private Riesgos_BDDataContext _conexionRiesgos;
        private Usuarios_BDDataContext _conexionUsuarios;

        public Riesgos_BDDataContext ConexionRiesgos {
            get {
                Riesgos_BDDataContext riesgos_BD = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();

                return riesgos_BD;
            }

            set {
                _conexionRiesgos = value;
            }
        }

        public Usuarios_BDDataContext ConexionUsuarios {
            get {
                Usuarios_BDDataContext usuarios_BD = (Usuarios_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralUsu();
                return usuarios_BD;
            }

            set {
                _conexionUsuarios = value;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="listaValues"></param>
        /// <param name="flag">Para saber a que dataContext conectarse de los modelos DBML</param>
        /// <returns>IdRiesgo</returns>
        public int insert(string tabla, List<Tuple<string, string>> listaValues, string flag) {

            string queryvalues = "";
            string queryproperties = "";

            foreach (Tuple<string, string> item in listaValues) {              
                    string propiedad = item.Item1;
                    string valor = item.Item2;

                    queryproperties += propiedad + ",";
                    queryvalues += "'" + valor + "',";                
            }

            queryproperties = queryproperties.Substring(0, queryproperties.Length - 1);
            queryvalues = queryvalues.Substring(0, queryvalues.Length - 1);
            string query = "Insert into " + tabla + " (" + queryproperties + ") values (" + queryvalues + ");SELECT CAST(scope_identity() AS int)";

            try {
                switch (flag) {
                    case "riesgos":
                        var rowAfectedR = ConexionRiesgos.ExecuteQuery<int>(query).Single();
                        return rowAfectedR;
                    case "usuarios":
                        var rowAfectedU = ConexionUsuarios.ExecuteQuery<int>(query).SingleOrDefault();
                        return rowAfectedU;
                    default: return 0;
                }

            } catch (Exception e) {
                string ex = e.ToString();
                return 0;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="listaValues"></param>
        /// <param name="flag">Para saber a que dataContext conectarse de los modelos DBML</param>
        /// <param name="condicion">Where de la sentencia SQL</param>
        /// <returns>Número de tuplas modificadas</returns>
        public int update(string tabla, List<Tuple<string, string>> listaValues, string flag, string condicion) {
            string query = "UPDATE "+ tabla + " SET ";


            foreach (Tuple<string,string> item in listaValues) {
                string propiedad = item.Item1;
                string valor = item.Item2;

                query += propiedad + " = '" + valor + "',";

            }

            query = query.Substring(0, query.Length - 1);
            query += " WHERE " + condicion;

            try {
                switch (flag) {
                    case "riesgos":
                        var rowAfectedR = ConexionRiesgos.ExecuteQuery<int>(query).SingleOrDefault();
                        return rowAfectedR;
                    case "usuarios":
                        var rowAfectedU = ConexionUsuarios.ExecuteQuery<int>(query).SingleOrDefault();
                        return rowAfectedU;
                    default: return 0;
                }

            } catch (Exception e) {
                string ex = e.ToString();
                return 0;
            }
        }


        public int delete(string tabla, string flag, string condicion) {
            string query = "DELETE from" + tabla + " where " + condicion;

            try {
                switch (flag) {
                    case "riesgos":
                        var rowAfectedR = ConexionRiesgos.ExecuteQuery<int>(query).SingleOrDefault();
                        return rowAfectedR;
                    case "usuarios":
                        var rowAfectedU = ConexionUsuarios.ExecuteQuery<int>(query).SingleOrDefault();
                        return rowAfectedU;
                    default: return 0;
                }

            } catch (Exception e) {
                string ex = e.ToString();
                return 0;
            }
        }
    }
}
