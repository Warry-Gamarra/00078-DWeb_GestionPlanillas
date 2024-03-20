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
    public class TC_Sexo
    {
        public int I_SexoID { get; set; }

        public string T_SexoCod { get; set; }

        public string T_SexoDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_Sexo> FindAll()
        {
            IEnumerable<TC_Sexo> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Sexo WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_Sexo>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_Sexo>();
            }

            return result;
        }
    }
}
