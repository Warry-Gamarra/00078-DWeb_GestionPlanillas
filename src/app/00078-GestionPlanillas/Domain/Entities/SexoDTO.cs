using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SexoDTO
    {
        public int sexoID { get; set; }

        public string sexoCod { get; set; }

        public string sexoDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
