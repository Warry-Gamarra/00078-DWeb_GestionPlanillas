using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class DependenciaServiceFacade : IDependenciaServiceFacade
    {
        private IDependenciaService _dependenciaService;

        public DependenciaServiceFacade()
        {
            _dependenciaService = new DependenciaService();
        }

        public SelectList ListarDependencias(bool incluirDeshabilitados = false)
        {
            var lista = _dependenciaService.ListarDependencias(incluirDeshabilitados);

            return new SelectList(lista, "I_DependenciaID", "T_DependenciaCodDesc");
        }
    }
}