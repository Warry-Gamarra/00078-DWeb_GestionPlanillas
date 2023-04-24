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
    public class TC_GrupoOcupacional
    {
        public int I_GrupoOcupacionalID { get; set; }

        public string T_GrupoOcupacionalCod { get; set; }

        public string T_GrupoOcupacionalDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_GrupoOcupacional> FindAll()
        {
            IEnumerable<TC_GrupoOcupacional> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_GrupoOcupacional WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_GrupoOcupacional>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_GrupoOcupacional>();
            }

            return result;
        }
    }
}
