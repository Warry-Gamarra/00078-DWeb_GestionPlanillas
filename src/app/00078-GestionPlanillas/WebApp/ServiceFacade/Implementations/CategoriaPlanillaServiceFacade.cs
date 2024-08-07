﻿using Domain.Services;
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

        public SelectList ObtenerComboCategoriasPlanillas(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _categoriaPlanillaService.ListarCategoriasPlanillas(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "categoriaPlanillaID", "categoriaPlanillaDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "categoriaPlanillaID", "categoriaPlanillaDesc");
            }
        }

        public SelectList ObtenerComboCategoriasPlanillas(int categoriaPlanillaExcluidaID, bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _categoriaPlanillaService.ListarCategoriasPlanillas(incluirDeshabilitados)
                .Where(x => x.categoriaPlanillaID != categoriaPlanillaExcluidaID);
            
            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "categoriaPlanillaID", "categoriaPlanillaDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "categoriaPlanillaID", "categoriaPlanillaDesc");
            }
        }
    }
}