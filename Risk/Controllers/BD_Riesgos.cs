using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Risk.Models;
namespace Risk.Controllers
{
    public class BD_Riesgos
    {
        Riesgos_BDDataContext riesgosBD = new Riesgos_BDDataContext();

        public List<tRiesgos_Categoria> listadoCategorias()
        {

            List<tRiesgos_Categoria> listCategorias = new List<tRiesgos_Categoria>();
            try
            {
                listCategorias = riesgosBD.tRiesgos_Categorias.ToList();
            }
            catch (Exception)
            {

                return null;
            }

            return listCategorias;
        }

        public List<tRiesgos_Clasificaciones> listadoClasif1()
        {

            List<tRiesgos_Clasificaciones> listClasif1 = new List<tRiesgos_Clasificaciones>();
            try
            {
                listClasif1 = riesgosBD.tRiesgos_Clasificaciones.Where(r => r.Nivel == 2).ToList();
            }
            catch (Exception)
            {

                return null;
            }

            return listClasif1;
        }

        public List<tRiesgos_Clasificaciones> listadoClasif2(int idEstructura)
        {
            List<tRiesgos_Clasificaciones> listClasif2 = new List<tRiesgos_Clasificaciones>();
            try
            {
                listClasif2 = riesgosBD.tRiesgos_Clasificaciones.Where(r => r.idPadre == idEstructura).ToList();
            }
            catch (Exception)
            {

                return null;
            }

            return listClasif2;
        }


    }
}