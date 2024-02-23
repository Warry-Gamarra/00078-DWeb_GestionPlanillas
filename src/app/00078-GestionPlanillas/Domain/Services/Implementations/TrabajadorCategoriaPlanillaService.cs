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
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class TrabajadorCategoriaPlanillaService : ITrabajadorCategoriaPlanillaService
    {
        public Response GrabarTrabajadorCategoriaPlanilla(Operacion operacion, TrabajadorCategoriaPlanillaEntity entity, int userID)
        {
            Result result;

            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:

                        var grabarTrabajadorCategoriaPlanilla = new USP_I_RegistrarTrabajadorCategoriaPlanilla()
                        {
                            I_TrabajadorID = entity.trabajadorID,
                            I_CategoriaPlanillaID = entity.categoriaPlanillaID,
                            I_DependenciaID = entity.dependenciaID,
                            I_GrupoTrabajoID = entity.grupoTrabajoID,
                            B_EsJefe = entity.esJefe,
                            I_UserID = userID
                        };

                        result = grabarTrabajadorCategoriaPlanilla.Execute();

                        break;

                    case Operacion.Actualizar:

                        if (!entity.trabajadorCategoriaPlanillaID.HasValue)
                        {
                            throw new Exception("Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.");
                        }

                        var actualizarTrabajadorCategoriaPlanilla = new USP_U_ActualizarTrabajadorCategoriaPlanilla()
                        {
                            I_TrabajadorCategoriaPlanillaID = entity.trabajadorCategoriaPlanillaID.Value,
                            I_CategoriaPlanillaID = entity.categoriaPlanillaID,
                            I_DependenciaID = entity.dependenciaID,
                            I_GrupoTrabajoID = entity.grupoTrabajoID,
                            B_EsJefe = entity.esJefe,
                            I_UserID = userID
                        };

                        result = actualizarTrabajadorCategoriaPlanilla.Execute();

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

        public List<TrabajadorCategoriaPlanillaDTO> ListarCategoriaPlanillaPorTrabajador(int trabajadorID)
        {
            var lista = VW_TrabajadoresCategoriaPlanilla.FindByTrabajadorID(trabajadorID)
                .Select(x => Mapper.VW_TrabajadoresCategoriaPlanilla_To_TrabajadorCategoriaPlanillaDTO(x))
                .ToList();

            return lista;
        }

        public List<TrabajadorCategoriaPlanillaDTO> ListarTrabajadoresCategoriaPlanilla(int? categoriaPlanillaID = null)
        {
            var lista = VW_TrabajadoresCategoriaPlanilla.FindByFilters(categoriaPlanillaID)
                .Select(x => Mapper.VW_TrabajadoresCategoriaPlanilla_To_TrabajadorCategoriaPlanillaDTO(x))
                .ToList();

            return lista;
        }

        public TrabajadorCategoriaPlanillaDTO ObtenerTrabajadorCategoriaPlanilla(int trabajadorCategoriaPlanillaID)
        {
            TrabajadorCategoriaPlanillaDTO trabajadorCategoriaPlanillaDTO;

            var view = VW_TrabajadoresCategoriaPlanilla.FindByID(trabajadorCategoriaPlanillaID);

            if (view == null)
            {
                trabajadorCategoriaPlanillaDTO = null;
            }
            else
            {
                trabajadorCategoriaPlanillaDTO = Mapper.VW_TrabajadoresCategoriaPlanilla_To_TrabajadorCategoriaPlanillaDTO(view);
            }

            return trabajadorCategoriaPlanillaDTO;
        }

        public TrabajadorCategoriaPlanillaDTO ObtenerTrabajadorPorDocumentoYCategoria(int tipoDocumentoID, string numDocumento, int categoriaPlanillaID)
        {
            TrabajadorCategoriaPlanillaDTO trabajadorCategoriaPlanillaDTO;

            var view = VW_TrabajadoresCategoriaPlanilla.FindByDocumentoYCategoria(tipoDocumentoID, numDocumento, categoriaPlanillaID);

            if (view == null)
            {
                trabajadorCategoriaPlanillaDTO = null;
            }
            else
            {
                trabajadorCategoriaPlanillaDTO = Mapper.VW_TrabajadoresCategoriaPlanilla_To_TrabajadorCategoriaPlanillaDTO(view);
            }

            return trabajadorCategoriaPlanillaDTO;
        }

        public Response CambiarEstado(int trabajadorCategoriaPlanillaID, bool estaHabilitado, int userID)
        {
            Result result;

            try
            {
                var cambiarEstado = new USP_U_CambiarEstadoTrabajadorCategoriaPlanilla()
                {
                    I_TrabajadorCategoriaPlanillaID = trabajadorCategoriaPlanillaID,
                    B_Habilitado = !estaHabilitado,
                    I_UserID = userID
                };

                result = cambiarEstado.Execute();
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

        public Response Eliminar(int trabajadorCategoriaPlanillaID, int userID)
        {
            Result result;

            try
            {
                var view = VW_TrabajadoresCategoriaPlanilla.FindByID(trabajadorCategoriaPlanillaID);

                if (view == null)
                {
                    result = new Result()
                    {
                        Message = "El registro seleccionado no está disponible."
                    };
                }
                else if(TR_TrabajadorPlanilla.ExistePlanillaTrabajador(view.I_TrabajadorID, view.I_CategoriaPlanillaID))
                {
                    result = new Result()
                    {
                        Message = "No se puede eliminar el registro porque ya existe una planilla generada."
                    };
                }
                else
                {
                    var eliminar = new USP_U_EliminarTrabajadorCategoriaPlanilla()
                    {
                        I_TrabajadorCategoriaPlanillaID = trabajadorCategoriaPlanillaID,
                        I_UserID = userID
                    };

                    result = eliminar.Execute();
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

        public CategoriaPlanilla ObtenerCategoriaPlanillaSegunVinculo(int vinculoID)
        {
            CategoriaPlanilla categoriaPlanilla;

            switch (vinculoID)
            {
                case 1:
                    categoriaPlanilla = CategoriaPlanilla.HaberesAdministrativo;
                    break;

                case 2:
                    categoriaPlanilla = CategoriaPlanilla.HaberesAdministrativo;
                    break;

                case 3:
                    categoriaPlanilla = CategoriaPlanilla.HaberesMedico;
                    break;

                case 4:
                    categoriaPlanilla = CategoriaPlanilla.HaberesDocente;
                    break;

                case 5:
                    categoriaPlanilla = CategoriaPlanilla.HaberesDocente;
                    break;

                case 6:
                    categoriaPlanilla = CategoriaPlanilla.Pensiones;
                    break;

                case 7:
                    categoriaPlanilla = CategoriaPlanilla.Pensiones;
                    break;

                case 9:
                    categoriaPlanilla = CategoriaPlanilla.Practicante;
                    break;

                default:
                    throw new NotImplementedException("Acción no implementada.");
            }

            return categoriaPlanilla;
        }
    }
}
