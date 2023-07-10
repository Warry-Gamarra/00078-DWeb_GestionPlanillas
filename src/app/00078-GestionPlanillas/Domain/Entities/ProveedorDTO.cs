using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProveedorDTO
    {
        public int proveedorID { get; set; }

        public string proveedorDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
