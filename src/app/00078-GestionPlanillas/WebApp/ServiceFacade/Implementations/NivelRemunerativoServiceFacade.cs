﻿using Domain.Services;
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

        public SelectList ListarNivelesRemunerativos(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _nivelRemunerativoService.ListarNivelesRemunerativos(incluirDeshabilitados);


            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "I_NivelRemunerativoID", "T_NivelRemunerativoDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "I_NivelRemunerativoID", "T_NivelRemunerativoDesc");
            }
        }
    }
}