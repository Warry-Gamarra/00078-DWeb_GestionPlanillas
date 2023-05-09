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
    public class USP_I_GenerarPlanilla_Docente_Administrativo
    {
        public DataTable Tbl_Trabajador { get; set; }

        public int I_Anio { get; set; }
        
        public int I_Mes { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public int I_UserID { get; set; }

        public Result Execute()
        {
            Result result;

            DynamicParameters parameters;

            try
            {
                string s_command = "USP_I_GenerarPlanilla_Docente_Administrativo";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    parameters = new DynamicParameters();
                    parameters.Add(name: "Tbl_Trabajador", value: Tbl_Trabajador.AsTableValuedParameter("dbo.type_dataTrabajador"));
                    parameters.Add(name: "I_Anio", dbType: DbType.Int32, value: this.I_Anio);
                    parameters.Add(name: "I_Mes", dbType: DbType.Int32, value: this.I_Mes);
                    parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: this.I_CategoriaPlanillaID);
                    parameters.Add(name: "I_UserID", dbType: DbType.Int32, value: this.I_UserID);
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
