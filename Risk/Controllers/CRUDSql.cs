using Risk.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

// --------------------------------- CRUD CON DATASET ------------------------------------------------------------

namespace Risk.Controllers {
    public class CRUDSql {

        DataSet miDataSet = new DataSet();
        SqlConnection conexionBD;
        SqlCommand cmd = new System.Data.SqlClient.SqlCommand();


        public CRUDSql() {
            string cadena = ConfigurationManager.ConnectionStrings["RiskMVCConnectionString"].ConnectionString;
            string connectionString = cadena.Split(new[] { "Password=" }, StringSplitOptions.None)[1].Replace("\"", "");
            EncritPass encryt = new EncritPass();
            string passOK = encryt.Desencrit(connectionString);
            conexionBD = new SqlConnection(cadena.Replace(connectionString, passOK));
        }



        public int insert(string tabla, List<Tuple<string, string>> listaValues, string flag) {

            string queryvalues = "";
            string queryproperties = "";

            string query2 = "SELECT CAST(scope_identity() AS int)";
            int ID;

            foreach (Tuple<string, string> item in listaValues) {
                if (item.Item1 == "IdRiesgo" || item.Item1 == "idEstructura") { continue; } else {
                    string propiedad = item.Item1;
                    string valor = item.Item2.Split(':')[0];

                    queryproperties += propiedad + ",";
                    queryvalues += "'" + valor + "',";
                }
            }

            queryproperties = queryproperties.Substring(0, queryproperties.Length - 1);
            queryvalues = queryvalues.Substring(0, queryvalues.Length - 1);
            string query = "Insert into " + tabla + " (" + queryproperties + ") values (" + queryvalues + ")";


            try {

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = conexionBD;

                conexionBD.Open();
                cmd.ExecuteNonQuery();

                cmd.CommandText = query2;
                ID = (int)cmd.ExecuteScalar();
                return ID;
            } catch (SqlException) {
                return 0;
            } finally {
                conexionBD.Close();
            }


        }



        public int update(string tabla, List<Tuple<string, string>> listaValues, string flag, string condicion) {
            string query = "UPDATE " + tabla + " SET ";


            foreach (Tuple<string, string> item in listaValues) {
                string propiedad = item.Item1;
                string valor = item.Item2;

                query += propiedad + " = '" + valor + "',";

            }

            query = query.Substring(0, query.Length - 1);
            query += " WHERE " + condicion;

            try {

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = conexionBD;

                conexionBD.Open();
                return cmd.ExecuteNonQuery();

            } catch (SqlException) {
                return 0;
            } finally {
                conexionBD.Close();
            }

        }


        public int delete(string tabla, string condicion) {
            string query = "DELETE from" + tabla + " where " + condicion;

            try {

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = conexionBD;

                conexionBD.Open();
                return cmd.ExecuteNonQuery();

            } catch (SqlException) {
                return 0;
            } finally {
                conexionBD.Close();
            }

        }
    }
}
