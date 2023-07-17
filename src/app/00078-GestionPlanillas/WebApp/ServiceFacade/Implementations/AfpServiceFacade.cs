using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class AfpServiceFacade : IAfpServiceFacade
    {
        private IAfpService _afpService;

        public AfpServiceFacade()
        {
            _afpService = new AfpService();
        }

        public SelectList ObtenerComboAfps(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _afpService.ListarAfps(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "afpID", "afpDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "afpID", "afpDesc");
            }
        }
    }
}