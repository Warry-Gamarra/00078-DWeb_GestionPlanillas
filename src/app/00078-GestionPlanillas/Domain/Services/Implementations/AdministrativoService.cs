using Data.Views;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class AdministrativoService : IAdministrativoService
    {
        public List<AdministrativoDTO> ListarAdministrativos()
        {
            var lista = VW_Administrativo.FindAll().Select(x => Mapper.VW_Administrativo_To_AdministrativoDTO(x)).ToList();

            return lista;
        }

        public AdministrativoDTO ObtenerAdministrativoPorID(int administrativoID)
        {
            var entity = VW_Administrativo.FindByAdministrativoID(administrativoID);

            if (entity != null)
            {
                return Mapper.VW_Administrativo_To_AdministrativoDTO(entity);
            }

            return null;
        }

        public List<AdministrativoDTO> ListarAdministrativoPorTrabajadorID(int trabajadorID, bool incluirDeshabilitados = false)
        {
            var lista = VW_Administrativo.FindByTrabajadorID(trabajadorID)
                .Select(x => Mapper.VW_Administrativo_To_AdministrativoDTO(x)).ToList();

            return lista;
        }
    }
}
