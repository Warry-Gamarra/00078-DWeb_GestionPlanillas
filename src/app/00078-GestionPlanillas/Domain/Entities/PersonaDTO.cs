using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PersonaDTO
    {
        public int personaID { get; set; }

        public int tipoDocumentoID { get; set; }

        public string numDocumento { get; set; }

        public string nombre { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public DateTime fecNac { get; set; }

        public string cui { get; set; }
    }
}
