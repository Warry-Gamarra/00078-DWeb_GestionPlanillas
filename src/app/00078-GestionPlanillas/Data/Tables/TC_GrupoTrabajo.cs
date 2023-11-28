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
    }
}
