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

        public Dictionary<int, string>  listadoCategorias()
        {

            Dictionary<int, string> dicCategorias = new Dictionary<int, string>();
            try
            {
                dicCategorias = riesgosBD.tRiesgos_Categorias.ToDictionary(r => r.IdCategoria, r => r.Categoria);
            }
            catch (Exception)
            {

                return null;
            }

            return dicCategorias;
        }


        public Dictionary<int, string> listadoClasif1()
        {

            Dictionary<int, string> dicClasif1 = new Dictionary<int, string>();
            try
            {
                dicClasif1 = riesgosBD.tRiesgos_Clasificaciones.Where(r => r.Nivel == 2).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
            }
            catch (Exception)
            {

                return null;
            }

            return dicClasif1;
        }

        public Dictionary<int, string> listadoClasifDinamic(int idEstructura)
        {
            Dictionary<int, string> dicClasif2 = new Dictionary<int, string>();
            
            try
            {
                dicClasif2 = riesgosBD.tRiesgos_Clasificaciones.Where(r => r.idPadre == idEstructura).ToDictionary(r => r.IdEstructura, r => r.CodCompleto + " " + r.Nombre);
            }
            catch (Exception)
            {

                return null;
            }

            return dicClasif2;
        }


    }
}