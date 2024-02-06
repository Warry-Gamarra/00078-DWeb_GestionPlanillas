using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ResumenSIAFDTO
    {
        public ResumenSIAFDTO(string titulo, IEnumerable<string> cabecera, IEnumerable<IDictionary<string, object>> detalle, string pieTabla)
        {
            this.titulo = titulo;

            this.cabecera = cabecera;

            this.detalle = detalle;

            this.pieTabla = pieTabla;
        }

        public IEnumerable<string> cabecera { get; }

        public IEnumerable<IDictionary<string, object>> detalle { get; }

        public string titulo { get; }

        public string pieTabla { get; }
    }
}
