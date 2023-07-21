using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ConceptoReferenciaModel
    {
        public int id { get; set; }

        public int plantillaPlanillaConceptoID { get; set; }

        public int conceptoReferenciaID { get; set; }

        public string conceptoCod { get; set; }

        public string conceptoDesc { get; set; }

        public string conceptoAbrv { get; set; }
    }
}