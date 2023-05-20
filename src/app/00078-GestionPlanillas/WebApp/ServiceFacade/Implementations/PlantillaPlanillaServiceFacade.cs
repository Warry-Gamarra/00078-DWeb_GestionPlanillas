﻿using Domain.Services.Implementations;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class PlantillaPlanillaServiceFacade : IPlantillaPlanillaServiceFacade
    {
        private IPlantillaPlanillaService _plantillaPlanillaService;

        public PlantillaPlanillaServiceFacade()
        {
            _plantillaPlanillaService = new PlantillaPlanillaService();
        }

        public Response GrabarPlantillaPlanilla(Operacion operacion, PlantillaPlanillaModel model, int userID)
        {
            Response response;
            bool existeOtraPlantillaHabilitada = false;

            try
            {
                var plantillaPlanillaDTO = _plantillaPlanillaService.ListarPlantillasPlanilla()
                    .Where(x => x.categoriaPlanillaID == model.categoriaPlanillaID)
                    .FirstOrDefault();

                if (operacion.Equals(Operacion.Registrar) && plantillaPlanillaDTO != null)
                {
                    existeOtraPlantillaHabilitada = true;
                }

                if (operacion.Equals(Operacion.Actualizar) && plantillaPlanillaDTO != null && plantillaPlanillaDTO.plantillaPlanillaID != model.plantillaPlanillaID)
                {
                    existeOtraPlantillaHabilitada = true;
                }

                if (!existeOtraPlantillaHabilitada)
                {
                    var plantillaPlanillaEntity = new PlantillaPlanillaEntity()
                    {
                        plantillaPlanillaID = model.plantillaPlanillaID,
                        categoriaPlanillaID = model.categoriaPlanillaID,
                        plantillaPlanillaDesc = model.plantillaPlanillaDesc
                    };

                    response = _plantillaPlanillaService.GrabarPlantillaPlanilla(operacion, plantillaPlanillaEntity, userID);
                }
                else
                {
                    response = new Response()
                    {
                        Message = "Sólo puede haber 1 plantilla de una misma categoría."
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

        public List<PlantillaPlanillaModel> ListarPlantillasPlanilla()
        {
            var lista = _plantillaPlanillaService.ListarPlantillasPlanilla(true)
                .Select(x => Mapper.PlantillaPlanillaDTO_To_PlantillaPlanillaModel(x))
                .ToList();
            
            return lista;
        }

        public PlantillaPlanillaModel ObtenerPlantillaPlanilla(int plantillaPlanillaID)
        {
            PlantillaPlanillaModel plantillaPlanillaModel;

            var plantillaPlanillaDTO = _plantillaPlanillaService.ObtenerPlantillaPlanilla(plantillaPlanillaID);

            if (plantillaPlanillaDTO == null)
            {
                plantillaPlanillaModel = null;
            }
            else
            {
                plantillaPlanillaModel = Mapper.PlantillaPlanillaDTO_To_PlantillaPlanillaModel(plantillaPlanillaDTO);
            }

            return plantillaPlanillaModel;
        }

        public Response CambiarEstado(int plantillaPlanillaID, bool estadHabilitado, int userID, string returnUrl)
        {
            var result = _plantillaPlanillaService.CambiarEstado(plantillaPlanillaID, estadHabilitado, userID);

            return result;
        }
    }
}