using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class DependenciaModel
    {
        public int? dependenciaID { get; set; }

        [DisplayName("Cod.Dependencia")]
        [Required(ErrorMessage = "El Código de la Dependencia es obligatoria.")]
        public string dependenciaCod { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public string dependenciaDesc { get; set; }
        
        public bool estaHabilitado { get; set; }
    }
}