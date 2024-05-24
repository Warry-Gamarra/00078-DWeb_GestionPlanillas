using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IPersonaServiceFacade
    {
        PersonaModel ObtenerPersona(int tipoDocumentoID, string numDocumento);

        List<PersonaModel> ListarPersonasPorDocIdentidad(int tipoDocumentoID, string numDocumento);

        SelectList ObtenerComboSexos(bool incluirDeshabilitados = false, int? selectedItem = null);
    }
}
