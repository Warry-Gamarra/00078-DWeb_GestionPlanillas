using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class MetaModel
    {
        public int? metaID { get; set; }

        [DisplayName("Cod.Meta")]
        [Required(ErrorMessage = "El Código de la Meta es obligatoria.")]
        public string metaCod { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public string metaDesc { get; set; }
    }
}