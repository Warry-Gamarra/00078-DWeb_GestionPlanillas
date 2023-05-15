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
    public class TC_TipoConcepto
    {
        public int I_TipoConceptoID { get; set; }

        public string T_TipoConceptoDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_TipoConcepto> FindAll()
        {
            IEnumerable<TC_TipoConcepto> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_TipoConcepto WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_TipoConcepto>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_TipoConcepto>();
            }

            return result;
        }
    }
}
