using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AfpDTO
    {
        public int afpID { get; set; }

        public string afpDesc { get; set; }

        public string afpCod { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
