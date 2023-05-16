using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoConceptoDTO
    {
        public int tipoConceptoID { get; set; }

        public string tipoConceptoDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
