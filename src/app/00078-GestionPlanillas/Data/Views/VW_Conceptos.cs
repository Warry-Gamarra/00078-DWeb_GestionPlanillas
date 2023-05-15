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
    public class VW_Conceptos
    {
        public int I_ConceptoID { get; set; }

        public int I_TipoConceptoID { get; set; }

        public string C_ConceptoCod { get; set; }

        public string T_ConceptoDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public string T_TipoConceptoDesc { get; set; }

        public static IEnumerable<VW_Conceptos> FindAll()
        {
            IEnumerable<VW_Conceptos> result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_Conceptos;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_Conceptos>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<VW_Conceptos>();
            }

            return result;
        }

        public static VW_Conceptos FindByID(int I_ConceptoID)
        {
            VW_Conceptos result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_Conceptos WHERE I_ConceptoID = @I_ConceptoID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<VW_Conceptos>(s_command, new { I_ConceptoID = I_ConceptoID }, commandType: System.Data.CommandType.Text);
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
