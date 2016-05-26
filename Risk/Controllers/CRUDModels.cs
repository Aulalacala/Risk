using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Risk.Controllers {
    public class CRUDModels {

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

      
    }
}