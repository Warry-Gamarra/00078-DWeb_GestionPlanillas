using Data.Connection;
using Data.Procedures;
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
    public class PlantillaPlanillaConceptoService : IPlantillaPlanillaConceptoService
    {
        public Response GrabarPlantillaPlanillaConcepto(Operacion operacion, PlantillaPlanillaConceptoEntity plantillaPlanillaConcepto, int userID)
        {
            Result result;

            try
            {
                switch(operacion)
                {
                    case Operacion.Registrar:

                        var grabarPlantillaPlanillaConcepto = new USP_I_RegistrarPlantillaPlanillaConcepto()
                        {
                            I_PlantillaPlanillaID = plantillaPlanillaConcepto.plantillaPlanillaID,
                            I_ConceptoID = plantillaPlanillaConcepto.conceptoID,
                            B_EsMontoFijo = plantillaPlanillaConcepto.esMontoFijo,
                            B_MontoEstaAqui = plantillaPlanillaConcepto.montoEstaAqui,
                            M_Monto = plantillaPlanillaConcepto.monto,
                            B_AplicarFiltro1 = plantillaPlanillaConcepto.aplicarFiltro1,
                            I_Filtro1 = plantillaPlanillaConcepto.filtro1,
                            B_AplicarFiltro2 = plantillaPlanillaConcepto.aplicarFiltro2,
                            I_Filtro2 = plantillaPlanillaConcepto.filtro2,
                            I_UserID = userID
                        };

                        result = grabarPlantillaPlanillaConcepto.Execute();

                        break;

                    case Operacion.Actualizar:

                        var actualizarPlantillaPlanillaConcepto = new USP_U_ActualizarPlantillaPlanillaConcepto()
                        {
                            I_PlantillaPlanillaConceptoID = plantillaPlanillaConcepto.plantillaPlanillaConceptoID.Value,
                            I_PlantillaPlanillaID = plantillaPlanillaConcepto.plantillaPlanillaID,
                            I_ConceptoID = plantillaPlanillaConcepto.conceptoID,
                            B_EsMontoFijo = plantillaPlanillaConcepto.esMontoFijo,
                            B_MontoEstaAqui = plantillaPlanillaConcepto.montoEstaAqui,
                            M_Monto = plantillaPlanillaConcepto.monto,
                            B_AplicarFiltro1 = plantillaPlanillaConcepto.aplicarFiltro1,
                            I_Filtro1 = plantillaPlanillaConcepto.filtro1,
                            B_AplicarFiltro2 = plantillaPlanillaConcepto.aplicarFiltro2,
                            I_Filtro2 = plantillaPlanillaConcepto.filtro2,
                            I_UserID = userID
                        };

                        result = actualizarPlantillaPlanillaConcepto.Execute();

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

        public List<ConceptoAsignadoPlantillaDTO> ListarConceptosAsignados(int plantillaPlanillaID)
        {
            var lista = VW_ConceptosAsignados_Plantilla.FindByPlantillaPlanillaID(plantillaPlanillaID)
                .Select(x => Mapper.VW_ConceptosAsignados_Plantilla_To_ConceptoAsignadoPlantillaDTO(x))
                .ToList();
            
            return lista;
        }

        public ConceptoAsignadoPlantillaDTO ObtenerPlantillaPlanillaConcepto(int conceptoID)
        {
            ConceptoAsignadoPlantillaDTO plantillaPlanillaDTO;

            var view = VW_ConceptosAsignados_Plantilla.FindByID(conceptoID);

            if (view == null)
            {
                plantillaPlanillaDTO = null;
            }
            else
            {
                plantillaPlanillaDTO = Mapper.VW_ConceptosAsignados_Plantilla_To_ConceptoAsignadoPlantillaDTO(view);
            }

            return plantillaPlanillaDTO;
        }

        public Response CambiarEstado(int plantillaPlanillaConceptoID, bool estadHabilitado, int userID)
        {
            Result result;

            try
            {
                var cambiarEstado = new USP_U_CambiarEstadoPlantillaPlanillaConcepto()
                {
                    I_PlantillaPlanillaConceptoID = plantillaPlanillaConceptoID,
                    B_Habilitado = !estadHabilitado,
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
