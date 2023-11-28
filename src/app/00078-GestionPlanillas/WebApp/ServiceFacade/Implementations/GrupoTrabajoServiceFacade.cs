using Domain.Services.Implementations;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class GrupoTrabajoServiceFacade : IGrupoTrabajoServiceFacade
    {
        private IGrupoTrabajoService _grupoTrabajoService;

        public GrupoTrabajoServiceFacade()
        {
            _grupoTrabajoService = new GrupoTrabajoService();
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
    }
}