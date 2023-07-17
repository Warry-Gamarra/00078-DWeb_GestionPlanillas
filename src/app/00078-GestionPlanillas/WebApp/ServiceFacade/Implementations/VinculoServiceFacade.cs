using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class VinculoServiceFacade : IVinculoServiceFacade
    {
        private IVinculoService _vinculoService;

        public VinculoServiceFacade()
        {
            _vinculoService = new VinculoService();
        }

        public SelectList ObtenerComboVinculos(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _vinculoService.ListarVinculos(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "vinculoID", "vinculoDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "vinculoID", "vinculoDesc");
            }
        }
    }
}