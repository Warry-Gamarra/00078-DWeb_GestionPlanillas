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
    public class HorasDocenteServiceFacade : IHorasDocenteServiceFacade
    {
        private IHorasDocenteService _horasDocenteService;

        public HorasDocenteServiceFacade()
        {
            _horasDocenteService = new HorasDocenteService();
        }

        public SelectList ObtenerComboHorasDedicacionDocente(int? selectedItem = null)
        {
            var result = new List<SelectListItem>();

            var lista = _horasDocenteService.ListarHorasDedicacionDocente();

            foreach (var group in lista.GroupBy(x => x.dedicacionDocenteDesc))
            {
                var optionGroup = new SelectListGroup() { Name = group.Key };

                var range = group.Select(x => new SelectListItem()
                {
                    Value = x.horasDocenteID.ToString(),
                    Text = x.dedicacionXHorasCorto,
                    Group = optionGroup,
                    Selected = selectedItem.HasValue ? x.horasDocenteID == selectedItem.Value : false
                });

                result.AddRange(
                   range 
                );
            }

            return new SelectList(result, "Value", "Text", "Group.Name", null, null);
        }

        public List<HorasDedicacionDocenteDTO> ListarHorasDedicacionDocente()
        {
            var result = new List<SelectListItem>();

            var lista = _horasDocenteService.ListarHorasDedicacionDocente()
                .ToList();

            return lista;
        }
    }
}