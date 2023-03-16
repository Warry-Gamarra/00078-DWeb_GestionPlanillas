using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class AdministrativoServiceFacade : IAdministrativoServiceFacade
    {
        IAdministrativoService _administrativoService;

        public AdministrativoServiceFacade()
        {
            _administrativoService = new AdministrativoService();
        }

        public List<AdministrativoModel> ListarAdministrativos()
        {
            var lista = _administrativoService.ListarAdministrativos()
                .Select(x => Mapper.AdministrativoDTO_To_AdministrativoModel(x))
                .ToList();

            return lista;
        }

        public AdministrativoModel ObtenerAdministrativoPorID(int adminsitrativoID)
        {
            var administrativoDTO = _administrativoService.ObtenerAdministrativoPorID(adminsitrativoID);

            if (administrativoDTO != null)
            {
                return Mapper.AdministrativoDTO_To_AdministrativoModel(administrativoDTO);
            }

            return null;
        }

        public List<AdministrativoModel> ListarAdministrativoPorTrabajadorID(int trabajadorID)
        {
            var lista = _administrativoService.ListarAdministrativoPorTrabajadorID(trabajadorID)
                .Select(x => Mapper.AdministrativoDTO_To_AdministrativoModel(x))
                .ToList();

            return lista;
        }
    }
}
