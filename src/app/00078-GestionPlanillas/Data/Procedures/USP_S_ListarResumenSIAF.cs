using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Data.Procedures
{
    public class USP_S_ListarResumenSIAF
    {
        public static IEnumerable<IDictionary<string, object>> Execute(int I_Anio, int I_Mes, int I_CategoriaPlanillaID)
        {
            IEnumerable<string> cabecera;
            IEnumerable<IDictionary<string, object>> detalle;
            string command;
            DynamicParameters parameters;

            try
            {
                command = "USP_S_ListarResumenSIAF";

                parameters = new DynamicParameters();

                parameters.Add(name: "I_Anio", dbType: DbType.Int32, value: I_Anio);

                parameters.Add(name: "I_Mes", dbType: DbType.Int32, value: I_Mes);

                parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    var query = _dbConnection.QueryMultiple(command, parameters, commandType: CommandType.StoredProcedure);

                    cabecera = query.Read<string>();

                    detalle = query.Read().Select(row => (IDictionary<string, object>)row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
