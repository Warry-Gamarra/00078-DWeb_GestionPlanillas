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
    public class VW_Administrativo
    {
        public int I_TrabajadorID { get; set; }

        public string C_TrabajadorCod { get; set; }

        public string T_Nombre { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public int I_TipoDocumentoID { get; set; }

        public string T_TipoDocumentoDesc { get; set; }

        public string C_NumDocumento { get; set; }

        public DateTime? D_FechaIngreso { get; set; }

        public int? I_RegimenID { get; set; }

        public string T_RegimenDesc { get; set; }

        public int? I_EstadoID { get; set; }

        public string T_EstadoDesc { get; set; }

        public int? I_VinculoID { get; set; }

        public string T_VinculoDesc { get; set; }

        public int I_AdministrativoID { get; set; }

        public int I_GrupoOcupacionalID { get; set; }

        public string C_GrupoOcupacionalCod { get; set; }

        public string T_GrupoOcupacionalDesc { get; set; }

        public int I_NivelRemunerativoID { get; set; }

        public string C_NivelRemunerativoCod { get; set; }

        public string T_NivelRemunerativoDesc { get; set; }

        public static IEnumerable<VW_Administrativo> FindAll()
        {
            IEnumerable<VW_Administrativo> result;

            try
            {
                string s_command = @"SELECT * FROM dbo.VW_Administrativo;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_Administrativo>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static VW_Administrativo FindByAdministrativoID(int I_AdministrativoID)
        {
            VW_Administrativo result;

            try
            {
                string s_command = @"SELECT * FROM dbo.VW_Administrativo adm WHERE adm.I_AdministrativoID = @I_AdministrativoID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<VW_Administrativo>(s_command, new { I_AdministrativoID = I_AdministrativoID }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static IEnumerable<VW_Administrativo> FindByTrabajadorID(int I_TrabajadorID)
        {
            IEnumerable<VW_Administrativo> result;

            try
            {
                string s_command = @"SELECT * FROM dbo.VW_Administrativo adm WHERE adm.I_TrabajadorID = @I_TrabajadorID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_Administrativo>(s_command, new { I_TrabajadorID  = I_TrabajadorID }, commandType: System.Data.CommandType.Text);
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
