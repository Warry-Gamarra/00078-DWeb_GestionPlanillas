using Data.Connection;
using Data.Procedures;
using Data.Views;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class TrabajadorService : ITrabajadorService
    {
        public List<TrabajadorDTO> ListarTrabajadores()
        {
            var lista = VW_Trabajadores.FindAll()
                .Select( x => Mapper.VW_Trabajadores_To_TrabajadorDTO(x))
                .ToList();

            return lista;
        }

        public TrabajadorDTO ObtenerTrabajador(int I_TrabajadorID)
        {
            TrabajadorDTO trabajadorDTO;

            var view = VW_Trabajadores.FindByID(I_TrabajadorID);

            if (view == null)
            {
                trabajadorDTO = null;
            }
            else
            {
                trabajadorDTO = Mapper.VW_Trabajadores_To_TrabajadorDTO(view);
            }

            return trabajadorDTO;
        }

        public Response GrabarTrabajador(Operacion operacion, TrabajadorEntity trabajadorEntity, int userID)
        {
            Result result;

            switch (operacion)
            {
                case Operacion.Registrar:

                    USP_I_RegistrarTrabajador grabarDocente = new USP_I_RegistrarTrabajador()
                    {
                        C_TrabajadorCod = trabajadorEntity.C_TrabajadorCod,
                        T_ApellidoPaterno = trabajadorEntity.T_ApellidoPaterno,
                        T_ApellidoMaterno = trabajadorEntity.T_ApellidoMaterno,
                        T_Nombre = trabajadorEntity.T_Nombre,
                        I_TipoDocumentoID = trabajadorEntity.I_TipoDocumentoID,
                        C_NumDocumento = trabajadorEntity.C_NumDocumento,
                        D_FechaIngreso = trabajadorEntity.D_FechaIngreso,
                        I_RegimenID = trabajadorEntity.I_RegimenID,
                        I_EstadoID = trabajadorEntity.I_EstadoID,
                        I_VinculoID = trabajadorEntity.I_VinculoID,
                        I_BancoID = trabajadorEntity.I_BancoID,
                        T_NroCuentaBancaria = trabajadorEntity.T_NroCuentaBancaria,
                        I_DependenciaID = trabajadorEntity.I_DependenciaID,
                        I_AfpID = trabajadorEntity.I_Afp,
                        T_Cuspp = trabajadorEntity.T_Cuspp,
                        I_CategoriaDocenteID = trabajadorEntity.I_CategoriaDocenteID,
                        I_HorasDocenteID = trabajadorEntity.I_HorasDocenteID,
                        I_GrupoOcupacionalID = trabajadorEntity.I_GrupoOcupacionalID,
                        I_NivelRemunerativoID = trabajadorEntity.I_NivelRemunerativoID,
                        I_UserID = userID
                    };

                    result = grabarDocente.Execute();

                    break;

                case Operacion.Actualizar:

                    USP_I_ActualizarTrabajador actualizarDocente = new USP_I_ActualizarTrabajador()
                    {
                        I_TrabajadorID = trabajadorEntity.I_TrabajadorID.Value,
                        C_TrabajadorCod = trabajadorEntity.C_TrabajadorCod,
                        T_ApellidoPaterno = trabajadorEntity.T_ApellidoPaterno,
                        T_ApellidoMaterno = trabajadorEntity.T_ApellidoMaterno,
                        T_Nombre = trabajadorEntity.T_Nombre,
                        I_TipoDocumentoID = trabajadorEntity.I_TipoDocumentoID,
                        C_NumDocumento = trabajadorEntity.C_NumDocumento,
                        D_FechaIngreso = trabajadorEntity.D_FechaIngreso,
                        I_RegimenID = trabajadorEntity.I_RegimenID,
                        I_EstadoID = trabajadorEntity.I_EstadoID,
                        I_VinculoID = trabajadorEntity.I_VinculoID,
                        I_BancoID = trabajadorEntity.I_BancoID,
                        T_NroCuentaBancaria = trabajadorEntity.T_NroCuentaBancaria,
                        I_DependenciaID = trabajadorEntity.I_DependenciaID,
                        I_AfpID = trabajadorEntity.I_Afp,
                        T_Cuspp = trabajadorEntity.T_Cuspp,
                        I_CategoriaDocenteID = trabajadorEntity.I_CategoriaDocenteID,
                        I_HorasDocenteID = trabajadorEntity.I_HorasDocenteID,
                        I_GrupoOcupacionalID = trabajadorEntity.I_GrupoOcupacionalID,
                        I_NivelRemunerativoID = trabajadorEntity.I_NivelRemunerativoID,
                        I_UserID = userID
                    };

                    result = actualizarDocente.Execute();

                    break;

                default:
                    result = new Result()
                    {
                        Message = "No se reconoce la operación a realizar."
                    };

                    break;
            }

            

            return Mapper.Result_To_Response(result);
        }
    }
}
