using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ITrabajadorService
    {
        List<TrabajadorDTO> ListarTrabajadores();

        Response GrabarTrabajador(TrabajadorEntity trabajadorEntity, int userID);
    }
}
