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
    public class USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador
    {
        public int I_PlanillaID { get; set; }

        public int I_TrabajadorPlanillaID { get; set; }

        public int I_Anio { get; set; }

        public int I_Mes { get; set; }

        public string T_MesDesc { get; set; }

        public string T_DependenciaDesc { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }

        public string T_ClasePlanillaDesc { get; set; }

        public string T_TipoPlanillaDesc { get; set; }

        public decimal I_TotalRemuneracion {  get; set; }

        public decimal I_TotalReintegro { get; set; }

        public decimal I_TotalDeduccion { get; set; }

        public decimal I_TotalBruto { get; set; }

        public decimal I_TotalDescuento { get; set; }

        public decimal I_TotalSueldo { get; set; }

        public static IEnumerable<USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador> Execute(int I_TrabajadorID, int I_Anio, int I_Mes)
        {
            IEnumerable<USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador> result;
            DynamicParameters parameters;
            string command;

            try
            {
                command = "USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador";

                parameters = new DynamicParameters();

                parameters.Add(name: "I_TrabajadorID", dbType: DbType.Int32, value: I_TrabajadorID);

                parameters.Add(name: "I_Anio", dbType: DbType.Int32, value: I_Anio);

                parameters.Add(name: "I_Mes", dbType: DbType.Int32, value: I_Mes);

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador>(command, parameters, commandType: CommandType.StoredProcedure);
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
