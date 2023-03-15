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
    public class VW_Docentes
    {
        public int I_TrabajadorID { get; set; }

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

        public int I_DocenteID { get; set; }

        public int I_CategoriaDocenteID { get; set; }

        public string C_CategoriaDocenteCod { get; set; }

        public string T_CategoriaDocenteDesc { get; set; }

        public int I_HorasDocenteID { get; set; }

        public int I_Horas { get; set; }

        public int I_DedicacionDocenteID { get; set; }

        public string C_DedicacionDocenteCod { get; set; }

        public string T_DedicacionDocenteDesc { get; set; }

        public static IEnumerable<VW_Docentes> FindAll()
        {
            IEnumerable<VW_Docentes> result;

            try
            {
                string s_command = @"SELECT * FROM dbo.VW_Docentes;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_Docentes>(s_command, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static VW_Docentes FindByDocenteID(int I_DocenteID)
        {
            VW_Docentes result;

            try
            {
                string s_command = @"SELECT * FROM dbo.VW_Docentes doc WHERE doc.I_DocenteID = @I_DocenteID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.QuerySingleOrDefault<VW_Docentes>(s_command, new { I_DocenteID = I_DocenteID }, commandType: System.Data.CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static IEnumerable<VW_Docentes> FindByTrabajadorID(int I_TrabajadorID)
        {
            IEnumerable<VW_Docentes> result;

            try
            {
                string s_command = @"SELECT * FROM dbo.VW_Docentes doc WHERE doc.I_TrabajadorID = @I_TrabajadorID;";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    result = _dbConnection.Query<VW_Docentes>(s_command, new { I_TrabajadorID = I_TrabajadorID }, commandType: System.Data.CommandType.Text);
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
