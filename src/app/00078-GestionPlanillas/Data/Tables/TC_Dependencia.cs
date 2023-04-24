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
    public class TC_Dependencia
    {
        public int I_DependenciaID { get; set; }

        public string T_DependenciaDesc { get; set; }

        public string C_DependenciaCod { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_Dependencia> FindAll()
        {
            IEnumerable<TC_Dependencia> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Dependencia WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_Dependencia>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_Dependencia>();
            }

            return result;
        }
    }
}
