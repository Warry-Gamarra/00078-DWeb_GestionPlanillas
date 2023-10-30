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
    public class TC_CategoriaPresupuestal
    {
        public int I_CategoriaPresupuestalID { get; set; }

        public string T_CategoriaPresupDesc { get; set; }

        public string T_CategoriaPresupCod { get; set; }

        public bool B_Habilitado { get; set; }

        public static IEnumerable<TC_CategoriaPresupuestal> FindAll()
        {
            IEnumerable<TC_CategoriaPresupuestal> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_CategoriaPresupuestal WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_CategoriaPresupuestal>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<TC_CategoriaPresupuestal>();
            }

            return result;
        }
    }
}
