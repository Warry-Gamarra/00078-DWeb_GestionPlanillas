using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class RegimenServiceFacade : IRegimenServiceFacade
    {
        private IRegimenService _regimenService;

        public RegimenServiceFacade()
        {
            _regimenService = new RegimenService();
        }

        public SelectList ObtenerComboRegimenes(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista  = _regimenService.ListarRegimenes(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "I_RegimenID", "T_RegimenDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "I_RegimenID", "T_RegimenDesc");
            }
        }
    }
}