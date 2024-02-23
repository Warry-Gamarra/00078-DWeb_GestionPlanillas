using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Data.Tables
{
    public class TR_TrabajadorPlanilla
    {
        public static bool ExistePlanillaTrabajador(int I_TrabajadorID, int I_CategoriaPlanillaID)
        {
            bool existePlanillaTrabajador;
            int cantRegistros;

            try
            {
                string s_command = @"SELECT tp.I_TrabajadorID, p.I_CategoriaPlanillaID FROM dbo.TR_Planilla p
                    INNER JOIN dbo.TR_TrabajadorPlanilla tp ON tp.I_PlanillaID = p.I_PlanillaID
                    WHERE p.B_Anulado = 0 AND tp.B_Anulado = 0 AND tp.I_TrabajadorID = @I_TrabajadorID AND p.I_CategoriaPlanillaID = @I_CategoriaPlanillaID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    cantRegistros = _dbConnection.Query<int>(s_command, new { I_TrabajadorID = I_TrabajadorID, I_CategoriaPlanillaID = I_CategoriaPlanillaID }, commandType: CommandType.Text).Count();

                    existePlanillaTrabajador = cantRegistros > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return existePlanillaTrabajador;
        }
    }
}
