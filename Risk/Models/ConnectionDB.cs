using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using Risk.Controllers;

namespace Risk.Models
{

  
    public partial class ConnectionDB
    {

       public  enum modelos {
            Riesgos_BD,
            Usuarios_CB
        };

        private static string _cadenaConexion;
        public static string cadenaConexion
        {
            get
            {
                return _cadenaConexion;
            }
            set
            {
                _cadenaConexion = value;
            }
        }


        private  static modelos _modelo;

        public ConnectionDB()
        {
            string cadena = ConfigurationManager.ConnectionStrings["RiskMVCConnectionString"].ConnectionString;
            string connectionString = cadena.Split(new[] { "Password=" }, StringSplitOptions.None)[1].Replace("\"", "");
            EncritPass encryt = new EncritPass();
            string passOK = encryt.Desencrit(connectionString);
            _cadenaConexion = cadena.Replace(connectionString, passOK);
        }


        public static object conexion(modelos model) {
            _modelo = model;
            switch (model) {
                case modelos.Riesgos_BD:
                    return new Riesgos_BDDataContext();
                case modelos.Usuarios_CB:
                    return new Usuarios_BDDataContext();
                default: return null;
            }
        }




        public partial class connectionUsuarios
        {
            private Usuarios_BDDataContext _db;

            public Usuarios_BDDataContext DB
            {
                get
                {
                    return _db;
                }
                set
                {
                    _db = value;
                }
            }


            public connectionUsuarios()
            {
                ConnectionDB con = new ConnectionDB(_modelo);
                _db = new Usuarios_BDDataContext(cadenaConexion);
            }

            public connectionUsuarios(string cadenaConexion)
            {
                _db = new Usuarios_BDDataContext(cadenaConexion);
            }
        }
               

        public partial class connectionRiesgos
        {
            private Riesgos_BDDataContext _db;

            public Riesgos_BDDataContext DB
            {
                get
                {
                    return _db;
                }
                set
                {
                    _db = value;
                }
            }

            public connectionRiesgos()
            {
                ConnectionDB con = new ConnectionDB(_modelo);
                _db = new Riesgos_BDDataContext(cadenaConexion);
            }

            public connectionRiesgos(string cadenaConexion)
            {
                _db = new Riesgos_BDDataContext(cadenaConexion);
            }

        }
    }
}