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
    public class USP_I_RegistrarPlantillaPlanilla
    {
        public int I_CategoriaPlanillaID { get; set; }

        public string T_PlantillaPlanillaDesc { get; set;  }

        public int I_UserID { get; set; }

        public Result Execute()
        {
            Result result;

            DynamicParameters parameters;

            try
            {
                string s_command = "USP_I_RegistrarPlantillaPlanilla";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    parameters = new DynamicParameters();
                    parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);
                    parameters.Add(name: "T_PlantillaPlanillaDesc", dbType: DbType.String, value: T_PlantillaPlanillaDesc);
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
