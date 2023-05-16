using Domain.Services.Implementations;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class PlantillaPlanillaServiceFacade : IPlantillaPlanillaServiceFacade
    {
        private IPlantillaPlanillaService _plantillaPlanillaService;

        public PlantillaPlanillaServiceFacade()
        {
            _plantillaPlanillaService = new PlantillaPlanillaService();
        }

        public SelectList ListarPlantillasPlanilla(bool incluirDeshabilitados = false)
        {
            var result = new List<SelectListItem>();

            var lista = _plantillaPlanillaService.ListarPlantillasPlanilla(incluirDeshabilitados);

            foreach (var group in lista.GroupBy(x => x.clasePlanillaDesc))
            {
                var optionGroup = new SelectListGroup() { Name = group.Key };

                var range = group.Select(x => new SelectListItem()
                {
                    Value = x.plantillaPlanillaID.ToString(),
                    Text = x.categoriaPlanillaDesc,
                    Group = optionGroup
                });

                result.AddRange(
                   range
                );
            }

            return new SelectList(result, "Value", "Text", "Group.Name", null, null);
        }
    }
}