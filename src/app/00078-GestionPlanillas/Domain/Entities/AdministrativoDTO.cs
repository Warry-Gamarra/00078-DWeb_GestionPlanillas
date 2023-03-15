using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AdministrativoDTO
    {
        public int I_TrabajadorID { get; set; }

        public string T_Nombre { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public int I_TipoDocumentoID { get; set; }

        public string T_TipoDocumentoDesc { get; set; }

        public string C_NumDocumento { get; set; }

        public DateTime? D_FechaIngreso { get; set; }

        public int? I_RegimenID { get; set; }

        public string T_RegimenDesc { get; set; }

        public int? I_EstadoID { get; set; }

        public string T_EstadoDesc { get; set; }

        public int? I_VinculoID { get; set; }

        public string T_VinculoDesc { get; set; }

        public int I_AdministrativoID { get; set; }

        public int I_GrupoOcupacionalID { get; set; }

        public string C_GrupoOcupacionalCod { get; set; }

        public string T_GrupoOcupacionalDesc { get; set; }

        public int I_NivelRemunerativoID { get; set; }

        public string C_NivelRemunerativoCod { get; set; }

        public string T_NivelRemunerativoDesc { get; set; }
    }
}
