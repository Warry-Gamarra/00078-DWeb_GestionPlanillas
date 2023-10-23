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
    public class DependenciaService : IDependenciaService
    {
        public Response GrabarDependencia(Operacion operacion, DependenciaEntity dependenciaEntity, int userID)
        {
            Result result;
            bool esCodigoDependenciaUnico = true;

            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:

                        if (TC_Dependencia.FindByCod(dependenciaEntity.dependenciaCod) != null)
                        {
                            esCodigoDependenciaUnico = false;
                        }

                        if (esCodigoDependenciaUnico)
                        {
                            var grabarDependencia = new USP_I_RegistrarDependencia()
                            {
                                C_DependenciaCod = dependenciaEntity.dependenciaCod,
                                T_DependenciaDesc = dependenciaEntity.dependenciaDesc,
                                I_UserID = userID
                            };

                            result = grabarDependencia.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = String.Format("El código \"{0}\" se encuentra repetido en el sistema.", dependenciaEntity.dependenciaCod)
                            };
                        }

                        break;

                    case Operacion.Actualizar:

                        if (!dependenciaEntity.dependenciaID.HasValue)
                        {
                            throw new Exception("Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.");
                        }

                        var dependenciaDTO = ListarDependencias(true)
                            .Where(x =>
                                x.dependenciaID != dependenciaEntity.dependenciaID.Value &&
                                x.dependenciaCod == dependenciaEntity.dependenciaCod)
                            .FirstOrDefault();

                        if (dependenciaDTO != null)
                        {
                            esCodigoDependenciaUnico = false;
                        }

                        if (esCodigoDependenciaUnico)
                        {
                            var actualizarDependencia = new USP_U_ActualizarDependencia()
                            {
                                I_DependenciaID = dependenciaEntity.dependenciaID.Value,
                                C_DependenciaCod = dependenciaEntity.dependenciaCod,
                                T_DependenciaDesc = dependenciaEntity.dependenciaDesc,
                                I_UserID = userID
                            };

                            result = actualizarDependencia.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = String.Format("El código \"{0}\" se encuentra repetido en el sistema.", dependenciaEntity.dependenciaCod)
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

        public List<DependenciaDTO> ListarDependencias(bool incluirDeshabilitados = false)
        {
            var lista = TC_Dependencia.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_Dependencia_To_DependenciaDTO(x))
                .OrderBy(x => x.dependenciaDesc)
                .ToList();

            return result;
        }

        public DependenciaDTO ObtenerDependencia(int dependenciaID)
        {
            DependenciaDTO dependenciaDTO;

            var table = TC_Dependencia.FindByID(dependenciaID);

            if (table == null)
            {
                dependenciaDTO = null;
            }
            else
            {
                dependenciaDTO = Mapper.TC_Dependencia_To_DependenciaDTO(table);
            }

            return dependenciaDTO;
        }

        public Response CambiarEstado(int dependenciaID, bool estaHabilitado, int userID)
        {
            Result result;

            try
            {
                var cambiarEstado = new USP_U_CambiarEstadoDependencia()
                {
                    I_DependenciaID = dependenciaID,
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

        public Response Eliminar(int dependenciaID, int userID)
        {
            Result result;

            try
            {
                var periodo = TC_Dependencia.FindByID(dependenciaID);

                if (periodo != null)
                {
                    var existeTrabajador = TC_Trabajador_Dependencia.ExisteTrabajador(dependenciaID);

                    if (!existeTrabajador)
                    {
                        var eliminar = new USP_U_EliminarDependencia()
                        {
                            I_DependenciaID = dependenciaID,
                            I_UserID = userID
                        };

                        result = eliminar.Execute();
                    }
                    else
                    {
                        result = new Result()
                        {
                            Message = "Existen trabajadores relacionados con la dependencia seleccionada."
                        };
                    }
                }
                else
                {
                    result = new Result()
                    {
                        Success = true,
                        Message = "La dependencia seleccionada ha sido eliminado con anterioridad."
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
