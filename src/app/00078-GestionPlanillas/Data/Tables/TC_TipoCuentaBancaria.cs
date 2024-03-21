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
    public class TC_TipoCuentaBancaria
    {
        public int I_TipoCuentaBancariaID { get; set; }

        public string T_TipoCuentaBancariaCod { get; set; }

        public string T_TipoCuentaBancariaDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_TipoCuentaBancaria> FindAll()
        {
            IEnumerable<TC_TipoCuentaBancaria> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_TipoCuentaBancaria WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_TipoCuentaBancaria>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_TipoCuentaBancaria>();
            }

            return result;
        }
    }
}
