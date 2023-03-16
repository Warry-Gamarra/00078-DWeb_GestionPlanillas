using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IAdministrativoServiceFacade
    {
        List<AdministrativoModel> ListarAdministrativos();

        AdministrativoModel ObtenerAdministrativoPorID(int adminsitrativoID);

        List<AdministrativoModel> ListarAdministrativoPorTrabajadorID(int trabajadorID);
    }
}
