using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class HorasDocenteServiceFacade : IHorasDocenteServiceFacade
    {
        private IHorasDocenteService _horasDocenteService;

        public HorasDocenteServiceFacade()
        {
            _horasDocenteService = new HorasDocenteService();
        }

        public SelectList ListarHorasDedicacionDocente()
        {
            var lista = _horasDocenteService.ListarHorasDedicacionDocente();

            return new SelectList(lista, "I_HorasDocenteID", "I_Horas", "T_DedicacionDocenteDesc", null, null);
        }
    }
}