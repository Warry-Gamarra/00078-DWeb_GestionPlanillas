using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ResumenSIAFDTO
    {
        public ResumenSIAFDTO(IEnumerable<string> cabecera, IEnumerable<IDictionary<string, object>> detalle)
        {
            this.cabecera = cabecera;

            this.detalle = detalle;
        }

        public IEnumerable<string> cabecera { get; }

        public IEnumerable<IDictionary<string, object>> detalle { get; }
    }
}
