using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class DepActividadMetaServiceFacade : IDepActividadMetaServiceFacade
    {
        private IDepActividadMetaService _depActividadMetaService;

        public DepActividadMetaServiceFacade()
        {
            _depActividadMetaService = new DepActividadMetaService();
        }

        public Response GrabarDepActividadMeta(Operacion operacion, DepActividadMetaModel model, int userID)
        {
            Response response;

            try
            {
                var depActividadMetaEntity = new DepActividadMetaEntity()
                {
                    depActividadMetaID = model.depActividadMetaID,
                    anio = model.anio,
                    categoriaPlanillaID = model.categoriaPlanillaID,
                    dependenciaID = model.dependenciaID,
                    descripcion = model.descripcion,
                    actividadID = model.actividadID,
                    metaID = model.metaID,
                    categoriaPresupuestalID = model.categoriaPresupuestalID
                };

                response = _depActividadMetaService.GrabarDepActividadMeta(operacion, depActividadMetaEntity, userID);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message
                };
            }

            return response;
        }

        public List<DepActividadMetaModel> ListarDepActividadMetas()
        {
            var lista = _depActividadMetaService.ListarDepActividadMetas()
                .Select(x => Mapper.DepActividadMetaDTO_To_DepActividadMetaModel(x))
                .ToList();

            return lista;
        }

        public DepActividadMetaModel ObtenerDepActividadMeta(int depActividadMetaID)
        {
            DepActividadMetaModel depActividadMetaModel;

            var depActividadMetaDTO = _depActividadMetaService.ObtenerDepActividadMeta(depActividadMetaID);

            if (depActividadMetaDTO == null)
            {
                depActividadMetaModel = null;
            }
            else
            {
                depActividadMetaModel = Mapper.DepActividadMetaDTO_To_DepActividadMetaModel(depActividadMetaDTO);
            }

            return depActividadMetaModel;
        }

        public Response Eliminar(int depActividadMetaID, int userID)
        {
            var result = _depActividadMetaService.Eliminar(depActividadMetaID, userID);

            return result;
        }
    }
}