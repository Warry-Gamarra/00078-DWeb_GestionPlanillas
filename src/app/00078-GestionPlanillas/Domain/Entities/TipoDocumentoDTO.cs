using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoDocumentoDTO
    {
        public int tipoDocumentoID { get; set; }

        public string tipoDocumentoDesc { get; set; }

        public bool estaHabilitado { get; set; }
    }
}
