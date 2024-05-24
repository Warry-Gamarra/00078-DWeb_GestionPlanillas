using Data.Views;
using Domain.Entities;
using Domain.Enums;
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

        TrabajadorDTO ObtenerTrabajador(int I_TrabajadorID);

        Response GrabarTrabajador(Operacion operacion, TrabajadorEntity trabajadorEntity, int userID);

        List<TrabajadorConPlanillaDTO> ListarTrabajadoresConPlanilla(int año, int mes, int categoriaPlanillaID);
    }
}
