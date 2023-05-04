using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class PeriodoServiceFacade : IPeriodoServiceFacade
    {
        IPeriodoService _periodoService;

        public PeriodoServiceFacade()
        {
            _periodoService = new PeriodoService();
        }

        public SelectList ListarAños()
        {
            var lista = _periodoService.ListarAños();

            var result = new List<SelectListItem>();

            lista.ForEach(x => {
                var item = new SelectListItem() {
                    Value = x.ToString(),
                    Text = x.ToString()
                };

                result.Add(item);
            });

            return new SelectList(result, "Value", "Text");
        }

        public SelectList ListarMeses(int I_Anio)
        {
            var lista = _periodoService.ListarMeses(I_Anio);

            return new SelectList(lista, "I_Mes", "T_MesDesc");
        }
    }
}
