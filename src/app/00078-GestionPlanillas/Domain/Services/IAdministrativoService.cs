using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAdministrativoService
    {
        List<AdministrativoDTO> ListarAdministrativos();

        AdministrativoDTO ObtenerAdministrativoPorID(int administrativoID);

        List<AdministrativoDTO> ListarAdministrativoPorTrabajadorID(int trabajadorID, bool incluirDeshabilitados = false);
    }
}
