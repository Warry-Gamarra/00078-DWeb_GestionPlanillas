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
    public class USP_I_RegistrarTrabajador
    {
        public string C_TrabajadorCod { get; set; }

        public string C_CodigoPlaza { get; set; }

        public int? I_PersonaID { get; set; }

        public string T_ApellidoPaterno { get; set; }

        public string T_ApellidoMaterno { get; set; }

        public string T_Nombre { get; set; }

        public int I_TipoDocumentoID { get; set; }

        public string C_NumDocumento { get; set; }

        public int I_SexoID {  get; set; }

        public DateTime? D_FechaIngreso { get; set; }

        public int I_RegimenID { get; set; }

        public int I_EstadoID { get; set; }

        public int I_VinculoID { get; set; }

        public int? I_BancoID { get; set; }

        public string T_NroCuentaBancaria { get;set; }

        public int? I_TipoCuentaBancariaID { get; set; }

        public int I_DependenciaID { get;set; }

        public int? I_AfpID { get; set; }

        public string T_Cuspp { get; set; }

        public int? I_CategoriaDocenteID { get; set; }

        public int? I_HorasDocenteID { get; set; }

        public int? I_GrupoOcupacionalID { get; set; }

        public int? I_NivelRemunerativoID { get; set; }

        public int I_CategoriaPlanillaID { get; set; }

        public int I_UserID { get; set; }

        public Result Execute()
        {
            Result result;

            DynamicParameters parameters;

            try
            {
                string s_command = "USP_I_RegistrarTrabajador";

                using (var _dbConnection = new SqlConnection(Database.ConnectionString))
                {
                    parameters = new DynamicParameters();
                    parameters.Add(name: "C_TrabajadorCod", dbType: DbType.String, value: C_TrabajadorCod);
                    parameters.Add(name: "C_CodigoPlaza", dbType: DbType.String, value: C_CodigoPlaza);
                    parameters.Add(name: "I_PersonaID", dbType: DbType.Int32, value: I_PersonaID);
                    parameters.Add(name: "T_ApellidoPaterno", dbType: DbType.String, value: T_ApellidoPaterno);
                    parameters.Add(name: "T_ApellidoMaterno", dbType: DbType.String, value: T_ApellidoMaterno);
                    parameters.Add(name: "T_Nombre", dbType: DbType.String, value: T_Nombre);
                    parameters.Add(name: "I_TipoDocumentoID", dbType: DbType.Int32, value: I_TipoDocumentoID);
                    parameters.Add(name: "C_NumDocumento", dbType: DbType.String, value: C_NumDocumento);
                    parameters.Add(name: "I_SexoID", dbType: DbType.Int32, value: I_SexoID);
                    parameters.Add(name: "D_FechaIngreso", dbType: DbType.DateTime, value: D_FechaIngreso);
                    parameters.Add(name: "I_RegimenID", dbType: DbType.Int32, value: I_RegimenID);
                    parameters.Add(name: "I_EstadoID", dbType: DbType.Int32, value: I_EstadoID);
                    parameters.Add(name: "I_VinculoID", dbType: DbType.Int32, value: I_VinculoID);
                    parameters.Add(name: "I_BancoID", dbType: DbType.Int32, value: I_BancoID);
                    parameters.Add(name: "T_NroCuentaBancaria", dbType: DbType.String, value: T_NroCuentaBancaria);
                    parameters.Add(name: "I_TipoCuentaBancariaID", dbType: DbType.Int32, value: I_TipoCuentaBancariaID);
                    parameters.Add(name: "I_DependenciaID", dbType: DbType.Int32, value: I_DependenciaID);
                    parameters.Add(name: "I_AfpID", dbType: DbType.Int32, value: I_AfpID);
                    parameters.Add(name: "T_Cuspp", dbType: DbType.String, value: T_Cuspp);
                    parameters.Add(name: "I_UserID", dbType: DbType.Int32, value: I_UserID);
                    parameters.Add(name: "I_CategoriaDocenteID", dbType: DbType.Int32, value: I_CategoriaDocenteID);
                    parameters.Add(name: "I_HorasDocenteID", dbType: DbType.Int32, value: I_HorasDocenteID);
                    parameters.Add(name: "I_GrupoOcupacionalID", dbType: DbType.Int32, value: I_GrupoOcupacionalID);
                    parameters.Add(name: "I_NivelRemunerativoID", dbType: DbType.Int32, value: I_NivelRemunerativoID);
                    parameters.Add(name: "I_CategoriaPlanillaID", dbType: DbType.Int32, value: I_CategoriaPlanillaID);
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
