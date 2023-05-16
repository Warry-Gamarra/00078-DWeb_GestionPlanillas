using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ConceptoModel
    {
        public int? conceptoID { get; set; }

        [DisplayName("Tip.Concepto")]
        [Required(ErrorMessage = "El Tipo de Concepto es obligatorio.")]
        public int tipoConceptoID { get; set; }

        public string tipoConceptoDesc { get; set; }

        [DisplayName("Cod.Concepto")]
        [Required(ErrorMessage = "El Código del Concepto es obligatorio.")]
        public string conceptoCod { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La {0} es obligatorio.")]
        public string conceptoDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}