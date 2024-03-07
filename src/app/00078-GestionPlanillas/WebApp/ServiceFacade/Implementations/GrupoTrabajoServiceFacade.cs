using Domain.Services.Implementations;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Enums;
using Domain.Helpers;
using WebApp.Models;
using Domain.Entities;

namespace WebApp.ServiceFacade.Implementations
{
    public class GrupoTrabajoServiceFacade : IGrupoTrabajoServiceFacade
    {
        private IGrupoTrabajoService _grupoTrabajoService;

        public GrupoTrabajoServiceFacade()
        {
            _grupoTrabajoService = new GrupoTrabajoService();
        }

        public Response GrabarGrupoTrabajo(Operacion operacion, GrupoTrabajoModel model, int userID)
        {
            Response response;

            try
            {
                var grupoTrabajoEntity = new GrupoTrabajoEntity()
                {
                    grupoTrabajoID = model.grupoTrabajoID,
                    grupoTrabajoCod = model.grupoTrabajoCod.Trim(),
                    grupoTrabajoDesc = model.grupoTrabajoDesc.Trim(),
                };

                response = _grupoTrabajoService.GrabarGrupoTrabajo(operacion, grupoTrabajoEntity, userID);
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

        public List<GrupoTrabajoModel> ListarGruposTrabajo()
        {
            var lista = _grupoTrabajoService.ListarGruposTrabajo(true)
               .Select(x => Mapper.GrupoTrabajoDTO_To_GrupoTrabajoModel(x))
               .ToList();

            return lista;
        }

        public SelectList ObtenerComboGruposTrabajo(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _grupoTrabajoService.ListarGruposTrabajo(incluirDeshabilitados);

            var result = new List<SelectListItem>();

            lista.ForEach(x => {
                var item = new SelectListItem()
                {
                    Value = x.grupoTrabajoID.ToString(),
                    Text = String.Format("{0} - {1}", x.grupoTrabajoCod, x.grupoTrabajoDesc)
                };

                result.Add(item);
            });

            if (selectedItem.HasValue)
            {
                return new SelectList(result, "Value", "Text", selectedItem.Value);
            }
            else
            {
                return new SelectList(result, "Value", "Text");
            }
        }

        public GrupoTrabajoModel ObtenerGrupoTrabajo(int grupoTrabajoID)
        {
            GrupoTrabajoModel grupoTrabajoModel;

            var grupoTrabajoDTO = _grupoTrabajoService.ObtenerGrupoTrabajo(grupoTrabajoID);

            if (grupoTrabajoDTO == null)
            {
                grupoTrabajoModel = null;
            }
            else
            {
                grupoTrabajoModel = Mapper.GrupoTrabajoDTO_To_GrupoTrabajoModel(grupoTrabajoDTO);
            }

            return grupoTrabajoModel;
        }

        public Response CambiarEstado(int grupoTrabajoID, bool estaHabilitado, int userID)
        {
            var result = _grupoTrabajoService.CambiarEstado(grupoTrabajoID, estaHabilitado, userID);

            return result;
        }

        public Response Eliminar(int grupoTrabajoID, int userID)
        {
            var result = _grupoTrabajoService.Eliminar(grupoTrabajoID, userID);

            return result;
        }
    }
}