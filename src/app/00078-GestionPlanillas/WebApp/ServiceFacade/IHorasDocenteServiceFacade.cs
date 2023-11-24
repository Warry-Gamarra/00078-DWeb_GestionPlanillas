using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApp.ServiceFacade
{
    public interface IHorasDocenteServiceFacade
    {
        SelectList ObtenerComboHorasDedicacionDocente(bool? esParaDocenteOrdinario, int? selectedItem = null);

        List<HorasDedicacionDocenteDTO> ListarHorasDedicacionDocente(bool? esParaDocenteOrdinario);
    }
}
