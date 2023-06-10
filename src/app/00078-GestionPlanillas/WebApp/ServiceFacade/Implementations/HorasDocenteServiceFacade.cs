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

        public SelectList ListarHorasDedicacionDocente(int? selectedItem = null)
        {
            var result = new List<SelectListItem>();

            var lista = _horasDocenteService.ListarHorasDedicacionDocente();

            foreach (var group in lista.GroupBy(x => x.T_DedicacionDocenteDesc))
            {
                var optionGroup = new SelectListGroup() { Name = group.Key };

                var range = group.Select(x => new SelectListItem()
                {
                    Value = x.I_HorasDocenteID.ToString(),
                    Text = String.Format("{0} / {1}", x.C_DedicacionDocenteCod,x.I_Horas.ToString()),
                    Group = optionGroup,
                    Selected = selectedItem.HasValue ? x.I_HorasDocenteID == selectedItem.Value : false
                });

                result.AddRange(
                   range 
                );
            }

            return new SelectList(result, "Value", "Text", "Group.Name", null, null);
        }
    }
}