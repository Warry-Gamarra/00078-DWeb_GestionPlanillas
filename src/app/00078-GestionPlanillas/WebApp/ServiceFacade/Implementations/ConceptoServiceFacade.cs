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
            bool esCodigoConceptoUnico = true;

            try
            {
                var conceptoDTO = _conceptoService.ListarConceptos()
                    .Where(x => x.conceptoCod == model.conceptoCod)
                    .FirstOrDefault();

                if (operacion.Equals(Operacion.Registrar) && conceptoDTO != null)
                {
                    esCodigoConceptoUnico = false;
                }

                if (operacion.Equals(Operacion.Actualizar) && conceptoDTO != null && conceptoDTO.conceptoID != model.conceptoID.Value)
                {
                    esCodigoConceptoUnico = false;
                }

                if (esCodigoConceptoUnico)
                {
                    var conceptoEntity = new ConceptoEntity()
                    {
                        conceptoID = model.conceptoID,
                        tipoConceptoID = model.tipoConceptoID,
                        conceptoCod = model.conceptoCod,
                        conceptoDesc = model.conceptoDesc
                    };

                    response = _conceptoService.GrabarConcepto(operacion, conceptoEntity, userID);
                }
                else
                {
                    response = new Response()
                    {
                        Message = "Código de Concepto repetido."
                    };
                }
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

        public Response CambiarEstado(int conceptoID, bool estadHabilitado, int userID, string returnUrl)
        {
            var result = _conceptoService.CambiarEstado(conceptoID, estadHabilitado, userID);

            return result;
        }
    }
}