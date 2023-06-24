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
    public class VW_ConceptosReferencia_Plantilla
    {
        public int I_ID { get; set; }

        public int I_PlantillaPlanillaConceptoID { get; set; }

        public bool B_Habilitado { get; set; }

        public int I_PlantillaPlanillaConceptoReferenciaID { get; set; }

        public int I_ConceptoID { get; set; }

        public string C_ConceptoCod { get; set; }

        public string T_ConceptoDesc { get; set; }

        public string T_ConceptoAbrv { get; set; }

        public static IEnumerable<VW_ConceptosReferencia_Plantilla> FindByConceptoBase(int I_PlantillaPlanillaConceptoID)
        {
            IEnumerable<VW_ConceptosReferencia_Plantilla> result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_ConceptosReferencia_Plantilla WHERE I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_ConceptosReferencia_Plantilla>(s_command, new {
                        I_PlantillaPlanillaConceptoID = I_PlantillaPlanillaConceptoID}, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<VW_ConceptosReferencia_Plantilla>();
            }

            return result;
        }
    }
}
