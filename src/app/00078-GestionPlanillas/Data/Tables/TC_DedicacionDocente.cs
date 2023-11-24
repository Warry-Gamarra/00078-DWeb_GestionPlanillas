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
    public class TC_DedicacionDocente
    {
        public int I_DedicacionDocenteID { get; set; }

        public string T_DedicacionDocenteDesc { get; set; }

        public string C_DedicacionDocenteCod { get; set; }

        public bool B_Habilitado { get; set; }

        public bool B_ParaDocenteOrdinario { get; set; }

        public static TC_DedicacionDocente FindByID(int I_DedicacionDocenteID)
        {
            TC_DedicacionDocente result;

            try
            {
                string s_command = "SELECT * FROM dbo.TC_DedicacionDocente WHERE B_Eliminado = 0 AND I_DedicacionDocenteID = @I_DedicacionDocenteID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<TC_DedicacionDocente>(s_command, 
                        new { I_DedicacionDocenteID = I_DedicacionDocenteID }, commandType: System.Data.CommandType.Text);
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
