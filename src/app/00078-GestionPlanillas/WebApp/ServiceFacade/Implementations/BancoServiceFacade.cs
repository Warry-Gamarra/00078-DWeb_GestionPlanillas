﻿using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class BancoServiceFacade : IBancoServiceFacade
    {
        private IBancoService _bancoService;

        public BancoServiceFacade()
        {
            _bancoService = new BancoService();
        }

        public SelectList ObtenerComboBancos(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _bancoService.ListarBancos(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "bancoID", "bancoDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "bancoID", "bancoDesc");
            }
        }

        public SelectList ObtenerComboTipoCuentasBancarias(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _bancoService.ListarTipoCuentasBancarias(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "tipoCuentaBancariaID", "tipoCuentaBancariaDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "tipoCuentaBancariaID", "tipoCuentaBancariaDesc");
            }
        }
    }
}