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
    public class PeriodoServiceFacade : IPeriodoServiceFacade
    {
        IPeriodoService _periodoService;

        public PeriodoServiceFacade()
        {
            _periodoService = new PeriodoService();
        }

        public List<int> ListarAños(bool soloAñoConMeses)
        {
            return _periodoService.ListarAños(soloAñoConMeses);
        }

        public SelectList ObtenerComboAños(bool soloAñoConMeses, int? selectedItem = null)
        {
            var lista = _periodoService.ListarAños(soloAñoConMeses);

            var result = new List<SelectListItem>();

            lista.ForEach(x => {
                var item = new SelectListItem()
                {
                    Value = x.ToString(),
                    Text = x.ToString()
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

        public SelectList ObtenerComboMesesSegunAño(int año, int? selectedItem = null)
        {
            var lista = _periodoService.ListarMesesSegunAño(año);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "mes", "mesDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "mes", "mesDesc");
            }
        }

        public Response GrabarAño(int año)
        {
            Response response;

            try
            {
                response = _periodoService.GrabarAño(año);
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

        public Response GrabarPeriodo(Operacion operacion, PeriodoModel model, int userID)
        {
            Response response;

            try
            {
                var periodoEntity = new PeriodoEntity()
                {
                    periodoID = model.periodoID,
                    anio = model.anio,
                    mes = model.mes
                };

                response = _periodoService.GrabarPeriodo(operacion, periodoEntity, userID);
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

        public List<PeriodoModel> ListarPeriodos()
        {
            var lista = _periodoService.ListarPeriodos()
                .Select(x => Mapper.PeriodoDTO_To_PeriodoModel(x))
                .ToList();

            return lista;
        }

        public PeriodoModel ObtenerPeriodo(int periodoID)
        {
            PeriodoModel model;

            var periodoDTO = _periodoService.ObtenerPeriodo(periodoID);

            if (periodoDTO == null)
            {
                model = null;
            }
            else
            {
                model = Mapper.PeriodoDTO_To_PeriodoModel(periodoDTO);
            }

            return model;
        }

        public SelectList ObtenerComboMeses(int? selectedItem = null)
        {
            var lista = _periodoService.ListarMeses();

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "mes", "mesDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "mes", "mesDesc");
            }
        }

        public Response Eliminar(int periodoID, int userID)
        {
            var result = _periodoService.Eliminar(periodoID, userID);

            return result;
        }
    }
}
