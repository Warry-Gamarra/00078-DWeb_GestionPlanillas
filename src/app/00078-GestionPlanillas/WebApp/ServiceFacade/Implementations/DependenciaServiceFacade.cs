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
    public class DependenciaServiceFacade : IDependenciaServiceFacade
    {
        private IDependenciaService _dependenciaService;

        public DependenciaServiceFacade()
        {
            _dependenciaService = new DependenciaService();
        }

        public Response GrabarDependencia(Operacion operacion, DependenciaModel model, int userID)
        {
            Response response;

            try
            {
                var dependenciaEntity = new DependenciaEntity()
                {
                    dependenciaID = model.dependenciaID,
                    dependenciaCod = model.dependenciaCod.Trim(),
                    dependenciaDesc = model.dependenciaDesc.Trim()
                };

                response = _dependenciaService.GrabarDependencia(operacion, dependenciaEntity, userID);
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

        public List<DependenciaModel> ListarDependencias()
        {
            var lista = _dependenciaService.ListarDependencias(true)
                .Select(x => Mapper.DependenciaDTO_To_DependenciaModel(x))
                .ToList();

            return lista;
        }

        public SelectList ObtenerComboDependencias(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _dependenciaService.ListarDependencias(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "dependenciaID", "dependenciaCodDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "dependenciaID", "dependenciaCodDesc");
            }
        }

        public DependenciaModel ObtenerDependencia(int dependenciaID)
        {
            DependenciaModel dependenciaModel;

            var dependenciaDTO = _dependenciaService.ObtenerDependencia(dependenciaID);

            if (dependenciaDTO == null)
            {
                dependenciaModel = null;
            }
            else
            {
                dependenciaModel = Mapper.DependenciaDTO_To_DependenciaModel(dependenciaDTO);
            }

            return dependenciaModel;
        }

        public Response CambiarEstado(int dependenciaID, bool estaHabilitado, int userID)
        {
            var result = _dependenciaService.CambiarEstado(dependenciaID, estaHabilitado, userID);

            return result;
        }

        public Response Eliminar(int dependenciaID, int userID)
        {
            var result = _dependenciaService.Eliminar(dependenciaID, userID);

            return result;
        }
    }
}