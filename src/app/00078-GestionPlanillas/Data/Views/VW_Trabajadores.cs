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
    public class VW_Trabajadores
    {
        public int I_TrabajadorID { get; set; }

        public string C_TrabajadorCod { get; set; }

        public int I_PersonaID { get; set; }

        public string T_Nombre { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public int I_TipoDocumentoID { get; set; }

        public string T_TipoDocumentoDesc { get; set; }

        public string C_NumDocumento { get; set; }

        public DateTime? D_FechaIngreso { get; set; }

        public int? I_RegimenID { get; set; }

        public string T_RegimenDesc { get; set; }

        public int? I_AfpID { get; set; }

        public string T_AfpDesc { get; set; }

        public string T_Cuspp { get; set; }

        public int I_EstadoID { get; set; }

        public string T_EstadoDesc { get; set; }

        public int I_VinculoID { get; set; }

        public string T_VinculoDesc { get; set; }

        public int? I_TrabajadorDependenciaID { get; set; }

        public int I_DependenciaID { get; set; }

        public string C_DependenciaCod { get; set; }

        public string T_DependenciaDesc { get; set; }

        public int? I_CuentaBancariaID { get; set; }

        public string T_NroCuentaBancaria { get; set; }

        public int? I_BancoID { get; set; }

        public string T_BancoDesc { get;set; }

        public string T_BancoAbrv { get; set; }

        public static IEnumerable<VW_Trabajadores> FindAll()
        {
            IEnumerable<VW_Trabajadores> result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_Trabajadores;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_Trabajadores>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<VW_Trabajadores>();
            }

            return result;
        }

        public static VW_Trabajadores FindByID(int I_TrabajadorID)
        {
            VW_Trabajadores result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_Trabajadores WHERE I_TrabajadorID = @I_TrabajadorID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<VW_Trabajadores>(s_command, new { I_TrabajadorID  = I_TrabajadorID  }, commandType: System.Data.CommandType.Text);
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
