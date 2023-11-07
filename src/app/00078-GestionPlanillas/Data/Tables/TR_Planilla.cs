using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Tables
{
    public class TR_Planilla
    {
        public static bool ExistePlanilla(int I_Anio, int I_Mes)
        {
            bool isThereAnyPayroll;
            int cantRegistros;

            try
            {
                string s_command = @"SELECT pla.I_PlanillaID FROM dbo.TR_Planilla pla INNER JOIN TR_Periodo per ON per.I_PeriodoID = pla.I_PeriodoID 
                    WHERE pla.B_Anulado = 0 AND per.I_Anio = @I_Anio AND per.I_Mes = @I_Mes;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    cantRegistros = _dbConnection.Query<int>(s_command, new { I_Anio = I_Anio, I_Mes = I_Mes }, commandType: CommandType.Text).Count();

                    isThereAnyPayroll = cantRegistros > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isThereAnyPayroll;
        }
    }
}
