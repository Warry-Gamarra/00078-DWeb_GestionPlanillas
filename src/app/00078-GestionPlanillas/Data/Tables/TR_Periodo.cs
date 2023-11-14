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

        public static IEnumerable<TR_Periodo> FindAll()
        {
            IEnumerable<TR_Periodo> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TR_Periodo ORDER BY I_Anio DESC, I_Mes ASC;";

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

        public static TR_Periodo FindByID(int I_PeriodoID)
        {
            TR_Periodo result;

            try
            {
                string s_command = "SELECT * FROM dbo.TR_Periodo WHERE I_PeriodoID = @I_PeriodoID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<TR_Periodo>(s_command, 
                        new { I_PeriodoID  = I_PeriodoID }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static IEnumerable<TR_Periodo> GetYears(bool soloAñoConMeses)
        {
            IEnumerable<TR_Periodo> result;
            string s_command;
            try
            {
                if (soloAñoConMeses)
                {
                    s_command = "SELECT a.I_Anio FROM dbo.TC_Anio a WHERE EXISTS(SELECT p.I_Anio FROM dbo.TR_Periodo p WHERE P.I_Anio = a.I_Anio) ORDER BY a.I_Anio DESC;";
                }
                else
                {
                    s_command = "SELECT I_Anio FROM dbo.TC_Anio ORDER BY I_Anio DESC;";
                }

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
                string s_command = "SELECT * FROM dbo.TR_Periodo WHERE I_Anio = @I_Anio ORDER BY I_Mes ASC;";

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
