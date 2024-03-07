﻿using Dapper;
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
    public class USP_U_ActualizarGrupoTrabajo
    {
        public int I_GrupoTrabajoID { get; set; }

        public string C_GrupoTrabajoCod { get; set; }

        public string T_GrupoTrabajoDesc { get; set; }

        public int I_UserID { get; set; }

        public Result Execute()
        {
            Result result;

            DynamicParameters parameters;

            try
            {
                string s_command = "USP_U_ActualizarGrupoTrabajo";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    parameters = new DynamicParameters();
                    parameters.Add(name: "I_GrupoTrabajoID", dbType: DbType.Int32, value: I_GrupoTrabajoID);
                    parameters.Add(name: "C_GrupoTrabajoCod", dbType: DbType.String, value: C_GrupoTrabajoCod);
                    parameters.Add(name: "T_GrupoTrabajoDesc", dbType: DbType.String, value: T_GrupoTrabajoDesc);
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