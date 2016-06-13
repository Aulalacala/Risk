using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Models
{
    //Objeto o Modelo necesario para pintar la partialView description de la vista de estructura
    public class DescriptionStructureModel
    {
        public string department { get; set; }
        public string responsible { get; set; }
        public string activities { get; set; }
        public string ITassets { get; set; }
        public string controls { get; set; }
        public string inputs { get; set; }
        public string outputs { get; set; }
    }
}