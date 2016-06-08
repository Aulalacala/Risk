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
    }
}