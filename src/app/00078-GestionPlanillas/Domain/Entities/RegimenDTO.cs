using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RegimenDTO
    {
        public int regimenID { get; set; }

        public string regimenDesc { get; set; }

        public string regimenCod { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
