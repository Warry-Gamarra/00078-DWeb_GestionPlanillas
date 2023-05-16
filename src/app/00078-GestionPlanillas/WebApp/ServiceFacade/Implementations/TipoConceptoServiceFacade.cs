using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class TipoConceptoServiceFacade : ITipoConceptoServiceFacade
    {
        ITipoConceptoService _tipoConceptoService;

        public TipoConceptoServiceFacade()
        {
            _tipoConceptoService = new TipoConceptoService();
        }

        public SelectList ListarTiposConceptos(bool incluirDeshabilitados = false)
        {
            var lista = _tipoConceptoService.ListarTiposConceptos(incluirDeshabilitados);

            return new SelectList(lista, "tipoConceptoID", "tipoConceptoDesc");
        }
    }
}