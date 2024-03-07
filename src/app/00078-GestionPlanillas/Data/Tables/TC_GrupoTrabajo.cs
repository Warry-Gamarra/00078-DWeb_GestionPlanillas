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
    public class TC_GrupoTrabajo
    {
        public int I_GrupoTrabajoID { get; set; }

        public string T_GrupoTrabajoDesc { get; set; }

        public string C_GrupoTrabajoCod { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_GrupoTrabajo> FindAll()
        {
            IEnumerable<TC_GrupoTrabajo> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_GrupoTrabajo WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_GrupoTrabajo>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_GrupoTrabajo>();
            }

            return result;
        }

        public static TC_GrupoTrabajo FindByCod(string C_GrupoTrabajoCod)
        {
            TC_GrupoTrabajo result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_GrupoTrabajo WHERE B_Eliminado = 0 AND C_GrupoTrabajoCod = @C_GrupoTrabajoCod;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<TC_GrupoTrabajo>(s_command, new { C_GrupoTrabajoCod = C_GrupoTrabajoCod }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public static TC_GrupoTrabajo FindByID(int I_GrupoTrabajoID)
        {
            TC_GrupoTrabajo result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_GrupoTrabajo WHERE B_Eliminado = 0 AND I_GrupoTrabajoID = @I_GrupoTrabajoID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<TC_GrupoTrabajo>(s_command, new { I_GrupoTrabajoID = I_GrupoTrabajoID }, commandType: System.Data.CommandType.Text);
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
