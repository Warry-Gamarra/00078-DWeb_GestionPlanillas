using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.ServiceFacade
{
    public interface IGrupoOcupacionalServiceFacade
    {
        SelectList ObtenerComboGruposOcupacionales(bool incluirDeshabilitados = false, int? selectedItem = null);

        List<GrupoOcupacionalDTO> ListarGruposOcupacionales(bool incluirDeshabilitados = false);
    }
}