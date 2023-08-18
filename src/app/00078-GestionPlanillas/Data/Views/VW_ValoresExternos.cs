using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Views
{
    public class VW_ValoresExternos
    {
        public int I_ConceptoExternoValorID { get; set; }

        public int I_PeriodoID { get; set; }

        public int I_Anio { get; set; }

        public int I_Mes { get; set; }

        public string T_MesDesc { get; set; }

        public int I_TrabajadorID { get; set; }

        public string T_TipoDocumentoDesc { get; set; }

        public string C_NumDocumento { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public string T_Nombre { get; set; }

        public int I_TrabajadorCategoriaPlanillaID { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }

        public int I_ConceptoID { get; set; }

        public string C_ConceptoCod { get; set; }

        public string T_ConceptoDesc { get; set; }

        public string T_TipoConceptoDesc { get; set; }

        public decimal M_ValorConcepto { get; set; }

        public int I_ProveedorID { get; set; }

        public string T_ProveedorDesc { get; set; }

        public static IEnumerable<VW_ValoresExternos> FindAll(int I_Anio, int I_Mes, int I_CategoriaPlanillaID)
        {
            IEnumerable<VW_ValoresExternos> result;
            DynamicParameters parameters;
            string command;

            try
            {
                command = "SELECT * FROM dbo.VW_ValoresExternos WHERE I_Anio = @I_Anio AND I_Mes = @I_Mes AND I_CategoriaPlanillaID = @I_CategoriaPlanillaID;";

                parameters = new DynamicParameters();
                parameters.Add(name: "I_Anio", dbType: DbType.Int32, value: I_Anio);
                parameters.Add(name: "I_Mes", dbType: DbType.Int32, value: I_Mes);
                parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_ValoresExternos>(command, parameters, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static VW_ValoresExternos FindByID(int I_ConceptoExternoValorID)
        {
            VW_ValoresExternos result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_ValoresExternos WHERE I_ConceptoExternoValorID = @I_ConceptoExternoValorID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<VW_ValoresExternos>(s_command, new { I_ConceptoExternoValorID = I_ConceptoExternoValorID }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public static VW_ValoresExternos FindByTrabajadorCategoriaPlanillaYConcepto(int I_PeriodoID, int I_TrabajadorCategoriaPlanillaID, int I_ConceptoID)
        {
            VW_ValoresExternos result;

            try
            {
                string s_command = @"SELECT * FROM dbo.VW_ValoresExternos 
                    WHERE I_PeriodoID = @I_PeriodoID AND I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID AND I_ConceptoID = @I_ConceptoID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<VW_ValoresExternos>(s_command, 
                        new { I_PeriodoID = I_PeriodoID, I_TrabajadorCategoriaPlanillaID = I_TrabajadorCategoriaPlanillaID, I_ConceptoID = I_ConceptoID }, 
                        commandType: System.Data.CommandType.Text);
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
