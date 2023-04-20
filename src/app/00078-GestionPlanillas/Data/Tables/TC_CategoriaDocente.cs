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
    public class TC_CategoriaDocente
    {
        public int I_CategoriaDocenteID { get; set; }

        public string T_CategoriaDocenteDesc { get; set; }

        public string C_CategoriaDocenteCod { get; set; }

        public bool B_Habilitado { get; set; }

        public bool B_Eliminado { get; set; }

        public int? I_UsuarioCre { get; set; }

        public DateTime? D_FecCre { get; set; }

        public int? I_UsuarioMod { get; set; }

        public DateTime? D_FecMod { get; set; }

        public static IEnumerable<TC_CategoriaDocente> FindAll()
        {
            IEnumerable<TC_CategoriaDocente> result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_CategoriaDocente WHERE B_Eliminado = 0;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<TC_CategoriaDocente>(s_command, commandType: System.Data.CommandType.Text);
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
