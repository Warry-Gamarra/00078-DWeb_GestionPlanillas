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

        public SelectList ListarAfps()
        {
            var lista = _afpService.ListarAfps();

            return new SelectList(lista, "I_AfpID", "T_AfpDesc");
        }
    }
}