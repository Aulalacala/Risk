using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risk.Models {
    public class GeneralModel {
        public string structureCode { get; set; }
        public string particleCode { get; set; }
        public string identifier { get; set; }
        public string name { get; set; }
        public string example { get; set; }
        public string cause { get; set; }
        public string justification { get; set; }
        public string categoria { get; set; }
        public string segmento { get; set; }
        public string level1 { get; set; }
        public string level2 { get; set; }
        public string level3 { get; set; }
        public string owner { get; set; }      
    }
}