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
    public class USP_I_RegistrarDatosUsuario
    {
        public int UserId { get; set; }

        public string N_NumDoc { get; set; }

        public string T_NomPersona { get; set; }

        public string T_CorreoUsuario { get; set; }

        public int CurrentUserId { get; set; }

        public DateTime D_FecRegistro { get; set; }

        public Result Execute()
        {
            Result result;

            DynamicParameters parameters;

            try
            {
                string s_command = "USP_I_RegistrarDatosUsuario";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    parameters = new DynamicParameters();
                    parameters.Add(name: "UserId", dbType: DbType.Int32, value: UserId);
                    parameters.Add(name: "N_NumDoc", dbType: DbType.String, value: N_NumDoc);
                    parameters.Add(name: "T_NomPersona", dbType: DbType.String, value: T_NomPersona);
                    parameters.Add(name: "T_CorreoUsuario", dbType: DbType.String, value: T_CorreoUsuario);
                    parameters.Add(name: "CurrentUserId", dbType: DbType.Int32, value: CurrentUserId);
                    parameters.Add(name: "D_FecRegistro", dbType: DbType.DateTime, value: D_FecRegistro);
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
