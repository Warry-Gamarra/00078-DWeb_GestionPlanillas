using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class MetaServiceFacade : IMetaServiceFacade
    {
        private IMetaService _metaService;

        public MetaServiceFacade()
        {
            _metaService = new MetaService();
        }

        public SelectList ObtenerComboMetas(int? selectedItem = null)
        {
            var lista = _metaService.ListarMetas();

            var result = new List<SelectListItem>();

            lista.ForEach(x => {
                var item = new SelectListItem()
                {
                    Value = x.metaID.ToString(),
                    Text = String.Format("{0} {1}", x.metaCod, x.metaCodDesc)
                };

                result.Add(item);
            });

            if (selectedItem.HasValue)
            {
                return new SelectList(result, "Value", "Text", selectedItem.Value);
            }
            else
            {
                return new SelectList(result, "Value", "Text");
            }
        }
    }
}