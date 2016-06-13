using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Controllers
{
    public class BD_IndicadoresKRIS
    {
        Consultas_BDDataContext ConexionIndicadores = (Consultas_BDDataContext)new ConnectionDB.connectionGeneral().connectionGeneralConsultas();

        #region KRISController

        public qIndicadores recuperarQIndicadores(int id) {

            qIndicadores indicadorRecup = new qIndicadores();
            if (id != 0) {
                indicadorRecup = ConexionIndicadores.qIndicadores.Where(i => i.IdIndicador == id).SingleOrDefault();
            }
            return indicadorRecup;
        }

        #region CRUD relativo a KRIS

        public bool deleteKRIS(int id)
        {
            try
            {
                var indicador = ConexionIndicadores.qIndicadores.Where(r=>r.IdIndicador== id).Select(r => r).FirstOrDefault();

                ConexionIndicadores.qIndicadores.DeleteOnSubmit(indicador);
                ConexionIndicadores.SubmitChanges();
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