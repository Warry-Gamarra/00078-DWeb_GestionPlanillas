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
    public class TC_Banco
    {
        public int I_BancoID { get; set; }

        public string T_BancoDesc { get; set; }

        public string T_BancoAbrv { get; set; }

        public bool B_Habilitado { get; set; }

        public bool B_Eliminado { get; set; }

        public static IEnumerable<TC_Banco> FindAll()
        {
            IEnumerable<TC_Banco> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Banco WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_Banco>(s_command, commandType: System.Data.CommandType.Text);
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
