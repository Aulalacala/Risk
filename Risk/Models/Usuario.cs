using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Risk.Models
{
    public class Usuario
    {

        public string _usuario { get; set; }

        [DataType(DataType.Password)]
        public string _clave { get; set; }

    }
}