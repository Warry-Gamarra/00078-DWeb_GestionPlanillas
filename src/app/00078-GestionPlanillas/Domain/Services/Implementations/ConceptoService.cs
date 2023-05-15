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
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class ConceptoService : IConceptoService
    {
        public Response GrabarConcepto(Operacion operacion, ConceptoEntity conceptoEntity, int userID)
        {
            Result result;

            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:

                        var grabarConcepto = new USP_I_RegistrarConcepto()
                        {
                            I_TipoConceptoID = conceptoEntity.I_TipoConceptoID,
                            T_ConceptoDesc = conceptoEntity.T_ConceptoDesc,
                            C_ConceptoCod = conceptoEntity.C_ConceptoCod,
                            I_UserID = userID
                        };

                        result = grabarConcepto.Execute();

                        break;

                    case Operacion.Actualizar:

                        var actualizarConcepto = new USP_U_ActualizarConcepto()
                        {
                            I_TipoConceptoID = conceptoEntity.I_TipoConceptoID,
                            T_ConceptoDesc = conceptoEntity.T_ConceptoDesc,
                            C_ConceptoCod = conceptoEntity.C_ConceptoCod,
                            I_UserID = userID
                        };

                        result = actualizarConcepto.Execute();

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

        public List<ConceptoDTO> ListarConceptos()
        {
            var lista = VW_Conceptos.FindAll()
                .Select(x => Mapper.VW_Conceptos_To_ConceptoDTO(x))
                .ToList();

            return lista;
        }

        public ConceptoDTO ObtenerConcepto(int I_ConceptoID)
        {
            ConceptoDTO conceptoDTO;

            var view = VW_Conceptos.FindByID(I_ConceptoID);

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
    }
}
