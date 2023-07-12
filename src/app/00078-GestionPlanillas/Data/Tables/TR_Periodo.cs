using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Tables
{
    public class TR_Periodo
    {
        public int I_PeriodoID { get; set; }

        public int I_Anio { get; set; }

        public int I_Mes { get; set; }

        public string T_MesDesc { get; set; }

        public static IEnumerable<TR_Periodo> GetYears()
        {
            IEnumerable<TR_Periodo> result;

            try
            {
                string s_command = "SELECT DISTINCT I_Anio FROM dbo.TR_Periodo;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TR_Periodo>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TR_Periodo>();
            }

            return result;
        }

        public static IEnumerable<TR_Periodo> FindMonthsByYear(int I_Anio)
        {
            IEnumerable<TR_Periodo> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TR_Periodo WHERE I_Anio = @I_Anio;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TR_Periodo>(s_command, new { I_Anio = I_Anio }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TR_Periodo>();
            }

            return result;
        }

        public static TR_Periodo GetByYearAndMonth(int I_Anio, int I_Mes)
        {
            TR_Periodo result;

            try
            {
                string s_command = "SELECT * FROM dbo.TR_Periodo WHERE I_Anio = @I_Anio AND I_Mes = @I_Mes;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<TR_Periodo>(s_command, 
                        new { I_Anio = I_Anio, I_Mes = I_Mes }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}
