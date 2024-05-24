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
    public class USP_S_ListarTrabajadoresConPlanilla
    {
        public int I_TrabajadorID { get; set; }

        public string C_TrabajadorCod { get; set; }

        public string T_Nombre { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public string T_TipoDocumentoDesc { get; set; }

        public string C_NumDocumento { get; set; }

        public string T_EstadoDesc { get; set; }

        public string T_VinculoDesc { get; set; }

        public int I_Anio { get; set; }

        public int I_Mes { get; set; }

        public static IEnumerable<USP_S_ListarTrabajadoresConPlanilla> Execute(int I_Anio, int I_Mes, int I_CategoriaPlanillaID)
        {
            IEnumerable<USP_S_ListarTrabajadoresConPlanilla> result;
            DynamicParameters parameters;
            string command;

            try
            {
                command = "USP_S_ListarTrabajadoresConPlanilla";

                parameters = new DynamicParameters();

                parameters.Add(name: "I_Anio", dbType: DbType.Int32, value: I_Anio);

                parameters.Add(name: "I_Mes", dbType: DbType.Int32, value: I_Mes);

                parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<USP_S_ListarTrabajadoresConPlanilla>(command, parameters, commandType: CommandType.StoredProcedure);
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
