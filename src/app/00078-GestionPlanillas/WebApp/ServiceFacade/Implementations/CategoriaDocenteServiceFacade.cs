using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class CategoriaDocenteServiceFacade : ICategoriaDocenteServiceFacade
    {
        private ICategoriaDocenteService _categoriaDocenteService;

        public CategoriaDocenteServiceFacade()
        {
            _categoriaDocenteService = new CategoriaDocenteService();
        }

        public SelectList ListarCategoriasDocente()
        {
            var lista = _categoriaDocenteService.ListarCategoriasDocente();

            return new SelectList(lista, "I_CategoriaDocenteID", "T_CategoriaDocenteDesc");
        }
    }
}