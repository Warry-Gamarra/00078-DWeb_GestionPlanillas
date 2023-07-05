using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ConceptoReferenciaDTO
    {
        public int id { get; set; }

        public int plantillaPlanillaConceptoID { get; set; }

        public int plantillaPlanillaConceptoReferenciaID { get; set; }

        public int conceptoID { get; set; }

        public string conceptoCod { get; set; }

        public string conceptoDesc { get; set; }

        public string conceptoAbrv { get; set; }
    }
}
