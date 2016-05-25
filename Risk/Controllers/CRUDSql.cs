using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Risk.Controllers {
    public class CRUDSql {

        private ConnectionDB.connectionRiesgos riesgosBD = new ConnectionDB.connectionRiesgos();
        private ConnectionDB.connectionUsuarios usuariosBD = new ConnectionDB.connectionUsuarios();



        public bool insert(string tabla, List<Tuple<string, string>> listaValues) {

            

            riesgosBD.DB.Mapping.GetTables();

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
            string query = "Insert into " + tabla + " (" + queryproperties + ") values (" + queryvalues + ")";


            try {
                riesgosBD.DB.ExecuteQuery<int>(query);
                return true;

            } catch (Exception) { return false; }

        }



        public bool update() {
            return false;
        }
        public bool delete() {
            return false;
        }
    }
}