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
    public class TC_NivelRemunerativo
    {
        public int I_NivelRemunerativoID { get; set; }

        public string C_NivelRemunerativoCod { get; set; }

        public string T_NivelRemunerativoDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_NivelRemunerativo> FindAll()
        {
            IEnumerable<TC_NivelRemunerativo> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_NivelRemunerativo WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_NivelRemunerativo>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_NivelRemunerativo>();
            }

            return result;
        }
    }
}
