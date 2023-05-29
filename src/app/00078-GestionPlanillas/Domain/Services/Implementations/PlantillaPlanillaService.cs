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
    public class PlantillaPlanillaService : IPlantillaPlanillaService
    {
        public Response GrabarPlantillaPlanilla(Operacion operacion, PlantillaPlanillaEntity plantillaPlanillaEntity, int userID)
        {
            Result result;

            try
            {
                switch (operacion)
                {
                    case Operacion.Registrar:

                        var grabarPlantillaPlanilla = new USP_I_RegistrarPlantillaPlanilla()
                        {
                            I_CategoriaPlanillaID = plantillaPlanillaEntity.categoriaPlanillaID,
                            T_PlantillaPlanillaDesc = plantillaPlanillaEntity.plantillaPlanillaDesc,
                            I_UserID = userID
                        };

                        result = grabarPlantillaPlanilla.Execute();

                        break;

                    case Operacion.Actualizar:

                        var actualizarPlantillaPlanilla = new USP_U_ActualizarPlantillaPlanilla()
                        {
                            I_PlantillaPlanillaID = plantillaPlanillaEntity.plantillaPlanillaID.Value,
                            I_CategoriaPlanillaID = plantillaPlanillaEntity.categoriaPlanillaID,
                            T_PlantillaPlanillaDesc = plantillaPlanillaEntity.plantillaPlanillaDesc,
                            I_UserID = userID
                        };

                        result = actualizarPlantillaPlanilla.Execute();

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

        public List<PlantillaPlanillaDTO> ListarPlantillasPlanilla(bool incluirDeshabilitados = false)
        {
            var lista = VW_PlantillasPlanilla.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.VW_PlantillasPlanilla_To_PlantillaPlanillaDTO(x))
                .ToList();

            return result;
        }

        public PlantillaPlanillaDTO ObtenerPlantillaPlanilla(int plantillaPlanillaID)
        {
            PlantillaPlanillaDTO plantillaPlanillaDTO;

            var view = VW_PlantillasPlanilla.FindByID(plantillaPlanillaID);

            if (view == null)
            {
                plantillaPlanillaDTO = null;
            }
            else
            {
                plantillaPlanillaDTO = Mapper.VW_PlantillasPlanilla_To_PlantillaPlanillaDTO(view);
            }

            return plantillaPlanillaDTO;
        }

        public Response CambiarEstado(int plantillaPlanillaID, bool estaHabilitado, int userID)
        {
            Result result;

            try
            {
                var cambiarEstado = new USP_U_CambiarEstadoPlantillaPlanilla()
                {
                    I_PlantillaPlanillaID = plantillaPlanillaID,
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
