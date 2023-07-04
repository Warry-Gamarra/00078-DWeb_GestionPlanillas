using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DocenteDTO
    {
        public int I_TrabajadorID { get; set; }

        public string C_TrabajadorCod { get; set; }

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

        public int I_DocenteID { get; set; }

        public int I_CategoriaDocenteID { get; set; }

        public string C_CategoriaDocenteCod { get; set; }

        public string T_CategoriaDocenteDesc { get; set; }

        public int I_HorasDocenteID { get; set; }

        public int I_Horas { get; set; }

        public int I_DedicacionDocenteID { get; set; }

        public string C_DedicacionDocenteCod { get; set; }

        public string T_DedicacionDocenteDesc { get; set; }

        public bool estaHabilitado { get;set; }
    }
}
