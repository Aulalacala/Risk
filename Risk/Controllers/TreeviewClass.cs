using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Controllers
{
    public class TreeviewClass
    {

        //public static ConnectionDB.connectionRiesgos riesgosBD = new ConnectionDB.connectionRiesgos();

        public static BD_Riesgos BD_Riesgos = new BD_Riesgos();

        public static List<TreeViewLocation> GetLocations(int id)
        {
            var locations = new List<TreeViewLocation>();

            List<tEstructura> cuantosHay = BD_Riesgos.Conexion.tEstructura.Where(r => r.idPadre == id).OrderBy(r => r.Orden).ToList();
            if (cuantosHay.Count != 0)
            {
                for (int i = 0; i < cuantosHay.Count; i++)
                {
                    if (tienesHijos(Convert.ToInt32(cuantosHay[i].idPadre)))
                    {
                        locations.Add(new TreeViewLocation
                        {
                            Id = cuantosHay[i].IdEstructura,
                            Name = cuantosHay[i].CodCompleto.Substring(cuantosHay[i].CodCompleto.Length - 2, 2) + " " + cuantosHay[i].Nombre.ToString(),
                            check = tienesHijos(cuantosHay[i].IdEstructura) == true ? "checked" : tienesHijosRiesgos(cuantosHay[i].IdEstructura),
                            nivel = Convert.ToInt32(cuantosHay[i].Nivel),
                            ChildLocations = GetLocations(Convert.ToInt32(cuantosHay[i].IdEstructura))
                        });
                    }
                }
            }

            return locations;
        }




        //PARA BUSCAR LOS HIJOS HAY QUE METER EL ID DE SU SANTO PADRE
        public static bool tienesHijos(int idPadre)
        {
            bool tieneHijos = false;
            return tieneHijos = BD_Riesgos.Conexion.tEstructura.Where(r => r.idPadre == idPadre).Any() ? tieneHijos = true : tieneHijos = false;
        }

        //PARA BUSCAR LOS HIJOS HAY QUE METER EL ID DE SU SANTO PADRE
        public static string tienesHijosRiesgos(int idEstructura)
        {
            string tieneHijosString = "";
            BD_Riesgos.Conexion.tRelEstructuraRiesgos.Where(r => r.IdEstructura == idEstructura).ToList().ForEach(x =>
            {
                tieneHijosString = BD_Riesgos.Conexion.qRiesgosNombres.Where(c => c.IdRiesgo == x.IdRiesgo).Any() ? tieneHijosString = "checked" : tieneHijosString = "";
            });
            return tieneHijosString;
        }
    }




    public class TreeViewLocation
    {
        public TreeViewLocation()
        {
            ChildLocations = new HashSet<TreeViewLocation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TreeViewLocation> ChildLocations { get; set; }
        public string check { get; set; }
        public int nivel { get; set; }
    }
}
