using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EstadoDTO
    {
        public int estadoID { get; set; }

        public string estadoDesc { get; set; }

        public string estadoCod { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
