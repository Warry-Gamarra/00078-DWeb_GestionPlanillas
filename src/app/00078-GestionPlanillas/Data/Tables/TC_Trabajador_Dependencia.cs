using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Tables
{
    public class TC_Trabajador_Dependencia
    {
        public static bool ExisteTrabajador(int I_DependenciaID)
        {
            bool isThereAnyEmployee;
            int cantRegistros;

            try
            {
                string s_command = @"SELECT td.I_TrabajadorDependenciaID FROM dbo.TC_Trabajador_Dependencia td 
                    INNER JOIN dbo.TC_Trabajador trab ON trab.I_TrabajadorID = td.I_TrabajadorID 
                    WHERE trab.B_Eliminado = 0 AND td.B_Eliminado = 0 AND td.I_DependenciaID = @I_DependenciaID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    cantRegistros = _dbConnection.Query<int>(s_command, new { I_DependenciaID = I_DependenciaID }, commandType: System.Data.CommandType.Text).Count();

                    isThereAnyEmployee = cantRegistros > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isThereAnyEmployee;
        }
    }
}
