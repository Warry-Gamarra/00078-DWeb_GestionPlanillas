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
    public class USP_I_GrabarDocente
    {
        public string C_TrabajadorCod { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public string T_Nombre { get; set; }

        public int I_TipoDocumentoID { get; set; }

        public string C_NumDocumento { get; set; }

        public int I_RegimenID { get; set; }

        public int I_EstadoID { get; set; }

        public int I_VinculoID { get; set; }

        public int I_UserID { get; set; }

        public bool B_Result { get; set; }

        public string T_Message { get; set; }

        public Result Execute()
        {
            Result result;

            DynamicParameters parameters;

            try
            {
                string s_command = "USP_I_GrabarDocente";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    parameters = new DynamicParameters();
                    parameters.Add(name: "C_TrabajadorCod", dbType: DbType.String, value: C_TrabajadorCod);
                    parameters.Add(name: "T_ApellidoPaterno", dbType: DbType.String, value: T_ApellidoPaterno);
                    parameters.Add(name: "T_ApellidoMaterno", dbType: DbType.String, value: T_ApellidoMaterno);
                    parameters.Add(name: "I_TipoDocumentoID", dbType: DbType.String, value: I_TipoDocumentoID);
                    parameters.Add(name: "T_ApellidoMaterno", dbType: DbType.Int32, value: T_ApellidoMaterno);
                    parameters.Add(name: "C_NumDocumento", dbType: DbType.String, value: C_NumDocumento);
                    parameters.Add(name: "I_RegimenID", dbType: DbType.String, value: I_RegimenID);
                    parameters.Add(name: "I_EstadoID", dbType: DbType.String, value: I_EstadoID);
                    parameters.Add(name: "I_VinculoID", dbType: DbType.String, value: I_VinculoID);
                    parameters.Add(name: "I_UserID", dbType: DbType.Int32, value: I_UserID);
                    parameters.Add(name: "B_Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add(name: "T_Message", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                    _dbConnection.Execute(s_command, parameters, commandType: CommandType.StoredProcedure);

                    result = new Result()
                    {
                        Success = parameters.Get<bool>("B_Result"),
                        Message = parameters.Get<string>("T_Message")
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    Message = ex.Message,
                };
            }

            return result;
        }
    }
}
