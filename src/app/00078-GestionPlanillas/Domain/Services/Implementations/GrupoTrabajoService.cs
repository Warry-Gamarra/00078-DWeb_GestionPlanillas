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
    public class GrupoTrabajoService : IGrupoTrabajoService
    {
        public Response GrabarGrupoTrabajo(Operacion operacion, GrupoTrabajoEntity grupoTrabajoEntity, int userID)
        {
            Result result;
            bool esCodigoGrupoTrabajoUnico = true;

            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:

                        if (TC_GrupoTrabajo.FindByCod(grupoTrabajoEntity.grupoTrabajoCod) != null)
                        {
                            esCodigoGrupoTrabajoUnico = false;
                        }

                        if (esCodigoGrupoTrabajoUnico)
                        {
                            var grabarGrupoTrabajo = new USP_I_RegistrarGrupoTrabajo()
                            {
                                C_GrupoTrabajoCod = grupoTrabajoEntity.grupoTrabajoCod,
                                T_GrupoTrabajoDesc = grupoTrabajoEntity.grupoTrabajoDesc,
                                I_UserID = userID
                            };

                            result = grabarGrupoTrabajo.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = String.Format("El código \"{0}\" se encuentra repetido en el sistema.", grupoTrabajoEntity.grupoTrabajoCod)
                            };
                        }

                        break;

                    case Operacion.Actualizar:

                        if (!grupoTrabajoEntity.grupoTrabajoID.HasValue)
                        {
                            throw new Exception("Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.");
                        }

                        var grupoTrabajo = TC_GrupoTrabajo.FindAll()
                            .Where(x =>
                                x.I_GrupoTrabajoID != grupoTrabajoEntity.grupoTrabajoID.Value &&
                                x.C_GrupoTrabajoCod == grupoTrabajoEntity.grupoTrabajoCod)
                            .FirstOrDefault();

                        if (grupoTrabajo != null)
                        {
                            esCodigoGrupoTrabajoUnico = false;
                        }

                        if (esCodigoGrupoTrabajoUnico)
                        {
                            var actualizarGrupoTrabajo = new USP_U_ActualizarGrupoTrabajo()
                            {
                                I_GrupoTrabajoID = grupoTrabajoEntity.grupoTrabajoID.Value,
                                C_GrupoTrabajoCod = grupoTrabajoEntity.grupoTrabajoCod,
                                T_GrupoTrabajoDesc = grupoTrabajoEntity.grupoTrabajoDesc,
                                I_UserID = userID
                            };

                            result = actualizarGrupoTrabajo.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = String.Format("El código \"{0}\" se encuentra repetido en el sistema.", grupoTrabajoEntity.grupoTrabajoCod)
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

        public List<GrupoTrabajoDTO> ListarGruposTrabajo(bool incluirDeshabilitados = false)
        {
            var lista = TC_GrupoTrabajo.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_GrupoTrabajo_To_GrupoTrabajoDTO(x))
                .ToList();

            return result;
        }

        public GrupoTrabajoDTO ObtenerGrupoTrabajo(int grupoTrabajoID)
        {
            GrupoTrabajoDTO grupoTrabajoDTO;

            var table = TC_GrupoTrabajo.FindByID(grupoTrabajoID);

            if (table == null)
            {
                grupoTrabajoDTO = null;
            }
            else
            {
                grupoTrabajoDTO = Mapper.TC_GrupoTrabajo_To_GrupoTrabajoDTO(table);
            }

            return grupoTrabajoDTO;
        }

        public Response CambiarEstado(int grupoTrabajoID, bool estaHabilitado, int userID)
        {
            Result result;

            try
            {
                var cambiarEstado = new USP_U_CambiarEstadoGrupoTrabajo()
                {
                    I_GrupoTrabajoID = grupoTrabajoID,
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

        public Response Eliminar(int grupoTrabajoID, int userID)
        {
            Result result;

            try
            {
                var grupoTrabajo = TC_GrupoTrabajo.FindByID(grupoTrabajoID);

                if (grupoTrabajo != null)
                {
                    if (!TR_TrabajadorPlanilla.ExisteGrupoTrabajo(grupoTrabajoID))
                    {
                        var eliminar = new USP_U_EliminarGrupoTrabajo()
                        {
                            I_GrupoTrabajoID = grupoTrabajoID,
                            I_UserID = userID
                        };

                        result = eliminar.Execute();
                    }
                    else
                    {
                        result = new Result()
                        {
                            Message = String.Format("El registro seleccionado \"{0} - {1}\" no se puede eliminar.", 
                                grupoTrabajo.C_GrupoTrabajoCod, grupoTrabajo.T_GrupoTrabajoDesc)
                        };
                    }
                }
                else
                {
                    result = new Result()
                    {
                        Success = true,
                        Message = "El registro seleccionado ha sido eliminado con anterioridad."
                    };
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
    }
}
