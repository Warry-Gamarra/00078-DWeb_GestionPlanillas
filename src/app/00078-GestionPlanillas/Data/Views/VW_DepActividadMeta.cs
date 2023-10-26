using Dapper;
using Data.Connection;
using Data.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Views
{
    public class VW_DepActividadMeta
    {
        public int I_DepActividadMetaID { get; set; }

        public int I_Anio { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public string T_CategoriaPlanillaDesc { get; set; }

        public int I_DependenciaID { get; set; }

        public string C_DependenciaCod { get; set; }

        public string T_DependenciaDesc { get; set; }

        public string T_Descripcion { get; set; }

        public int I_ActividadID { get; set; }

        public string C_ActividadCod { get; set; }

        public string T_ActividadDesc { get; set; }

        public int I_MetaID { get; set; }

        public string C_MetaCod { get; set; }

        public string T_MetaDesc { get; set; }

        public int I_CategoriaPresupuestalID { get; set; }

        public string T_CategoriaPresupCod { get; set; }

        public string T_CategoriaPresupDesc { get; set; }

        public static IEnumerable<VW_DepActividadMeta> FindAll()
        {
            IEnumerable<VW_DepActividadMeta> result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_DepActividadMeta;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_DepActividadMeta>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = new List<VW_DepActividadMeta>();
            }

            return result;
        }

        public static VW_DepActividadMeta FindByID(int I_DepActividadMetaID)
        {
            VW_DepActividadMeta result;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_DepActividadMeta WHERE I_DepActividadMetaID = @I_DepActividadMetaID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<VW_DepActividadMeta>(s_command, new { I_DepActividadMetaID = I_DepActividadMetaID }, 
                        commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public static bool IsDuplicate(int? I_DepActividadMetaID, int I_Anio, int I_CategoriaPlanillaID, int I_DependenciaID, int I_ActividadID, int I_MetaID)
        {
            IEnumerable<VW_DepActividadMeta> result;
            bool isDuplicate;

            try
            {
                string s_command = "SELECT * FROM dbo.VW_DepActividadMeta WHERE ";

                s_command += I_DepActividadMetaID.HasValue ? "NOT I_DepActividadMetaID = @I_DepActividadMetaID AND " : "";

                s_command += "I_Anio = @I_Anio AND I_CategoriaPlanillaID = @I_CategoriaPlanillaID AND I_DependenciaID = @I_DependenciaID AND I_ActividadID = @I_ActividadID AND I_MetaID = @I_MetaID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_DepActividadMeta>(s_command, new {
                        I_DepActividadMetaID = I_DepActividadMetaID,
                        I_Anio = I_Anio,
                        I_CategoriaPlanillaID = I_CategoriaPlanillaID,
                        I_DependenciaID = I_DependenciaID,
                        I_ActividadID = I_ActividadID,
                        I_MetaID = I_MetaID
                    },commandType: System.Data.CommandType.Text);
                }

                isDuplicate = result.Count() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isDuplicate;
        }
    }
}
