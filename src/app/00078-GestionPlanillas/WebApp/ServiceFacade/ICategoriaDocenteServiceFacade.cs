using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApp.ServiceFacade
{
    public interface ICategoriaDocenteServiceFacade
    {
        SelectList ObtenerComboCategoriasDocente(bool? esParaDocenteOrdinario, bool incluirDeshabilitados = false, int? selectedItem = null);

        List<CategoriaDocenteDTO> ListarCategoriasDocente(bool? esParaDocenteOrdinario, bool incluirDeshabilitados = false);
    }
}
