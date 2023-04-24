using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface ITrabajadorServiceFacade
    {
        List<TrabajadorModel> ListarTrabajadores();

        TrabajadorModel ObtenerTrabajador(int I_TrabajadorID);

        Response GrabarTrabajador(Operacion operacion, TrabajadorModel model, int userID);
    }
}
