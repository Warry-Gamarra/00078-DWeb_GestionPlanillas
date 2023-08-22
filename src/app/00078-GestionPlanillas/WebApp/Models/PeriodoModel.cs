using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PeriodoModel
    {
        public int? periodoID { get; set; }

        public int anio { get; set; }

        public int mes { get; set; }

        public string mesDesc { get; set; }
    }
}