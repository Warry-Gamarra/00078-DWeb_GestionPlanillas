using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Procedures
{
    public class USP_S_ListarResumenPlanillaTrabajador
    {
        public int I_TrabajadorID { get; }

        public string C_TrabajadorCod { get; }

        public string T_Nombre { get; }

        public string T_ApellidoPaterno { get; }

        public string T_ApellidoMaterno { get; }

        public int I_TipoDocumentoID { get; }

        public string T_TipoDocumentoDesc { get; }

        public string C_NumDocumento { get; }

        public int? I_RegimenID { get; }

        public string T_RegimenDesc { get; }

        public int? I_EstadoID { get; }

        public string T_EstadoDesc { get; }

        public int? I_VinculoID { get; }

        public string T_VinculoDesc { get; }

        public int I_TrabajadorPlanillaID { get; }

        public decimal I_TotalRemuneracion { get; }

        public decimal I_TotalReintegro { get; }

        public decimal I_TotalDeduccion { get; }

        public decimal I_TotalBruto { get; }

        public decimal I_TotalDescuento { get; }

        public decimal I_TotalSueldo { get; }

        public int I_PlanillaID { get; }

        public int I_PeriodoID { get; }

        public int I_Anio { get; }

        public int I_Mes { get; }

        public string T_MesDesc { get; }

        public int I_CategoriaPlanillaID { get; }

        public string T_CategoriaPlanillaDesc { get; }

        public static IEnumerable<USP_S_ListarResumenPlanillaTrabajador> Execute(int I_Anio, int I_Mes, int I_CategoriaPlanillaID)
        {
            IEnumerable<USP_S_ListarResumenPlanillaTrabajador> result;
            DynamicParameters parameters;
            string command;

            try
            {
                command = "USP_S_ListarResumenPlanillaTrabajador";

                parameters = new DynamicParameters();

                parameters.Add(name: "I_Anio", dbType: DbType.Int32, value: I_Anio);

                parameters.Add(name: "I_Mes", dbType: DbType.Int32, value: I_Mes);

                parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<USP_S_ListarResumenPlanillaTrabajador>(command, parameters, commandType: CommandType.StoredProcedure);
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
