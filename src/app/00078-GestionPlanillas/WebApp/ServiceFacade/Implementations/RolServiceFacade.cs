using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class RolServiceFacade : IRolServiceFacade
    {
        private IRolService _rolService;

        public RolServiceFacade()
        {
            _rolService = new RolService();
        }

        public SelectList ObtenerComboRoles(int? selectedItem = null)
        {
            var lista = _rolService.ListarRoles();

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "roleId", "roleName", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "roleId", "roleName");
            }
        }
    }
}