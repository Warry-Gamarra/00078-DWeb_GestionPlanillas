using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class MetaServiceFacade : IMetaServiceFacade
    {
        private IMetaService _metaService;

        public MetaServiceFacade()
        {
            _metaService = new MetaService();
        }

        public Response GrabarMeta(Operacion operacion, MetaModel model, int userID)
        {
            Response response;

            try
            {
                var metaEntity = new MetaEntity()
                {
                    metaID = model.metaID,
                    metaCod = model.metaCod.Trim(),
                    metaDesc = model.metaDesc.Trim()
                };

                response = _metaService.GrabarMeta(operacion, metaEntity, userID);
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

        public List<MetaModel> ListarMetas()
        {
            var lista = _metaService.ListarMetas()
                .Select(x => Mapper.MetaDTO_To_MetaModel(x))
                .ToList();

            return lista;
        }

        public SelectList ObtenerComboMetas(int? selectedItem = null)
        {
            var lista = _metaService.ListarMetas();

            var result = new List<SelectListItem>();

            lista.ForEach(x => {
                var item = new SelectListItem()
                {
                    Value = x.metaID.ToString(),
                    Text = String.Format("{0} {1}", x.metaCod, x.metaDesc)
                };

                result.Add(item);
            });

            if (selectedItem.HasValue)
            {
                return new SelectList(result, "Value", "Text", selectedItem.Value);
            }
            else
            {
                return new SelectList(result, "Value", "Text");
            }
        }

        public MetaModel ObtenerMeta(int metaID)
        {
            MetaModel metaModel;

            var metaDTO = _metaService.ObtenerMeta(metaID);

            if (metaDTO == null)
            {
                metaModel = null;
            }
            else
            {
                metaModel = Mapper.MetaDTO_To_MetaModel(metaDTO);
            }

            return metaModel;
        }

        public Response Eliminar(int metaID, int userID)
        {
            var result = _metaService.Eliminar(metaID, userID);

            return result;
        }
    }
}