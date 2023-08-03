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
    public class VW_ConceptosAsignados_Plantilla
    {
        public int I_PlantillaPlanillaConceptoID { get; set; }

        public int I_PlantillaPlanillaID { get; set; }

        public string T_PlantillaPlanillaDesc { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }

        public int I_ClasePlanillaID { get; set; }

        public string T_ClasePlanillaDesc { get; set; }

        public int I_TipoConceptoID { get; set; }

        public string T_TipoConceptoDesc { get; set; }

        public bool B_EsAdicion { get; set; }

        public bool B_IncluirEnTotalBruto { get; set; }

        public int I_ConceptoID { get; set; }

        public string C_ConceptoCod { get; set; }

        public string T_ConceptoDesc { get; set; }

        public string T_ConceptoAbrv { get; set; }

        public bool B_EsValorFijo { get; set; }

        public bool B_ValorEsExterno { get; set; }

        public decimal? M_ValorConcepto { get; set; }

        public bool B_AplicarFiltro1 { get; set; }

        public int? I_Filtro1 { get; set; }

        public bool B_AplicarFiltro2 { get; set; }

        public int? I_Filtro2 { get; set; }

        public bool B_Habilitado { get; set; }

        public static VW_ConceptosAsignados_Plantilla FindByID(int I_PlantillaPlanillaConceptoID)
        {
            VW_ConceptosAsignados_Plantilla result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_ConceptosAsignados_Plantilla WHERE I_PlantillaPlanillaConceptoID = @I_PlantillaPlanillaConceptoID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<VW_ConceptosAsignados_Plantilla>(s_command, new { I_PlantillaPlanillaConceptoID = I_PlantillaPlanillaConceptoID },
                        commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public static IEnumerable<VW_ConceptosAsignados_Plantilla> FindByPlantillaPlanillaID(int I_PlantillaPlanillaID)
        {
            IEnumerable<VW_ConceptosAsignados_Plantilla> result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_ConceptosAsignados_Plantilla WHERE I_PlantillaPlanillaID = @I_PlantillaPlanillaID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_ConceptosAsignados_Plantilla>(s_command, new { I_PlantillaPlanillaID = I_PlantillaPlanillaID }, 
                        commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<VW_ConceptosAsignados_Plantilla>();
            }

            return result;
        }

        public static IEnumerable<VW_ConceptosAsignados_Plantilla> FindByCategoriaYConceptoID(int I_CategoriaPlanillaID, int I_ConceptoID)
        {
            IEnumerable<VW_ConceptosAsignados_Plantilla> result;

            try
            {
                string s_command = @"SELECT * FROM dbo.VW_ConceptosAsignados_Plantilla 
                    WHERE B_Habilitado = 1 AND I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND I_ConceptoID = @I_ConceptoID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_ConceptosAsignados_Plantilla>(s_command, new { I_CategoriaPlanillaID = I_CategoriaPlanillaID, I_ConceptoID = I_ConceptoID },
                        commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<VW_ConceptosAsignados_Plantilla>();
            }

            return result;
        }
    }
}
