using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class GrupoOcupacionalServiceFacade : IGrupoOcupacionalServiceFacade
    {
        private IGrupoOcupacionalService _grupoOcupacionalService;

        public GrupoOcupacionalServiceFacade()
        {
            _grupoOcupacionalService = new GrupoOcupacionalService();
        }

        public SelectList ListarGruposOcupacionales(bool incluirDeshabilitados = false)
        {
            var lista = _grupoOcupacionalService.ListarGruposOcupacionales(incluirDeshabilitados);

            return new SelectList(lista, "I_GrupoOcupacionalID", "T_GrupoOcupacionalDesc");
        }
    }
}