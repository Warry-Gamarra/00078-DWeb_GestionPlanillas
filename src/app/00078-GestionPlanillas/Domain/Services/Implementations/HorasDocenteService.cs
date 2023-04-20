using Data.Tables;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class HorasDocenteService : IHorasDocenteService
    {
        public List<HorasDedicacionDocenteDTO> ListarHorasDedicacionDocente()
        {
            var result = new List<HorasDedicacionDocenteDTO>();

            foreach(var hora in TC_HorasDocente.FindAll())
            {
                var dedicacion = TC_DedicacionDocente.FindByID(hora.I_DedicacionDocenteID);

                var item = new HorasDedicacionDocenteDTO()
                {
                    I_HorasDocenteID = hora.I_HorasDocenteID,
                    I_Horas = hora.I_Horas,
                    B_HoraHabilitado = hora.B_Habilitado,
                    I_DedicacionDocenteID = dedicacion.I_DedicacionDocenteID,
                    T_DedicacionDocenteDesc = dedicacion.T_DedicacionDocenteDesc,
                    C_DedicacionDocenteCod = dedicacion.C_DedicacionDocenteCod,
                    B_DedicacionHabilitado = dedicacion.B_Habilitado
                };

                result.Add(item);
            }

            return result.OrderBy(x => x.I_Horas).ToList();
        }
    }
}
