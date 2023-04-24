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
    public class TC_Vinculo
    {
        public int I_VinculoID { get; set; }

        public string T_VinculoDesc { get; set; }

        public string C_VinculoCod { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_Vinculo> FindAll()
        {
            IEnumerable<TC_Vinculo> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Vinculo WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_Vinculo>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_Vinculo>();
            }

            return result;
        }
    }
}
