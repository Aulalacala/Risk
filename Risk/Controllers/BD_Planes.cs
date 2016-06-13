using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Controllers
{
    public class BD_Planes
    {
        Consultas_BDDataContext ConexionPlanes = (Consultas_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralConsultas();

        #region PlanController

        #region Consultas relativas a Planes de accion
        public qPlanes recuperaPlan(int id)
        {
            qPlanes planRecuperado = new qPlanes();
            if (id != 0)
            {
                planRecuperado = ConexionPlanes.qPlanes.Where(r => r.IdPlanAccion == id).SingleOrDefault();
            }
            return planRecuperado;
        }
        #endregion

        #endregion
    }
}