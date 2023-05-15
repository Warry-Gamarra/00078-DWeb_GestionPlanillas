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
        public int I_ConceptoID { get; set; }

        [DisplayName("Tip.Concepto")]
        [Required(ErrorMessage = "El Tipo de Concepto es obligatorio.")]
        public int I_TipoConceptoID { get; set; }

        public string T_TipoConceptoDesc { get; set; }

        [DisplayName("Cod.Concepto")]
        [Required(ErrorMessage = "El Código del Concepto es obligatorio.")]
        public string C_ConceptoCod { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La {0} es obligatorio.")]
        public string T_ConceptoDesc { get; set; }

        public bool B_Habilitado { get; set; }
    }
}