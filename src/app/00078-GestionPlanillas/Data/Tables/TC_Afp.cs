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
    public class TC_Afp
    {
        public int I_AfpID { get; set; }

        public string T_AfpDesc { get; set; }

        public string C_AfpCod { get; set; }

        public bool B_Habilitado { get; set; }

        public bool B_Eliminado { get; set; }

        public static IEnumerable<TC_Afp> FindAll()
        {
            IEnumerable<TC_Afp> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Afp WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_Afp>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
