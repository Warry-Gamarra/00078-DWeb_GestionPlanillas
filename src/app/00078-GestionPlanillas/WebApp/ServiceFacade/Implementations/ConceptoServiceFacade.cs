using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class ConceptoServiceFacade : IConceptoServiceFacade
    {
        private IConceptoService _conceptoService;

        public ConceptoServiceFacade()
        {
            _conceptoService = new ConceptoService();
        }

        public Response GrabarConcepto(Operacion operacion, ConceptoModel model, int userID)
        {
            Response response;

            try
            {
                var conceptoEntity = new ConceptoEntity()
                {
                    conceptoID = model.conceptoID,
                    tipoConceptoID = model.tipoConceptoID,
                    conceptoCod = model.conceptoCod.Trim(),
                    conceptoDesc = model.conceptoDesc.Trim(),
                    conceptoAbrv = model.conceptoAbrv.Trim()
                };

                response = _conceptoService.GrabarConcepto(operacion, conceptoEntity, userID);
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

        public List<ConceptoModel> ListarConceptos()
        {
            var lista = _conceptoService.ListarConceptos(true)
                .Select(x => Mapper.ConceptoDTO_To_ConceptoModel(x))
                .ToList();

            return lista;
        }

        public SelectList ListarConceptos(bool incluirDeshabilitados)
        {
            var lista = _conceptoService.ListarConceptos(incluirDeshabilitados);

            var result = new List<SelectListItem>();

            lista.ForEach(x => {
                var item = new SelectListItem()
                {
                    Value = x.conceptoID.ToString(),
                    Text = String.Format("{0} - {1}{2}", x.conceptoCod, x.conceptoDesc.ToString(), 
                        (x.conceptoAbrv != null && x.conceptoAbrv.Length > 0 ? " (" + x.conceptoAbrv + ")" : "" ))
                };

                result.Add(item);
            });

            return new SelectList(result, "Value", "Text");
        }

        public ConceptoModel ObtenerConcepto(int conceptoID)
        {
            ConceptoModel conceptoModel;

            var conceptoDTO = _conceptoService.ObtenerConcepto(conceptoID);

            if (conceptoDTO == null)
            {
                conceptoModel = null;
            }
            else
            {
                conceptoModel = Mapper.ConceptoDTO_To_ConceptoModel(conceptoDTO);
            }

            return conceptoModel;
        }

        public Response CambiarEstado(int conceptoID, bool estaHabilitado, int userID)
        {
            var result = _conceptoService.CambiarEstado(conceptoID, estaHabilitado, userID);

            return result;
        }
    }
}