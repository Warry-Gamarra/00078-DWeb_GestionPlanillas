﻿using Data.Connection;
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

                        var validacionRegistrar = ValidarCamposConcepto(operacion, plantillaPlanillaConcepto);

                        if (validacionRegistrar.Item1)
                        {
                            var grabarPlantillaPlanillaConcepto = new USP_I_RegistrarPlantillaPlanillaConcepto()
                            {
                                I_PlantillaPlanillaID = plantillaPlanillaConcepto.plantillaPlanillaID,
                                I_ConceptoID = plantillaPlanillaConcepto.conceptoID,
                                B_EsValorFijo = plantillaPlanillaConcepto.esValorFijo,
                                B_ValorEsExterno = plantillaPlanillaConcepto.valorEsExterno,
                                M_ValorConcepto = plantillaPlanillaConcepto.valorConcepto,
                                B_AplicarFiltro1 = plantillaPlanillaConcepto.aplicarFiltro1,
                                I_Filtro1 = plantillaPlanillaConcepto.filtro1,
                                B_AplicarFiltro2 = plantillaPlanillaConcepto.aplicarFiltro2,
                                I_Filtro2 = plantillaPlanillaConcepto.filtro2,
                                I_UserID = userID
                            };

                            result = grabarPlantillaPlanillaConcepto.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = validacionRegistrar.Item2
                            };
                        }
                        
                        break;

                    case Operacion.Actualizar:

                        var validacionActualizar = ValidarCamposConcepto(operacion, plantillaPlanillaConcepto);

                        if (validacionActualizar.Item1)
                        {
                            var actualizarPlantillaPlanillaConcepto = new USP_U_ActualizarPlantillaPlanillaConcepto()
                            {
                                I_PlantillaPlanillaConceptoID = plantillaPlanillaConcepto.plantillaPlanillaConceptoID.Value,
                                I_PlantillaPlanillaID = plantillaPlanillaConcepto.plantillaPlanillaID,
                                I_ConceptoID = plantillaPlanillaConcepto.conceptoID,
                                B_EsValorFijo = plantillaPlanillaConcepto.esValorFijo,
                                B_ValorEsExterno = plantillaPlanillaConcepto.valorEsExterno,
                                M_ValorConcepto = plantillaPlanillaConcepto.valorConcepto,
                                B_AplicarFiltro1 = plantillaPlanillaConcepto.aplicarFiltro1,
                                I_Filtro1 = plantillaPlanillaConcepto.filtro1,
                                B_AplicarFiltro2 = plantillaPlanillaConcepto.aplicarFiltro2,
                                I_Filtro2 = plantillaPlanillaConcepto.filtro2,
                                I_UserID = userID
                            };

                            result = actualizarPlantillaPlanillaConcepto.Execute();
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = validacionActualizar.Item2
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
    
        private Tuple<bool, string> ValidarCamposConcepto(Operacion operacion, PlantillaPlanillaConceptoEntity plantillaPlanillaConcepto)
        {
            bool correcto = true;
            string mensaje = null;

            switch (operacion)
            {
                case Operacion.Registrar:
                    
                    if (!plantillaPlanillaConcepto.valorEsExterno && 
                        (!plantillaPlanillaConcepto.valorConcepto.HasValue || plantillaPlanillaConcepto.valorConcepto.Value <= 0))
                    {
                        correcto = false;
                        mensaje = "El valor del concepto es obligatorio.";
                        break;
                    }

                    if (plantillaPlanillaConcepto.aplicarFiltro1 && !plantillaPlanillaConcepto.filtro1.HasValue)
                    {
                        correcto = false;
                        mensaje = "Debe seleccionar una opción para el filtro 1.";
                        break;
                    }

                    if (plantillaPlanillaConcepto.aplicarFiltro2 && !plantillaPlanillaConcepto.filtro2.HasValue)
                    {
                        correcto = false;
                        mensaje = "Debe seleccionar una opción para el filtro 2.";
                        break;
                    }

                    if (ListarConceptosAsignados(plantillaPlanillaConcepto.plantillaPlanillaID)
                        .Where(x => 
                            x.conceptoID == plantillaPlanillaConcepto.conceptoID && 
                            x.filtro1 == plantillaPlanillaConcepto.filtro1 && 
                            x.filtro2 == plantillaPlanillaConcepto.filtro2)
                        .FirstOrDefault() != null)
                    {
                        correcto = false;
                        mensaje = "El concepto se encuentra repetido.";
                    }
                    
                    break;

                case Operacion.Actualizar:

                    if (!plantillaPlanillaConcepto.plantillaPlanillaConceptoID.HasValue)
                    {
                        correcto = false;
                        mensaje = "Ha ocurrido un error al obtener los datos. Por favor recargue la página y vuelva a intentarlo.";
                        break;
                    }

                    if (!plantillaPlanillaConcepto.valorEsExterno && 
                        (!plantillaPlanillaConcepto.valorConcepto.HasValue || plantillaPlanillaConcepto.valorConcepto.Value <= 0))
                    {
                        correcto = false;
                        mensaje = "El valor del concepto es obligatorio.";
                        break;
                    }

                    if (plantillaPlanillaConcepto.aplicarFiltro1 && !plantillaPlanillaConcepto.filtro1.HasValue)
                    {
                        correcto = false;
                        mensaje = "Debe seleccionar una opción para el filtro 1.";
                        break;
                    }

                    if (plantillaPlanillaConcepto.aplicarFiltro2 && !plantillaPlanillaConcepto.filtro2.HasValue)
                    {
                        correcto = false;
                        mensaje = "Debe seleccionar una opción para el filtro 2.";
                        break;
                    }

                    var listaConceptos = ListarConceptosAsignados(plantillaPlanillaConcepto.plantillaPlanillaID);

                    var conceptoDTO = listaConceptos
                        .Where(x =>
                            x.plantillaPlanillaConceptoID != plantillaPlanillaConcepto.plantillaPlanillaConceptoID.Value &&
                            x.conceptoID == plantillaPlanillaConcepto.conceptoID &&
                            x.filtro1 == plantillaPlanillaConcepto.filtro1 &&
                            x.filtro2 == plantillaPlanillaConcepto.filtro2)
                        .FirstOrDefault();

                    if (conceptoDTO != null)
                    {
                        correcto = false;
                        mensaje = "El concepto se encuentra repetido.";
                    }

                    break;

                default:
                    correcto = false;
                    mensaje = "No se reconoce la operación a realizar.";
                    break;
            }
            
            return new Tuple<bool, string>(correcto, mensaje);
        }
    }
}
