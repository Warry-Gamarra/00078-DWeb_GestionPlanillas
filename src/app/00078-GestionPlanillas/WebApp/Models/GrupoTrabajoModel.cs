using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class GrupoTrabajoModel
    {
        public int? grupoTrabajoID { get; set; }
        
        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public string grupoTrabajoDesc { get; set; }

        [DisplayName("Cod.Grupo Trabajo")]
        [Required(ErrorMessage = "El Código es obligatorio.")]
        public string grupoTrabajoCod { get; set; }

        public bool estaHabilitado { get; set; }
    }
}