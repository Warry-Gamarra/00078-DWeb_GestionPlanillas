using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class TipoDocumentoServiceFacade : ITipoDocumentoServiceFacade
    {
        private ITipoDocumentoService _tipoDocumentoService;

        public TipoDocumentoServiceFacade()
        {
            _tipoDocumentoService = new TipoDocumentoService();
        }

        public SelectList ObtenerComboTipoDocumentos(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _tipoDocumentoService.ListaTipoDocumentos(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "I_TipoDocumentoID", "T_TipoDocumentoDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "I_TipoDocumentoID", "T_TipoDocumentoDesc");
            }
        }
    }
}