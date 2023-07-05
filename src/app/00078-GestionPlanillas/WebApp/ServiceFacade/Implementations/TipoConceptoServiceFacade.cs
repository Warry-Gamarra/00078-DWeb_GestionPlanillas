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

        public SelectList ObtenerComboTiposConceptos(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _tipoConceptoService.ListarTiposConceptos(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "tipoConceptoID", "tipoConceptoDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "tipoConceptoID", "tipoConceptoDesc");
            }
        }
    }
}