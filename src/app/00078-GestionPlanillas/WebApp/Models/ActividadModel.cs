using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ActividadModel
    {
        public int? actividadID { get; set; }

        [DisplayName("Cod.Actividad")]
        [Required(ErrorMessage = "El Código de la Actividad es obligatorio.")]
        public string actividadCod { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public string actividadDesc { get; set; }
    }
}