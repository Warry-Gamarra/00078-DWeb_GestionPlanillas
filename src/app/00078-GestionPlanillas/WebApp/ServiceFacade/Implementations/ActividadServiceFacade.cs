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
    public class ActividadServiceFacade : IActividadServiceFacade
    {
        private IActividadService _actividadService;

        public ActividadServiceFacade()
        {
            _actividadService = new ActividadService();
        }


        public Response GrabarActividad(Operacion operacion, ActividadModel model, int userID)
        {
            Response response;

            try
            {
                var actividadEntity = new ActividadEntity()
                {
                    actividadID = model.actividadID,
                    actividadCod = model.actividadCod.Trim(),
                    actividadDesc = model.actividadDesc.Trim()
                };

                response = _actividadService.GrabarActividad(operacion, actividadEntity, userID);
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

        public List<ActividadModel> ListarActividades()
        {
            var lista = _actividadService.ListarActividades()
                .Select(x => Mapper.ActividadDTO_To_ActividadModel(x))
                .ToList();

            return lista;
        }

        public SelectList ObtenerComboActividades(int? selectedItem = null)
        {
            var lista = _actividadService.ListarActividades();

            var result = new List<SelectListItem>();

            lista.ForEach(x => {
                var item = new SelectListItem()
                {
                    Value = x.actividadID.ToString(),
                    Text = String.Format("{0} {1}", x.actividadCod, x.actividadDesc)
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

        public ActividadModel ObtenerActividad(int actividadID)
        {
            ActividadModel actividadModel;

            var actividadDTO = _actividadService.ObtenerActividad(actividadID);

            if (actividadDTO == null)
            {
                actividadModel = null;
            }
            else
            {
                actividadModel = Mapper.ActividadDTO_To_ActividadModel(actividadDTO);
            }

            return actividadModel;
        }

        public Response Eliminar(int actividadID, int userID)
        {
            var result = _actividadService.Eliminar(actividadID, userID);

            return result;
        }
    }
}