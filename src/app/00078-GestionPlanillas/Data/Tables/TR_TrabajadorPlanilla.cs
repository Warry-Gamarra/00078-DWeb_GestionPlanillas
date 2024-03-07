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
        public static bool ExistePlanillaTrabajador(int I_TrabajadorCategoriaPlanillaID)
        {
            bool existePlanillaTrabajador;
            int cantRegistros;

            try
            {
                string s_command = @"SELECT tp.I_TrabajadorPlanillaID FROM dbo.TR_Planilla p
                    INNER JOIN dbo.TR_TrabajadorPlanilla tp ON tp.I_PlanillaID = p.I_PlanillaID
                    WHERE p.B_Anulado = 0 AND tp.B_Anulado = 0 AND tp.I_TrabajadorCategoriaPlanillaID = @I_TrabajadorCategoriaPlanillaID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    cantRegistros = _dbConnection.Query<int>(s_command, new { I_TrabajadorCategoriaPlanillaID = I_TrabajadorCategoriaPlanillaID }, commandType: CommandType.Text).Count();

                    existePlanillaTrabajador = cantRegistros > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return existePlanillaTrabajador;
        }

        public static bool ExisteGrupoTrabajo(int I_GrupoTrabajoID)
        {
            bool existeGrupoTrabajo;
            int cantRegistros;

            try
            {
                string s_command = @"SELECT tp.I_TrabajadorPlanillaID FROM dbo.TR_Planilla p
                    INNER JOIN dbo.TR_TrabajadorPlanilla tp ON tp.I_PlanillaID = p.I_PlanillaID
                    WHERE p.B_Anulado = 0 AND tp.B_Anulado = 0 AND tp.I_GrupoTrabajoID = @I_GrupoTrabajoID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    cantRegistros = _dbConnection.Query<int>(s_command, new { I_GrupoTrabajoID = I_GrupoTrabajoID }, commandType: CommandType.Text).Count();

                    existeGrupoTrabajo = cantRegistros > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return existeGrupoTrabajo;
        }
    }
}
