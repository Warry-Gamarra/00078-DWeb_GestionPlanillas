using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PersonaDTO
    {
        public int I_PersonaID { get; set; }

        public int I_TipoDocumentoID { get; set; }

        public string C_NumDocumento { get; set; }

        public string T_Nombre { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public DateTime D_FecNac { get; set; }

        public string C_Cui { get; set; }

        public bool B_Habilitado { get; set; }
    }
}
