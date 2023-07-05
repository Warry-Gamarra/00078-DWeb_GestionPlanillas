using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class EstadoServiceFacade : IEstadoServiceFacade
    {
        private IEstadoService _estadoService;

        public EstadoServiceFacade()
        {
            _estadoService = new EstadoService();
        }

        public SelectList ObtenerComboEstados(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _estadoService.ListarEstados(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "I_EstadoID", "T_EstadoDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "I_EstadoID", "T_EstadoDesc");
            }
        }
    }
}