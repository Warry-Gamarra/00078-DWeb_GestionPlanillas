using Data.Tables;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class RolService : IRolService
    {
        public List<RolDTO> ListarRoles()
        {
            var lista = Webpages_Roles.FindAll();

            var result = lista.Select(x => Mapper.Webpages_Roles_To_Webpages_Roles(x))
                .ToList();

            return result;
        }
    }
}
