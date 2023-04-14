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
    public class TC_Regimen
    {
        public int I_RegimenID { get; set; }

        public string T_RegimenDesc { get; set; }

        public string C_RegimenCod { get; set; }

        public bool B_Habilitado { get; set; }

        public bool B_Eliminado { get; set; }

        public static IEnumerable<TC_Regimen> FindAll()
        {
            IEnumerable<TC_Regimen> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Regimen;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_Regimen>(s_command, commandType: System.Data.CommandType.Text);
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
