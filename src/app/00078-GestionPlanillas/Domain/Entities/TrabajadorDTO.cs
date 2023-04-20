using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TrabajadorDTO
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

        public int? I_AfpID { get; set; }

        public string T_AfpDesc { get; set; }

        public string T_Cuspp { get; set; }

        public int I_EstadoID { get; set; }

        public string T_EstadoDesc { get; set; }

        public int I_VinculoID { get; set; }

        public string T_VinculoDesc { get; set; }

        public int? I_TrabajadorDependenciaID { get; set; }

        public int? I_DependenciaID { get; set; }

        public string C_DependenciaCod { get; set; }

        public string T_DependenciaDesc { get; set; }

        public int? I_CuentaBancariaID { get; set; }

        public string T_NroCuentaBancaria { get; set; }

        public int? I_BancoID { get; set; }

        public string T_BancoDesc { get; set; }

        public string T_BancoAbrv { get; set; }
    }
}
