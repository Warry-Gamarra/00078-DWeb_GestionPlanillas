using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TrabajadorEntity
    {
        public int? I_TrabajadorID { get; set; }

        public string C_TrabajadorCod { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public string T_Nombre { get; set; }

        public int I_TipoDocumentoID { get; set; }

        public string C_NumDocumento { get; set; }

        public DateTime? D_FechaIngreso { get; set; }

        public int I_RegimenID { get; set; }

        public int I_EstadoID { get; set; }

        public int I_VinculoID { get; set; }

        public int? I_BancoID { get; set; }

        public string T_NroCuentaBancaria { get; set; }

        public int? I_DependenciaID { get; set; }

        public int? I_Afp { get; set; }

        public string T_Cuspp { get; set; }
    }
}
