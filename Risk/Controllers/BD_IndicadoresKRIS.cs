using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Controllers
{
    public class BD_IndicadoresKRIS
    {
        Consultas_BDDataContext Conexion = (Consultas_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralConsultas();






        #region KRISController

        #region CRUD relativo a KRIS

        //TODO: Cambiar la tabla.Tiene que ser de KRIS
        public bool deleteKRIS(int id)
        {
            try
            {
                //var KRIS = ConexionRiesgos.tRiesgos.Where(r => r.IdRiesgo == id).Select(r => r).FirstOrDefault();
                //ConexionRiesgos.tRiesgos.DeleteOnSubmit(KRIS);
                //ConexionRiesgos.SubmitChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion


        #endregion
    }
}