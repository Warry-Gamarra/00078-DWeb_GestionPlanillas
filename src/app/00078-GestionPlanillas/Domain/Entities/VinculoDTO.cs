using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VinculoDTO
    {
        public int vinculoID { get; set; }

        public string vinculoDesc { get; set; }

        public string vinculoCod { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
