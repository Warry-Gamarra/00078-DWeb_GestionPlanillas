using Data.Views;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    internal static class Mapper
    {
        public static TrabajadorDTO VW_Trabajadores_To_TrabajadorDTO(VW_Trabajadores view)
        {
            var trabajadorDTO = new TrabajadorDTO()
            {
                I_TrabajadorID = view.I_TrabajadorID,
                T_Nombre = view.T_Nombre,
                T_ApellidoPaterno = view.T_ApellidoPaterno,
                T_ApellidoMaterno = view.T_ApellidoMaterno,
                I_TipoDocumentoID = view.I_TipoDocumentoID,
                T_TipoDocumentoDesc = view.T_TipoDocumentoDesc,
                C_NumDocumento = view.C_NumDocumento,
                D_FechaIngreso = view.D_FechaIngreso,
                I_RegimenID = view.I_RegimenID,
                T_RegimenDesc = view.T_RegimenDesc,
                I_EstadoID = view.I_EstadoID,
                T_EstadoDesc = view.T_EstadoDesc,
                I_VinculoID = view.I_VinculoID,
                T_VinculoDesc = view.T_VinculoDesc
            };

            return trabajadorDTO;
        }
    }
}
