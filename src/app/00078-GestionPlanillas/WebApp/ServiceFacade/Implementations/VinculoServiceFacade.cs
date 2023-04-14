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

        public SelectList ListarVinculos()
        {
            var lista = _vinculoService.ListarVinculos();

            return new SelectList(lista, "I_VinculoID", "T_VinculoDesc");
        }
    }
}