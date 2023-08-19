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
    public class Webpages_Roles
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public static IEnumerable<Webpages_Roles> FindAll()
        {
            IEnumerable<Webpages_Roles> result;

            try
            {
                string s_command = "SELECT * FROM dbo.Webpages_Roles;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<Webpages_Roles>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static Webpages_Roles FindByID(int RoleId)
        {
            Webpages_Roles result;

            try
            {
                string s_command = "SELECT * FROM dbo.Webpages_Roles WHERE RoleId = @RoleId;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<Webpages_Roles>(s_command, new { RoleId = RoleId }, commandType: System.Data.CommandType.Text);
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
