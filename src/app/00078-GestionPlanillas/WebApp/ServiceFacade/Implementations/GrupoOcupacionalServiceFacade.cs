using Domain.Entities;
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

        public SelectList ObtenerComboGruposOcupacionales(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _grupoOcupacionalService.ListarGruposOcupacionales(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "grupoOcupacionalID", "grupoOcupacionalDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "grupoOcupacionalID", "grupoOcupacionalDesc");
            }
        }

        public List<GrupoOcupacionalDTO> ListarGruposOcupacionales(bool incluirDeshabilitados = false)
        {
            var lista = _grupoOcupacionalService.ListarGruposOcupacionales(incluirDeshabilitados)
                .ToList();

            return lista;
        }
    }
}