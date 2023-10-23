using Data.Connection;
using Data.Procedures;
using Data.Tables;
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
    public class MetaService : IMetaService
    {
        public Response GrabarMeta(Operacion operacion, MetaEntity metaEntity, int userID)
        {
            Result result;
            bool esCodigoMetaUnico = true;

            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:

                        if (TC_Meta.FindByCod(metaEntity.metaCod) != null)
                        {
                            esCodigoMetaUnico = false;
                        }

                        if (esCodigoMetaUnico)
                        {
                            var grabarMeta = new USP_I_RegistrarMeta()
                            {
                                C_MetaCod = metaEntity.metaCod,
                                T_MetaDesc = metaEntity.metaDesc,
                                I_UserID = userID
                            };

                            result = grabarMeta.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = String.Format("El código \"{0}\" se encuentra repetido en el sistema.", metaEntity.metaCod)
                            };
                        }

                        break;

                    case Operacion.Actualizar:

                        if (!metaEntity.metaID.HasValue)
                        {
                            throw new Exception("Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.");
                        }

                        var metaDTO = TC_Meta.FindAll()
                            .Where(x =>
                                x.I_MetaID != metaEntity.metaID.Value &&
                                x.C_MetaCod == metaEntity.metaCod)
                            .FirstOrDefault();

                        if (metaDTO != null)
                        {
                            esCodigoMetaUnico = false;
                        }

                        if (esCodigoMetaUnico)
                        {
                            var actualizarMeta = new USP_U_ActualizarMeta()
                            {
                                I_MetaID = metaEntity.metaID.Value,
                                C_MetaCod = metaEntity.metaCod,
                                T_MetaDesc = metaEntity.metaDesc,
                                I_UserID = userID
                            };

                            result = actualizarMeta.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = String.Format("El código \"{0}\" se encuentra repetido en el sistema.", metaEntity.metaCod)
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

        public List<MetaDTO> ListarMetas()
        {
            var lista = TC_Meta.FindAll();

            var result = lista
                .Select(x => Mapper.TC_Meta_To_MetaDTO(x))
                .OrderBy(x => x.metaDesc)
                .ToList();

            return result;
        }

        public MetaDTO ObtenerMeta(int metaID)
        {
            MetaDTO metaDTO;

            var table = TC_Meta.FindByID(metaID);

            if (table == null)
            {
                metaDTO = null;
            }
            else
            {
                metaDTO = Mapper.TC_Meta_To_MetaDTO(table);
            }

            return metaDTO;
        }

        public Response Eliminar(int metaID, int userID)
        {
            Result result;

            try
            {
                var meta = TC_Meta.FindByID(metaID);

                if (meta != null)
                {
                    var eliminar = new USP_U_EliminarMeta()
                    {
                        I_MetaID = metaID,
                        I_UserID = userID
                    };

                    result = eliminar.Execute();
                }
                else
                {
                    result = new Result()
                    {
                        Success = true,
                        Message = "La meta seleccionada ha sido eliminada con anterioridad."
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
