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
    public class TC_Persona
    {
        public int I_PersonaID { get; set; }

        public int I_TipoDocumentoID { get; set; }

        public string C_NumDocumento { get; set; }

        public string T_Nombre { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public DateTime D_FecNac { get; set; }

        public string C_Cui { get; set; }

        public bool B_Habilitado { get; set; }

        public static TC_Persona FindByNumDocumento(int I_TipoDocumentoID, string C_NumDocumento)
        {
            TC_Persona result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_Persona WHERE B_Eliminado = 0 AND I_TipoDocumentoID = @I_TipoDocumentoID AND C_NumDocumento = @C_NumDocumento;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<TC_Persona>(s_command, new {
                        I_TipoDocumentoID = I_TipoDocumentoID,
                        C_NumDocumento = C_NumDocumento
                    },commandType: System.Data.CommandType.Text);
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
