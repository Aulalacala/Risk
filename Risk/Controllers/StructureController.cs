using Risk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Risk.Controllers
{
    public class StructureController : Controller
    {

        public static Riesgos_BDDataContext riesgosBD = new Riesgos_BDDataContext();

        // GET: Structure
        public ActionResult Index()
        {
            var locations = GetLocations(0);
            return View(locations);
        }

        public static List<TreeViewLocation> GetLocations(int id)
        {
            var locations = new List<TreeViewLocation>();

            List<tEstructura> cuantosHay = riesgosBD.tEstructura.Where(r => r.idPadre == id).OrderBy(r => r.Orden).ToList();
            if (cuantosHay.Count != 0)
            {
                for (int i = 0; i < cuantosHay.Count; i++)
                {
                    if (tienesHijos(Convert.ToInt32(cuantosHay[i].idPadre)))
                    {
                        locations.Add(new TreeViewLocation
                        {
                            Id = cuantosHay[i].IdEstructura,
                            Name = cuantosHay[i].Nombre.ToString(),
                            ChildLocations = GetLocations(Convert.ToInt32(cuantosHay[i].IdEstructura))
                        });
                        }
                    else
                    {
                        locations.Add(new TreeViewLocation
                        {
                            Id = cuantosHay[i].IdEstructura,
                            Name = cuantosHay[i].Nombre.ToString(),
                        });
                    }
                    //GetLocations(cuantosHay[i].IdEstructura);
                }
            }
            
            return locations;
        }

        //PARA BUSCAR LOS HIJOS HAY QUE METER EL ID DE SU SANTO PADRE
        public static bool tienesHijos(int idPadre)
        {
            bool tieneHijos = false;
            return tieneHijos = riesgosBD.tEstructura.Where(r => r.idPadre == idPadre).Any() ? tieneHijos = true : tieneHijos = false;
        }

        //public static List<TreeViewLocation> GetLocations()
        //{
        //    var locations = new List<TreeViewLocation>
        //                        {
        //                            new TreeViewLocation
        //                                {
        //                                Id=1,
        //                                Name = "United States",
        //                                    ChildLocations =
        //                                        {
        //                                            new TreeViewLocation
        //                                                {
        //                                                    Name = "Chicago",
        //                                                    ChildLocations =
        //                                                        {
        //                                                            new TreeViewLocation {Name = "Rack 1"},
        //                                                            new TreeViewLocation {Name = "Rack 2"},
        //                                                            new TreeViewLocation {Name = "Rack 3"},
        //                                                        }
        //                                                },
        //                                            new TreeViewLocation {Name = "Dallas"}
        //                                        }
        //                                },
        //                            new TreeViewLocation
        //                                {
        //                                    Name = "Canada",
        //                                    ChildLocations =
        //                                        {
        //                                            new TreeViewLocation {Name = "Ontario"},
        //                                            new TreeViewLocation {Name = "Windsor"}
        //                                        }
        //                                }
        //                        };
        //    return locations;
        //}
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
    }
}