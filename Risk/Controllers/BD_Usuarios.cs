using Risk.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace Risk.Controllers {
    public class BD_Usuarios {

        private DataContext _conexion;

        public DataContext Conexion {
            get {
                Usuarios_BDDataContext usuarios_BD = (Usuarios_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralUsu();
                return usuarios_BD;
            }

            set {
                _conexion = value;
            }
        }
    }
}