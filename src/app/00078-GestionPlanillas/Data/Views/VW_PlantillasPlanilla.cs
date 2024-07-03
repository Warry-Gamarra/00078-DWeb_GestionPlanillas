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
    public class VW_PlantillasPlanilla
    {
        public int I_PlantillaPlanillaID { get; set; }

        public string T_PlantillaPlanillaDesc { get; set; }

        public bool B_Habilitado { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }

        public int I_ClasePlanillaID { get; set; }

        public string T_ClasePlanillaDesc { get; set; }

        public static IEnumerable<VW_PlantillasPlanilla> FindAll()
        {
            IEnumerable<VW_PlantillasPlanilla> result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_PlantillasPlanilla;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_PlantillasPlanilla>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<VW_PlantillasPlanilla>();
            }

            return result;
        }

        public static VW_PlantillasPlanilla FindByID(int I_PlantillaPlanillaID)
        {
            VW_PlantillasPlanilla result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_PlantillasPlanilla WHERE I_PlantillaPlanillaID = @I_PlantillaPlanillaID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingle<VW_PlantillasPlanilla>(s_command, new { I_PlantillaPlanillaID  = I_PlantillaPlanillaID }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public static VW_PlantillasPlanilla FindByCategoriaID(int I_CategoriaPlanillaID)
        {
            VW_PlantillasPlanilla result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_PlantillasPlanilla WHERE B_Habilitado = 1 AND I_CategoriaPlanillaID = @I_CategoriaPlanillaID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QueryFirst<VW_PlantillasPlanilla>(s_command, new { I_CategoriaPlanillaID = I_CategoriaPlanillaID }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}
