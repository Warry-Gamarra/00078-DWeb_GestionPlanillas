using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class CategoriaPlanillaServiceFacade : ICategoriaPlanillaServiceFacade
    {
        private ICategoriaPlanillaService _categoriaPlanillaService;

        public CategoriaPlanillaServiceFacade()
        {
            _categoriaPlanillaService = new CategoriaPlanillaService();
        }

        public SelectList ListarCategoriasPlanillas(bool incluirDeshabilitados = false)
        {
            var lista = _categoriaPlanillaService.ListarCategoriasPlanillas(incluirDeshabilitados);

            return new SelectList(lista, "I_CategoriaPlanillaID", "T_CategoriaPlanillaDesc");
        }
    }
}