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
    public class TC_Proveedor
    {
        public int I_ProveedorID { get; set; }

        public string T_ProveedorDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_Proveedor> FindAll()
        {
            IEnumerable<TC_Proveedor> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Proveedor WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_Proveedor>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_Proveedor>();
            }

            return result;
        }
    }
}
