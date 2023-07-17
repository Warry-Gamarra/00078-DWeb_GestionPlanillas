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

        public SelectList ObtenerComboAños(int? selectedItem = null)
        {
            var lista = _periodoService.ListarAños();

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

        public SelectList ObtenerComboMeses(int I_Anio, int? selectedItem = null)
        {
            var lista = _periodoService.ListarMeses(I_Anio);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "mes", "mesDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "mes", "mesDesc");
            }
        }
    }
}
