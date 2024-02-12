using Data.Connection;
using Data.Procedures;
using Data.Tables;
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
        private ITrabajadorCategoriaPlanillaService _trabajadorCategoriaPlanillaService;

        public TrabajadorService()
        {
            _trabajadorCategoriaPlanillaService = new TrabajadorCategoriaPlanillaService();
        }

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
            bool registroDuplicado = false;
            int categoriaPlanillaID;
            TC_Persona persona;
            VW_TrabajadoresCategoriaPlanilla trabajadoresCategoriaPlanilla;
            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:

                        categoriaPlanillaID = (int)_trabajadorCategoriaPlanillaService.ObtenerCategoriaPlanillaSegunVinculo(trabajadorEntity.vinculoID);

                        persona = TC_Persona.FindByNumDocumento(trabajadorEntity.tipoDocumentoID, trabajadorEntity.numDocumento);

                        trabajadoresCategoriaPlanilla = VW_TrabajadoresCategoriaPlanilla.FindByDocumentoYCategoria(
                            trabajadorEntity.tipoDocumentoID, trabajadorEntity.numDocumento, categoriaPlanillaID);

                        if (trabajadoresCategoriaPlanilla != null)
                        {
                            registroDuplicado = true;
                        }

                        if (!registroDuplicado)
                        {
                            var grabarDocente = new USP_I_RegistrarTrabajador()
                            {
                                C_TrabajadorCod = trabajadorEntity.trabajadorCod,
                                I_PersonaID = (persona != null) ? persona.I_PersonaID : 0,
                                T_ApellidoPaterno = trabajadorEntity.apellidoPaterno,
                                T_ApellidoMaterno = trabajadorEntity.apellidoMaterno,
                                T_Nombre = trabajadorEntity.nombre,
                                I_TipoDocumentoID = trabajadorEntity.tipoDocumentoID,
                                C_NumDocumento = trabajadorEntity.numDocumento,
                                D_FechaIngreso = trabajadorEntity.fechaIngreso,
                                I_RegimenID = trabajadorEntity.regimenID,
                                I_EstadoID = trabajadorEntity.estadoID,
                                I_VinculoID = trabajadorEntity.vinculoID,
                                I_BancoID = trabajadorEntity.bancoID,
                                T_NroCuentaBancaria = trabajadorEntity.nroCuentaBancaria,
                                I_DependenciaID = trabajadorEntity.dependenciaID,
                                I_AfpID = trabajadorEntity.afp,
                                T_Cuspp = trabajadorEntity.cuspp,
                                I_CategoriaDocenteID = trabajadorEntity.categoriaDocenteID,
                                I_HorasDocenteID = trabajadorEntity.horasDocenteID,
                                I_GrupoOcupacionalID = trabajadorEntity.grupoOcupacionalID,
                                I_NivelRemunerativoID = trabajadorEntity.nivelRemunerativoID,
                                I_CategoriaPlanillaID = categoriaPlanillaID,
                                I_UserID = userID
                            };

                            result = grabarDocente.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = "El Num.Documento se encuentra repetido."
                            };
                        }

                        break;

                    case Operacion.Actualizar:

                        if (!trabajadorEntity.trabajadorID.HasValue)
                        {
                            throw new Exception("Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.");
                        }

                        categoriaPlanillaID = (int)_trabajadorCategoriaPlanillaService.ObtenerCategoriaPlanillaSegunVinculo(trabajadorEntity.vinculoID);

                        trabajadoresCategoriaPlanilla = VW_TrabajadoresCategoriaPlanilla.FindByDocumentoYCategoria(
                            trabajadorEntity.tipoDocumentoID, trabajadorEntity.numDocumento, categoriaPlanillaID);

                        if (trabajadoresCategoriaPlanilla != null && trabajadoresCategoriaPlanilla.I_TrabajadorID != trabajadorEntity.trabajadorID.Value)
                        {
                            registroDuplicado = true;
                        }

                        if (!registroDuplicado)
                        {
                            var actualizarDocente = new USP_U_ActualizarTrabajador()
                            {
                                I_TrabajadorID = trabajadorEntity.trabajadorID.Value,
                                C_TrabajadorCod = trabajadorEntity.trabajadorCod,
                                T_ApellidoPaterno = trabajadorEntity.apellidoPaterno,
                                T_ApellidoMaterno = trabajadorEntity.apellidoMaterno,
                                T_Nombre = trabajadorEntity.nombre,
                                I_TipoDocumentoID = trabajadorEntity.tipoDocumentoID,
                                C_NumDocumento = trabajadorEntity.numDocumento,
                                D_FechaIngreso = trabajadorEntity.fechaIngreso,
                                I_RegimenID = trabajadorEntity.regimenID,
                                I_EstadoID = trabajadorEntity.estadoID,
                                I_VinculoID = trabajadorEntity.vinculoID,
                                I_BancoID = trabajadorEntity.bancoID,
                                T_NroCuentaBancaria = trabajadorEntity.nroCuentaBancaria,
                                I_DependenciaID = trabajadorEntity.dependenciaID,
                                I_AfpID = trabajadorEntity.afp,
                                T_Cuspp = trabajadorEntity.cuspp,
                                I_CategoriaDocenteID = trabajadorEntity.categoriaDocenteID,
                                I_HorasDocenteID = trabajadorEntity.horasDocenteID,
                                I_GrupoOcupacionalID = trabajadorEntity.grupoOcupacionalID,
                                I_NivelRemunerativoID = trabajadorEntity.nivelRemunerativoID,
                                I_CategoriaPlanillaID = categoriaPlanillaID,
                                I_UserID = userID
                            };

                            result = actualizarDocente.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = "El Num.Documento se encuentra repetido."
                            };
                        }

                        break;

                    default:
                        result = new Result()
                        {
                            Message = "No se reconoce la operación a realizar."
                        };

                        break;
                }
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    Message = ex.Message
                };
            }

            return Mapper.Result_To_Response(result);
        }

        public List<TrabajadorConPlanillaDTO> ListarTrabajadoresConPlanilla(int año, int mes)
        {
            var lista = USP_S_ListarTrabajadoresConPlanilla.Execute(año, mes)
                .Select(x => Mapper.USP_S_ListarTrabajadoresConPlanilla_To_TrabajadorConPlanillaDTO(x))
                .ToList();

            return lista;
        }
    }
}
