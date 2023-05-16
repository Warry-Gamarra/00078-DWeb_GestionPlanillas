using Dapper;
using Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Views
{
    public class VW_PlantillaPlanilla
    {
        public int I_PlantillaPlanillaID { get; set; }

        public string T_PlantillaPlanillaDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }

        public int I_ClasePlanillaID { get; set; }

        public string T_ClasePlanillaDesc { get; set; }

        public static IEnumerable<VW_PlantillaPlanilla> FindAll()
        {
            IEnumerable<VW_PlantillaPlanilla> result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_PlantillaPlanilla;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_PlantillaPlanilla>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<VW_PlantillaPlanilla>();
            }

            return result;
        }
    }
}
