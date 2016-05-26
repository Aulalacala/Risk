using Risk.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;



namespace Risk.Controllers {
    public class CRUDSql {

        private Riesgos_BDDataContext _conexionRiesgos;
        private DataContext _conexionUsuarios;

        public Riesgos_BDDataContext ConexionRiesgos {
            get {
                Riesgos_BDDataContext riesgos_BD = (Riesgos_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralRiesgos();

                return riesgos_BD;
            }

            set {
                _conexionRiesgos = value;
            }
        }

        public DataContext ConexionUsuarios {
            get {
                Usuarios_BDDataContext usuarios_BD = (Usuarios_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralUsu();
                return usuarios_BD;
            }

            set {
                _conexionUsuarios = value;
            }
        }




        public int insert(string tabla, List<Tuple<string, string>> listaValues, string flag) {

            string queryvalues = "";
            string queryproperties = "";

            foreach (Tuple<string, string> item in listaValues) {
                if (item.Item1 == "IdRiesgo" || item.Item1 == "idEstructura") { continue; }
                else {
                    string propiedad = item.Item1;
                    string valor = item.Item2.Split(':')[0];

                    queryproperties += propiedad + ",";
                    queryvalues += "'" + valor + "',";
                }
            }

            queryproperties = queryproperties.Substring(0, queryproperties.Length - 1);
            queryvalues = queryvalues.Substring(0, queryvalues.Length - 1);
            string query = "Insert into " + tabla + " (" + queryproperties + ") values (" + queryvalues + ");SELECT CAST(scope_identity() AS int)";

            try {
                switch (flag) {
                    case "riesgos":
                        var rowAfectedR =(int)ConexionRiesgos.ExecuteQuery<int>(query).Single();
                        return rowAfectedR;
                    case "usuarios":
                       var  rowAfectedU = ConexionUsuarios.ExecuteQuery<int>(query).SingleOrDefault();
                        return rowAfectedU;
                    default: return 0;
                }

            } catch (Exception e) {
                string ex = e.ToString();
                return 0;
            }

        }



        public bool update() {
            return false;
        }
        public bool delete() {
            return false;
        }
    }
}