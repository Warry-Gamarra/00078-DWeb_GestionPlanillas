using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoCuentaBancariaDTO
    {
        public int tipoCuentaBancariaID { get; set; }

        public string tipoCuentaBancariaCod { get; set; }

        public string tipoCuentaBancariaDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
