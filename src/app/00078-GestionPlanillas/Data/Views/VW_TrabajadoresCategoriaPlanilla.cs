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
    public class VW_TrabajadoresCategoriaPlanilla
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

        public int I_EstadoID { get; set; }

        public string T_EstadoDesc { get; set; }

        public int I_VinculoID { get; set; }

        public string T_VinculoDesc { get; set; }

        public int I_TrabajadorCategoriaPlanillaID { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }

        public static IEnumerable<VW_TrabajadoresCategoriaPlanilla> FindByFilters(int? I_CategoriaPlanillaID = null)
        {
            IEnumerable<VW_TrabajadoresCategoriaPlanilla> result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_TrabajadoresCategoriaPlanilla ";

                if (I_CategoriaPlanillaID.HasValue)
                {
                    s_command = s_command + "WHERE I_CategoriaPlanillaID = @I_CategoriaPlanillaID";
                }

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_TrabajadoresCategoriaPlanilla>(s_command,
                        new { I_CategoriaPlanillaID = I_CategoriaPlanillaID }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<VW_TrabajadoresCategoriaPlanilla>();
            }

            return result;
        }

        public static VW_TrabajadoresCategoriaPlanilla FindByDocumentoYCategoria(int I_TipoDocumentoID, string C_NumDocumento, int I_CategoriaPlanillaID)
        {
            VW_TrabajadoresCategoriaPlanilla result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_TrabajadoresCategoriaPlanilla WHERE I_TipoDocumentoID = @I_TipoDocumentoID AND C_NumDocumento = @C_NumDocumento;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<VW_TrabajadoresCategoriaPlanilla>(s_command,
                        new { I_TipoDocumentoID = I_TipoDocumentoID, C_NumDocumento = C_NumDocumento, I_CategoriaPlanillaID = I_CategoriaPlanillaID }, commandType: System.Data.CommandType.Text);
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
