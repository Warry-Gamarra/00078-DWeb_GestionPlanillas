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
    public class USP_I_RegistrarPlantillaPlanillaConcepto
    {
        public DataTable Tbl_ConceptoReferencia { get; set; }

        public int I_PlantillaPlanillaID { get; set; }

        public int I_ConceptoID { get; set; }

        public bool B_EsValorFijo { get; set; }

        public bool B_ValorEsExterno { get; set; }

        public decimal? M_ValorConcepto { get; set; }

        public bool B_AplicarFiltro1 { get; set; }

        public int? I_Filtro1 { get; set; }

        public bool B_AplicarFiltro2 { get; set; }

        public int? I_Filtro2 { get; set; }

        public int I_UserID { get; set; }

        public Result Execute()
        {
            Result result;

            DynamicParameters parameters;

            try
            {
                string s_command = "USP_I_RegistrarPlantillaPlanillaConcepto";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    parameters = new DynamicParameters();
                    parameters.Add(name: "Tbl_ConceptoReferencia", value: Tbl_ConceptoReferencia.AsTableValuedParameter("dbo.type_dataIdentifiers"));
                    parameters.Add(name: "I_PlantillaPlanillaID", dbType: DbType.Int32, value: I_PlantillaPlanillaID);
                    parameters.Add(name: "I_ConceptoID", dbType: DbType.Int32, value: I_ConceptoID);
                    parameters.Add(name: "B_EsValorFijo", dbType: DbType.Boolean, value: B_EsValorFijo);
                    parameters.Add(name: "B_ValorEsExterno", dbType: DbType.Boolean, value: B_ValorEsExterno);
                    parameters.Add(name: "M_ValorConcepto", dbType: DbType.Decimal, value: M_ValorConcepto);
                    parameters.Add(name: "B_AplicarFiltro1", dbType: DbType.Boolean, value: B_AplicarFiltro1);
                    parameters.Add(name: "I_Filtro1", dbType: DbType.Int32, value: I_Filtro1);
                    parameters.Add(name: "B_AplicarFiltro2", dbType: DbType.Boolean, value: B_AplicarFiltro2);
                    parameters.Add(name: "I_Filtro2", dbType: DbType.Int32, value: I_Filtro2);
                    parameters.Add(name: "I_UserID", dbType: DbType.Int32, value: I_UserID);
                    parameters.Add(name: "B_Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add(name: "T_Message", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                    _dbConnection.Execute(s_command, parameters, commandType: CommandType.StoredProcedure);

                    result = new Result()
                    {
                        Success = parameters.Get<bool>("B_Result"),
                        Message = parameters.Get<string>("T_Message")
                    };
                }
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    Message = ex.Message,
                };
            }

            return result;
        }
    }
}
