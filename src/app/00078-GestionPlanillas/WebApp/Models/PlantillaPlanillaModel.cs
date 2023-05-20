using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PlantillaPlanillaModel
    {
        public int? plantillaPlanillaID { get; set; }

        [DisplayName("Cat.Concepto")]
        [Required(ErrorMessage = "La Categoría es obligatoria.")]
        public int categoriaPlanillaID { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public string plantillaPlanillaDesc { get; set; }

        public bool estaHabilitado { get; set; }

        public string categoriaPlanillaDesc { get; set; }

        public int clasePlanillaID { get; set; }

        public string clasePlanillaDesc { get; set; }
    }
}