using Data.Connection;
using Data.Procedures;
using Data.Tables;
using Data.Views;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
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
    public class ActividadService : IActividadService
    {
        public Response GrabarActividad(Operacion operacion, ActividadEntity actividadEntity, int userID)
        {
            Result result;
            bool esCodigoActividadUnico = true;

            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:

                        if (TC_Actividad.FindByCod(actividadEntity.actividadCod) != null)
                        {
                            esCodigoActividadUnico = false;
                        }

                        if (esCodigoActividadUnico)
                        {
                            var grabarActividad = new USP_I_RegistrarActividad()
                            {
                                C_ActividadCod = actividadEntity.actividadCod,
                                T_ActividadDesc = actividadEntity.actividadDesc,
                                I_UserID = userID
                            };

                            result = grabarActividad.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = String.Format("El código \"{0}\" se encuentra repetido en el sistema.", actividadEntity.actividadCod)
                            };
                        }

                        break;

                    case Operacion.Actualizar:

                        if (!actividadEntity.actividadID.HasValue)
                        {
                            throw new Exception("Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.");
                        }

                        var actividadDTO = TC_Actividad.FindAll()
                            .Where(x =>
                                x.I_ActividadID != actividadEntity.actividadID.Value &&
                                x.C_ActividadCod == actividadEntity.actividadCod)
                            .FirstOrDefault();

                        if (actividadDTO != null)
                        {
                            esCodigoActividadUnico = false;
                        }

                        if (esCodigoActividadUnico)
                        {
                            var actualizarActividad = new USP_U_ActualizarActividad()
                            {
                                I_ActividadID = actividadEntity.actividadID.Value,
                                C_ActividadCod = actividadEntity.actividadCod,
                                T_ActividadDesc = actividadEntity.actividadDesc,
                                I_UserID = userID
                            };

                            result = actualizarActividad.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = String.Format("El código \"{0}\" se encuentra repetido en el sistema.", actividadEntity.actividadCod)
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

        public List<ActividadDTO> ListarActividades()
        {
            var lista = TC_Actividad.FindAll();

            var result = lista
                .Select(x => Mapper.TC_Actividad_To_ActividadDTO(x))
                .OrderBy(x => x.actividadDesc)
                .ToList();

            return result;
        }

        public ActividadDTO ObtenerActividad(int actividadID)
        {
            ActividadDTO actividadDTO;

            var table = TC_Actividad.FindByID(actividadID);

            if (table == null)
            {
                actividadDTO = null;
            }
            else
            {
                actividadDTO = Mapper.TC_Actividad_To_ActividadDTO(table);
            }

            return actividadDTO;
        }

        public Response Eliminar(int actividadID, int userID)
        {
            Result result;

            try
            {
                var actividad = TC_Actividad.FindByID(actividadID);

                if (actividad != null)
                {
                    if (!VW_DepActividadMeta.existsActividad(actividadID))
                    {
                        var eliminar = new USP_U_EliminarActividad()
                        {
                            I_ActividadID = actividadID,
                            I_UserID = userID
                        };

                        result = eliminar.Execute();
                    }
                    else
                    {
                        result = new Result()
                        {
                            Message = "La actividad \"" + actividad.C_ActividadCod + "\" no se puede eliminar."
                        };
                    }
                }
                else
                {
                    result = new Result()
                    {
                        Success = true,
                        Message = "La actividad seleccionada ha sido eliminada con anterioridad."
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
