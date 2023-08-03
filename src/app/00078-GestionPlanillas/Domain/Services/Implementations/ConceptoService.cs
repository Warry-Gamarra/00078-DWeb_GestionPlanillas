using Data.Connection;
using Data.Procedures;
using Data.Views;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class ConceptoService : IConceptoService
    {
        public Response GrabarConcepto(Operacion operacion, ConceptoEntity conceptoEntity, int userID)
        {
            Result result;
            bool esCodigoConceptoUnico = true;

            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:
                        
                        if (ListarConceptos().Where(x => x.conceptoCod == conceptoEntity.conceptoCod).FirstOrDefault() != null)
                        {
                            esCodigoConceptoUnico = false;
                        }

                        if (esCodigoConceptoUnico)
                        {
                            var grabarConcepto = new USP_I_RegistrarConcepto()
                            {
                                I_TipoConceptoID = conceptoEntity.tipoConceptoID,
                                C_ConceptoCod = conceptoEntity.conceptoCod,
                                T_ConceptoDesc = conceptoEntity.conceptoDesc,
                                T_ConceptoAbrv = conceptoEntity.conceptoAbrv,
                                I_UserID = userID
                            };

                            result = grabarConcepto.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = String.Format("El código \"{0}\" se encuentra repetido en el sistema.", conceptoEntity.conceptoCod)
                            };
                        }

                        break;

                    case Operacion.Actualizar:

                        if (!conceptoEntity.conceptoID.HasValue)
                        {
                            throw new Exception("Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.");
                        }

                        var conceptoDTO = ListarConceptos()
                            .Where(x =>
                                x.conceptoID != conceptoEntity.conceptoID.Value &&
                                x.conceptoCod == conceptoEntity.conceptoCod)
                            .FirstOrDefault();

                        if (conceptoDTO != null)
                        {
                            esCodigoConceptoUnico = false;
                        }

                        if (esCodigoConceptoUnico)
                        {
                            var actualizarConcepto = new USP_U_ActualizarConcepto()
                            {
                                I_ConceptoID = conceptoEntity.conceptoID.Value,
                                I_TipoConceptoID = conceptoEntity.tipoConceptoID,
                                C_ConceptoCod = conceptoEntity.conceptoCod,
                                T_ConceptoDesc = conceptoEntity.conceptoDesc,
                                T_ConceptoAbrv = conceptoEntity.conceptoAbrv,
                                I_UserID = userID
                            };

                            result = actualizarConcepto.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = String.Format("El código \"{0}\" se encuentra repetido en el sistema.", conceptoEntity.conceptoCod)
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

        public List<ConceptoDTO> ListarConceptos(bool incluirDeshabilitados = false)
        {
            var lista = VW_Conceptos.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }
            
            var result = lista
                .Select(x => Mapper.VW_Conceptos_To_ConceptoDTO(x))
                .ToList();

            return result;
        }

        public ConceptoDTO ObtenerConcepto(int conceptoID)
        {
            ConceptoDTO conceptoDTO;

            var view = VW_Conceptos.FindByID(conceptoID);

            if (view == null)
            {
                conceptoDTO = null;
            }
            else
            {
                conceptoDTO = Mapper.VW_Conceptos_To_ConceptoDTO(view);
            }

            return conceptoDTO;
        }

        public ConceptoDTO ObtenerConcepto(string conceptoCod)
        {
            ConceptoDTO conceptoDTO;

            var view = VW_Conceptos.FindByCod(conceptoCod);

            if (view == null)
            {
                conceptoDTO = null;
            }
            else
            {
                conceptoDTO = Mapper.VW_Conceptos_To_ConceptoDTO(view);
            }

            return conceptoDTO;
        }

        public Response CambiarEstado(int conceptoID, bool estaHabilitado, int userID)
        {
            Result result;

            try
            {
                var cambiarEstado = new USP_U_CambiarEstadoConcepto()
                {
                    I_ConceptoID = conceptoID,
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
    }
}
