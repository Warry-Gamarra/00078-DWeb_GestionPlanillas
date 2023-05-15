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
            bool grabar = true;

            try
            {
                var conceptoDTO = _conceptoService.ListarConceptos().Where(x => x.C_ConceptoCod == model.C_ConceptoCod).FirstOrDefault();

                if (operacion.Equals(Operacion.Registrar) && conceptoDTO != null)
                {
                    grabar = false;
                }

                if (operacion.Equals(Operacion.Actualizar))
                {
                    if (conceptoDTO != null && conceptoDTO.I_ConceptoID != model.I_ConceptoID.Value)
                    {
                        grabar = false;
                    }
                }

                if (grabar)
                {
                    var conceptoEntity = new ConceptoEntity()
                    {
                        I_ConceptoID = model.I_ConceptoID,
                        I_TipoConceptoID = model.I_TipoConceptoID,
                        C_ConceptoCod = model.C_ConceptoCod,
                        T_ConceptoDesc = model.T_ConceptoDesc
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
            var lista = _conceptoService.ListarConceptos()
                .Select(x => Mapper.ConceptoDTO_To_ConceptoModel(x))
                .ToList();

            return lista;
        }

        public ConceptoModel ObtenerConcepto(int I_ConceptoID)
        {
            ConceptoModel conceptoModel;

            var conceptoDTO = _conceptoService.ObtenerConcepto(I_ConceptoID);

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
    }
}