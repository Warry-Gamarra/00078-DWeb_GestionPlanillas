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
    public class NivelRemunerativoServiceFacade : INivelRemunerativoServiceFacade
    {
        private INivelRemunerativoService _nivelRemunerativoService;
        
        public NivelRemunerativoServiceFacade()
        {
            _nivelRemunerativoService = new NivelRemunerativoService();
        }

        public SelectList ObtenerComboNivelesRemunerativos(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _nivelRemunerativoService.ListarNivelesRemunerativos(incluirDeshabilitados);


            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "nivelRemunerativoID", "nivelRemunerativoDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "nivelRemunerativoID", "nivelRemunerativoDesc");
            }
        }

        public List<NivelRemunerativoDTO> ListarNivelesRemunerativos(bool incluirDeshabilitados = false)
        {
            var lista = _nivelRemunerativoService.ListarNivelesRemunerativos(incluirDeshabilitados)
                .ToList();

            return lista;
        }
    }
}