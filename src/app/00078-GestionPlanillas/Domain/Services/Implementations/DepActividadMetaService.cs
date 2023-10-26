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
    public class DepActividadMetaService : IDepActividadMetaService
    {
        public Response GrabarDepActividadMeta(Operacion operacion, DepActividadMetaEntity depActividadMetaEntity, int userID)
        {
            Result result;

            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:
                        if (!VW_DepActividadMeta.IsDuplicate(null, depActividadMetaEntity.anio, depActividadMetaEntity.categoriaPlanillaID, 
                            depActividadMetaEntity.dependenciaID, depActividadMetaEntity.actividadID, depActividadMetaEntity.metaID))
                        {
                            var grabarDepActividadMeta = new USP_I_RegistrarDepActividadMeta()
                            {
                                I_Anio = depActividadMetaEntity.anio,
                                I_CategoriaPlanillaID = depActividadMetaEntity.categoriaPlanillaID,
                                I_DependenciaID = depActividadMetaEntity.dependenciaID,
                                T_Descripcion = depActividadMetaEntity.descripcion,
                                I_ActividadID = depActividadMetaEntity.actividadID,
                                I_MetaID = depActividadMetaEntity.metaID,
                                I_CategoriaPresupuestalID = depActividadMetaEntity.categoriaPresupuestalID,
                                I_UserID = userID
                            };

                            result = grabarDepActividadMeta.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = "Registro duplicado"
                            };
                        }

                        break;

                    case Operacion.Actualizar:

                        if (!depActividadMetaEntity.depActividadMetaID.HasValue)
                        {
                            throw new Exception("Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.");
                        }

                        if (!VW_DepActividadMeta.IsDuplicate(depActividadMetaEntity.depActividadMetaID.Value, depActividadMetaEntity.anio, depActividadMetaEntity.categoriaPlanillaID,
                            depActividadMetaEntity.dependenciaID, depActividadMetaEntity.actividadID, depActividadMetaEntity.metaID))
                        {
                            var actualizarDepActividadMeta = new USP_U_ActualizarDepActividadMeta()
                            {
                                I_DepActividadMetaID = depActividadMetaEntity.depActividadMetaID.Value,
                                I_Anio = depActividadMetaEntity.anio,
                                I_CategoriaPlanillaID = depActividadMetaEntity.categoriaPlanillaID,
                                I_DependenciaID = depActividadMetaEntity.dependenciaID,
                                T_Descripcion = depActividadMetaEntity.descripcion,
                                I_ActividadID = depActividadMetaEntity.actividadID,
                                I_MetaID = depActividadMetaEntity.metaID,
                                I_CategoriaPresupuestalID = depActividadMetaEntity.categoriaPresupuestalID,
                                I_UserID = userID
                            };

                            result = actualizarDepActividadMeta.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = "Registro duplicado"
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

        public List<DepActividadMetaDTO> ListarDepActividadMetas()
        {
            var lista = VW_DepActividadMeta.FindAll();

            var result = lista
                .Select(x => Mapper.VW_DepActividadMeta_To_DepActividadMetaDTO(x))
                .OrderBy(x => x.actividadCod)
                .ThenBy(x => x.metaCod)
                .ToList();

            return result;
        }

        public DepActividadMetaDTO ObtenerDepActividadMeta(int depActividadMetaID)
        {
            DepActividadMetaDTO depActividadMetaDTO;

            var view = VW_DepActividadMeta.FindByID(depActividadMetaID);

            if (view == null)
            {
                depActividadMetaDTO = null;
            }
            else
            {
                depActividadMetaDTO = Mapper.VW_DepActividadMeta_To_DepActividadMetaDTO(view);
            }

            return depActividadMetaDTO;
        }

        public Response Eliminar(int depActividadMetaID, int userID)
        {
            Result result;

            try
            {
                var depActividadMeta = VW_DepActividadMeta.FindByID(depActividadMetaID);

                if (depActividadMeta != null)
                {
                    var eliminar = new USP_D_EliminarDepActividadMeta()
                    {
                        I_DepActividadMetaID = depActividadMetaID,
                        I_UserID = userID
                    };

                    result = eliminar.Execute();
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
