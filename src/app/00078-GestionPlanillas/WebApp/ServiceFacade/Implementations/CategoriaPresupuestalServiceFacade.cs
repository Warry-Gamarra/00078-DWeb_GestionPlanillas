using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade.Implementations
{
    public class CategoriaPresupuestalServiceFacade : ICategoriaPresupuestalServiceFacade
    {
        private ICategoriaPresupuestalService _categoriaPresupuestalService;

        public CategoriaPresupuestalServiceFacade()
        {
            _categoriaPresupuestalService = new CategoriaPresupuestalService();
        }

        public SelectList ObtenerComboCategoriaPresupuestal(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _categoriaPresupuestalService.ListarCategoriaPresupuestal(incluirDeshabilitados);

            var result = new List<SelectListItem>();

            lista.ForEach(x => {
                var item = new SelectListItem()
                {
                    Value = x.categoriaPresupuestalID.ToString(),
                    Text = String.Format("{0} {1}", x.categoriaPresupCod, x.categoriaPresupDesc)
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