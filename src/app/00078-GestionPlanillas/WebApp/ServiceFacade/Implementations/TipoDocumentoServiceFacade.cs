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

        public SelectList ListarTipoDocumentos(bool incluirDeshabilitados = false)
        {
            var lista = _tipoDocumentoService.ListaTipoDocumentos(incluirDeshabilitados);

            return new SelectList(lista, "I_TipoDocumentoID", "T_TipoDocumentoDesc");
        }
    }
}