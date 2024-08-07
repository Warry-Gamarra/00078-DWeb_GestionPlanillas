﻿using Domain.Entities;
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

        public SelectList ObtenerComboCategoriasDocente(bool? esParaDocenteOrdinario, bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _categoriaDocenteService.ListarCategoriasDocente(esParaDocenteOrdinario, incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "categoriaDocenteID", "categoriaDocenteDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "categoriaDocenteID", "categoriaDocenteDesc");
            }
        }

        public List<CategoriaDocenteDTO> ListarCategoriasDocente(bool? esParaDocenteOrdinario,  bool incluirDeshabilitados = false)
        {
            var lista = _categoriaDocenteService.ListarCategoriasDocente(esParaDocenteOrdinario, incluirDeshabilitados)
                .ToList();

            return lista;
        }
    }
}