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
    public class USP_I_RegistrarTrabajadorCategoriaPlanilla
    {
        public int I_TrabajadorID { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public int I_DependenciaID { get; set; }

        public int? I_GrupoTrabajoID { get; set; }

        public bool B_EsJefe {  get; set; }

        public int I_UserID { get; set; }

        public Result Execute()
        {
            Result result;

            DynamicParameters parameters;

            try
            {
                string s_command = "USP_I_RegistrarTrabajadorCategoriaPlanilla";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    parameters = new DynamicParameters();
                    parameters.Add(name: "I_TrabajadorID", dbType: DbType.Int32, value: I_TrabajadorID);
                    parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);
                    parameters.Add(name: "I_DependenciaID", dbType: DbType.Int32, value: I_DependenciaID);
                    parameters.Add(name: "I_GrupoTrabajoID", dbType: DbType.Int32, value: I_GrupoTrabajoID);
                    parameters.Add(name: "B_EsJefe", dbType: DbType.Boolean, value: B_EsJefe);
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
