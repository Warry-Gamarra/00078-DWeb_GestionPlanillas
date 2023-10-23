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
    public class TC_Meta
    {
        public int I_MetaID { get; set; }

        public string T_MetaDesc { get; set; }

        public string C_MetaCod { get; set; }

        public static IEnumerable<TC_Meta> FindAll()
        {
            IEnumerable<TC_Meta> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Meta WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_Meta>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_Meta>();
            }

            return result;
        }

        public static TC_Meta FindByID(int I_MetaID)
        {
            TC_Meta result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Meta WHERE B_Eliminado = 0 AND I_MetaID = @I_MetaID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<TC_Meta>(s_command, new { I_MetaID = I_MetaID }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public static TC_Meta FindByCod(string C_MetaCod)
        {
            TC_Meta result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Meta WHERE B_Eliminado = 0 AND C_MetaCod = @C_MetaCod;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<TC_Meta>(s_command, new { C_MetaCod = C_MetaCod }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}
