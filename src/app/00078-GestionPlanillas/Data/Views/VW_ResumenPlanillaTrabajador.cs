using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Views
{
    public class VW_ResumenPlanillaTrabajador
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

        public int I_TrabajadorPlanillaID { get; set; }

        public decimal I_TotalRemuneracion { get; set; }

        public decimal I_TotalReintegro { get; set; }

        public decimal I_TotalDeduccion { get; set; }

        public decimal I_TotalBruto { get; set; }

        public decimal I_TotalDescuento { get; set; }

        public decimal I_TotalSueldo { get; set; }

        public int I_PlanillaID { get; set; }

        public int I_PeriodoID { get; set; }

        public int I_Anio { get; set; }

        public int I_Mes { get; set; }

        public string T_MesDesc { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }

        public static IEnumerable<VW_ResumenPlanillaTrabajador> FindAll(int? I_Anio, int? I_Mes, int? I_CategoriaPlanillaID)
        {
            IEnumerable<VW_ResumenPlanillaTrabajador> result;
            DynamicParameters parameters;
            string command, filters = "";

            try
            {
                command = "SELECT * FROM dbo.VW_ResumenPlanillaTrabajador";

                parameters = new DynamicParameters();

                if (I_Anio.HasValue)
                {
                    filters = " WHERE I_Anio = @I_Anio";

                    parameters.Add(name: "I_Anio", dbType: DbType.Int32, value: I_Anio);
                }

                if (I_Mes.HasValue)
                {
                    filters = filters + (filters.Length == 0 ? " WHERE " : " AND ") + "I_Mes = @I_Mes";

                    parameters.Add(name: "I_Mes", dbType: DbType.Int32, value: I_Mes);
                }

                if (I_CategoriaPlanillaID.HasValue)
                {
                    filters = filters + (filters.Length == 0 ? " WHERE " : " AND ") + "I_CategoriaPlanillaID = @I_CategoriaPlanillaID";

                    parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);
                }

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_ResumenPlanillaTrabajador>(command + filters, parameters, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static VW_ResumenPlanillaTrabajador GetByPeriodoTrabajador(int I_TrabajadorID, int I_PeriodoID, int I_CategoriaPlanillaID)
        {
            VW_ResumenPlanillaTrabajador result;
            DynamicParameters parameters;
            string command;

            try
            {
                command = "SELECT * FROM dbo.VW_ResumenPlanillaTrabajador p " +
                    "WHERE p.I_TrabajadorID = @I_TrabajadorID AND p.I_PeriodoID = @I_PeriodoID AND p.I_CategoriaPlanillaID = @I_CategoriaPlanillaID;";

                parameters = new DynamicParameters();
                parameters.Add(name: "I_TrabajadorID", dbType: DbType.Int32, value: I_TrabajadorID);
                parameters.Add(name: "I_PeriodoID", dbType: DbType.Int32, value: I_PeriodoID);
                parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);
                
                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<VW_ResumenPlanillaTrabajador>(command, parameters, commandType: System.Data.CommandType.Text);
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
