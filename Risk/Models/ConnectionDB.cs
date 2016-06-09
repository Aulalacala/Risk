using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using Risk.Controllers;

namespace Risk.Models {


    public partial class ConnectionDB {

        private  string _cadenaConexion;
        public  string cadenaConexion {
            get {
                return _cadenaConexion;
            }
            set {
                _cadenaConexion = value;
            }
        }


    
        public ConnectionDB() {
            string cadena = ConfigurationManager.ConnectionStrings["RiskMVCConnectionString"].ConnectionString;
            string connectionString = cadena.Split(new[] { "Password=" }, StringSplitOptions.None)[1].Replace("\"", "");
            EncritPass encryt = new EncritPass();
            string passOK = encryt.Desencrit(connectionString);
            _cadenaConexion = cadena.Replace(connectionString, passOK);
        }

        public partial class connectionGeneral{
            private  Usuarios_BDDataContext _dbUsuarios;
            private Riesgos_BDDataContext _dbRiesgos;
            private Consultas_BDDataContext _dbConsultas;

            public Usuarios_BDDataContext DbUsuarios {
                get {
                    return _dbUsuarios;
                }

                set {
                    _dbUsuarios = value;
                }
            }

            public Riesgos_BDDataContext DbRiesgos {
                get {
                    return _dbRiesgos;
                }

                set {
                    _dbRiesgos = value;
                }
            }

            public Consultas_BDDataContext DbConsultas
            {
                get
                {
                    return _dbConsultas;
                }

                set
                {
                    _dbConsultas = value;
                }
            }

            public connectionGeneral() {
                ConnectionDB con = new ConnectionDB();
            }

            public object connectionGeneralUsu() {
                ConnectionDB con = new ConnectionDB();
                this._dbUsuarios = new Usuarios_BDDataContext(con.cadenaConexion);
                return _dbUsuarios;
            }

            public object connectionGeneralRiesgos() {
                ConnectionDB con = new ConnectionDB();
                this._dbRiesgos = new Riesgos_BDDataContext(con.cadenaConexion);
                return _dbRiesgos;
            }

            public object connectionGeneralConsultas()
            {
                ConnectionDB con = new ConnectionDB();
                this._dbConsultas = new Consultas_BDDataContext(con.cadenaConexion);
                return _dbConsultas;
            }

        }

        #region Forma OLD
        //    public partial class connectionUsuarios {
        //        private static Usuarios_BDDataContext _db;

        //        public static Usuarios_BDDataContext DB {
        //            get {
        //                return _db;
        //            }
        //            set {
        //                _db = value;
        //            }
        //        }


        //        public connectionUsuarios() {
        //            ConnectionDB con = new ConnectionDB();
        //            _db = new Usuarios_BDDataContext(con.cadenaConexion);
        //        }

        //        public connectionUsuarios(string cadenaConexion) {
        //            _db = new Usuarios_BDDataContext(cadenaConexion);
        //        }
        //    }


        //    public partial class connectionRiesgos {
        //        private Riesgos_BDDataContext _db;

        //        public Riesgos_BDDataContext DB {
        //            get {
        //                return _db;
        //            }
        //            set {
        //                _db = value;
        //            }
        //        }

        //        public connectionRiesgos() {
        //            ConnectionDB con = new ConnectionDB();
        //            _db = new Riesgos_BDDataContext(con.cadenaConexion);
        //        }

        //        public connectionRiesgos(string cadenaConexion) {
        //            _db = new Riesgos_BDDataContext(cadenaConexion);
        //        }

        //    }
        #endregion
    }
}