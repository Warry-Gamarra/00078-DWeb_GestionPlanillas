using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DedicacionDocenteDTO
    {
        public int dedicacionDocenteID { get; set; }

        public string dedicacionDocenteDesc { get; set; }

        public string dedicacionDocenteCod { get; set; }

        public bool esParaDocenteOrdinario { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
