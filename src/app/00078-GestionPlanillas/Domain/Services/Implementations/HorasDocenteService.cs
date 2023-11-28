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
        public List<HorasDedicacionDocenteDTO> ListarHorasDedicacionDocente(bool? esParaDocenteOrdinario)
        {
            var result = new List<HorasDedicacionDocenteDTO>();

            foreach(var hora in TC_HorasDocente.FindAll())
            {
                var dedicacion = TC_DedicacionDocente.FindByID(hora.I_DedicacionDocenteID);

                var item = new HorasDedicacionDocenteDTO()
                {
                    horasDocenteID = hora.I_HorasDocenteID,
                    horas = hora.I_Horas,
                    estaHoraHabilitada = hora.B_Habilitado,
                    dedicacionDocenteID = dedicacion.I_DedicacionDocenteID,
                    dedicacionDocenteDesc = dedicacion.T_DedicacionDocenteDesc,
                    dedicacionDocenteCod = dedicacion.C_DedicacionDocenteCod,
                    estaDedicacionHabilitada = dedicacion.B_Habilitado,
                    esParaDocenteOrdinario = dedicacion.B_ParaDocenteOrdinario
                };

                if (esParaDocenteOrdinario.HasValue)
                {
                    if (dedicacion.B_ParaDocenteOrdinario == esParaDocenteOrdinario.Value)
                    {
                        result.Add(item);
                    }   
                }
                else
                {
                    result.Add(item);
                }
            }

            if (esParaDocenteOrdinario.HasValue && esParaDocenteOrdinario.Value)
                return result.OrderBy(x => x.horas).ToList();
            else
                return result.ToList();
        }
    }
}
