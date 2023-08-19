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
    public class USP_U_ActualizarDatosUsuario
    {
        public int UserId { get; set; }

        public int I_DatosUsuarioID { get; set; }

        public string N_NumDoc { get; set; }

        public string T_NomPersona { get; set; }

        public string T_CorreoUsuario { get; set; }

        public int? I_DependenciaID { get; set; }

        public int CurrentUserId { get; set; }

        public DateTime D_FecMod { get; set; }

        public Result Execute()
        {
            Result result;

            DynamicParameters parameters;

            try
            {
                string s_command = "USP_U_ActualizarDatosUsuario";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    parameters = new DynamicParameters();
                    parameters.Add(name: "UserId", dbType: DbType.Int32, value: UserId);
                    parameters.Add(name: "I_DatosUsuarioID", dbType: DbType.Int32, value: I_DatosUsuarioID);
                    parameters.Add(name: "N_NumDoc", dbType: DbType.String, value: N_NumDoc);
                    parameters.Add(name: "T_NomPersona", dbType: DbType.String, value: T_NomPersona);
                    parameters.Add(name: "T_CorreoUsuario", dbType: DbType.String, value: T_CorreoUsuario);
                    parameters.Add(name: "I_DependenciaID", dbType: DbType.Int32, value: I_DependenciaID);
                    parameters.Add(name: "CurrentUserId", dbType: DbType.Int32, value: CurrentUserId);
                    parameters.Add(name: "D_FecMod", dbType: DbType.DateTime, value: D_FecMod);
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
