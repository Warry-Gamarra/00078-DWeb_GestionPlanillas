using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Views
{
    public class VW_ResumenPlanillaTrabajador
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

        public int I_TrabajadorPlanillaID { get; set; }

        public decimal I_TotalRemuneracion { get; set; }

        public decimal I_TotalDescuento { get; set; }

        public decimal I_TotalReintegro { get; set; }

        public decimal I_TotalDeduccion { get; set; }

        public decimal I_TotalSueldo { get; set; }

        public int I_PlanillaID { get; set; }

        public int I_PeriodoID { get; set; }

        public int I_Anio { get; set; }

        public string T_MesDesc { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }

        public static IEnumerable<VW_ResumenPlanillaTrabajador> FindAll()
        {
            IEnumerable<VW_ResumenPlanillaTrabajador> result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_ResumenPlanillaTrabajador;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_ResumenPlanillaTrabajador>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
