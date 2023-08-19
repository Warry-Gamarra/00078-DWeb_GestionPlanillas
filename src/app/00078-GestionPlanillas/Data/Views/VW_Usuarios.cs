using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Views
{
    public class VW_Usuarios
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime? D_FecActualizaPassword { get; set; }

        public bool B_CambiaPassword { get; set; }

        public bool B_Habilitado { get; set; }

        public int? I_UsuarioCre { get; set; }

        public int? I_DatosUsuarioID { get; set; }

        public string N_NumDoc { get; set; }

        public string T_NomPersona { get; set; }

        public string T_CorreoUsuario { get; set; }

        public DateTime? D_FecAlta { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int? I_DependenciaID { get; set; }

        public string T_DependenciaDesc { get; set; }

        public static IEnumerable<VW_Usuarios> FindAll()
        {
            IEnumerable<VW_Usuarios> result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_Usuarios;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_Usuarios>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<VW_Usuarios>();
            }

            return result;
        }

        public static VW_Usuarios FindByID(int UserId)
        {
            VW_Usuarios result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_Usuarios WHERE UserId = @UserId;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<VW_Usuarios>(s_command, new { UserId  = UserId }, commandType: System.Data.CommandType.Text);
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
