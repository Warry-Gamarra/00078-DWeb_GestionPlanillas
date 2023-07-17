using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NivelRemunerativoDTO
    {
        public int nivelRemunerativoID { get; set; }

        public string nivelRemunerativoCod { get; set; }

        public string nivelRemunerativoDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
