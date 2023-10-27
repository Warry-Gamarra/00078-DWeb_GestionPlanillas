using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class ActividadServiceFacade : IActividadServiceFacade
    {
        private IActividadService _actividadService;

        public ActividadServiceFacade()
        {
            _actividadService = new ActividadService();
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
    }
}