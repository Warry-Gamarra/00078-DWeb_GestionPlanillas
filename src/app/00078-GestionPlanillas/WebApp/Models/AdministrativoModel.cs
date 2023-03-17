using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class AdministrativoModel
    {
        public int I_TrabajadorID { get; set; }

        public string C_TrabajadorCod { get; set; }

        public string T_Nombre { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public string T_ApellidosNombre
        {
            get { return String.Format("{0} {1}, {2}", T_ApellidoPaterno, T_ApellidoMaterno, T_Nombre); }
        }

        public int I_TipoDocumentoID { get; set; }

        public string T_TipoDocumentoDesc { get; set; }

        public string C_NumDocumento { get; set; }

        public DateTime? D_FechaIngreso { get; set; }

        public string T_FechaIngreso
        {
            get
            {
                return D_FechaIngreso.HasValue ? D_FechaIngreso.Value.ToString("dd/MM/yyyy") : "";
            }

        }

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
