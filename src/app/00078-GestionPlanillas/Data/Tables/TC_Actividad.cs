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
    public class TC_Actividad
    {
        public int I_ActividadID { get; set; }

        public string T_ActividadDesc { get; set; }

        public string C_ActividadCod { get; set; }

        public static IEnumerable<TC_Actividad> FindAll()
        {
            IEnumerable<TC_Actividad> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Actividad WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_Actividad>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_Actividad>();
            }

            return result;
        }

        public static TC_Actividad FindByID(int I_ActividadID)
        {
            TC_Actividad result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Actividad WHERE B_Eliminado = 0 AND I_ActividadID = @I_ActividadID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<TC_Actividad>(s_command, new { I_ActividadID = I_ActividadID }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public static TC_Actividad FindByCod(string C_ActividadCod)
        {
            TC_Actividad result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Actividad WHERE B_Eliminado = 0 AND C_ActividadCod = @C_ActividadCod;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<TC_Actividad>(s_command, new { C_ActividadCod = C_ActividadCod }, commandType: System.Data.CommandType.Text);
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
