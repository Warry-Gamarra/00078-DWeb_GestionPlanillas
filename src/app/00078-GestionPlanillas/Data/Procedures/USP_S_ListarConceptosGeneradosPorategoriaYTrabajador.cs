using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Procedures
{
    public class USP_S_ListarConceptosGeneradosPorategoriaYTrabajador
    {
        public int I_TipoConceptoID { get; set; }

        public string T_TipoConceptoDesc { get; set; }

        public string C_ConceptoCod { get; set; }

        public string T_ConceptoDesc { get; set; }

        public string T_ConceptoAbrv { get; set; }

        public decimal M_Monto { get; set; }

        public static IEnumerable<USP_S_ListarConceptosGeneradosPorategoriaYTrabajador> Execute(int I_TrabajadorPlanillaID)
        {
            IEnumerable<USP_S_ListarConceptosGeneradosPorategoriaYTrabajador> result;
            DynamicParameters parameters;
            string command;

            try
            {
                command = "USP_S_ListarConceptosGeneradosPorategoriaYTrabajador";

                parameters = new DynamicParameters();

                parameters.Add(name: "I_TrabajadorPlanillaID", dbType: DbType.Int32, value: I_TrabajadorPlanillaID);

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<USP_S_ListarConceptosGeneradosPorategoriaYTrabajador>(command, parameters, commandType: CommandType.StoredProcedure);
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
