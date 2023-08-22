using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PeriodoModel
    {
        public int? periodoID { get; set; }

        [DisplayName("Año")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public int anio { get; set; }

        [DisplayName("Mes")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public int mes { get; set; }

        public string mesDesc { get; set; }
    }
}