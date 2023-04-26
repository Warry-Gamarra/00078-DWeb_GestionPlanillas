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
    public class TC_CategoriaPlanilla
    {
        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_CategoriaPlanilla> FindAll()
        {
            IEnumerable<TC_CategoriaPlanilla> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_CategoriaPlanilla WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_CategoriaPlanilla>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_CategoriaPlanilla>();
            }

            return result;
        }
    }
}
