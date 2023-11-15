using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Procedures
{
    public class USP_S_ListarResumenPorActividadYDependencia
    {
        public string C_ActividadCod { get; }

        public string C_DependenciaCod { get; }

        public string T_DependenciaDesc { get; }

        public decimal I_TotalRemuneracion { get; }

        public decimal I_TotalReintegro { get; }

        public decimal I_TotalDeduccion { get; }

        public decimal I_TotalBruto { get; }

        public decimal I_TotalDescuento { get; }

        public decimal I_TotalSueldo { get; }

        public static IEnumerable<USP_S_ListarResumenPorActividadYDependencia> Execute(int I_Anio, int I_Mes, int I_CategoriaPlanillaID)
        {
            IEnumerable<USP_S_ListarResumenPorActividadYDependencia> result;
            DynamicParameters parameters;
            string command;

            try
            {
                command = "USP_S_ListarResumenPorActividadYDependencia";

                parameters = new DynamicParameters();

                parameters.Add(name: "I_Anio", dbType: DbType.Int32, value: I_Anio);

                parameters.Add(name: "I_Mes", dbType: DbType.Int32, value: I_Mes);

                parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<USP_S_ListarResumenPorActividadYDependencia>(command, parameters, commandType: CommandType.StoredProcedure);
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
