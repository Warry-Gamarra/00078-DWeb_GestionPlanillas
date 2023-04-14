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

        public SelectList ListarRegimenes()
        {
            var lista  = _regimenService.ListarRegimenes();

            return new SelectList(lista, "I_RegimenID", "T_RegimenDesc");
        }
    }
}