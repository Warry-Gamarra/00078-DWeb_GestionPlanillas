using Data.Procedures;
using Data.Views;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class TrabajadorService : ITrabajadorService
    {
        public List<TrabajadorDTO> ListarTrabajadores()
        {
            var lista = VW_Trabajadores.FindAll()
                .Select( x => Mapper.VW_Trabajadores_To_TrabajadorDTO(x))
                .ToList();

            return lista;
        }

        public Response GrabarTrabajador(TrabajadorEntity trabajadorEntity, int userID)
        {
            USP_I_GrabarDocente grabarDocente = new USP_I_GrabarDocente()
            {
                C_TrabajadorCod = trabajadorEntity.C_TrabajadorCod,
                T_ApellidoPaterno = trabajadorEntity.T_ApellidoPaterno,
                T_ApellidoMaterno = trabajadorEntity.T_ApellidoMaterno,
                T_Nombre = trabajadorEntity.T_Nombre,
                I_TipoDocumentoID = trabajadorEntity.I_TipoDocumentoID,
                C_NumDocumento = trabajadorEntity   .C_NumDocumento,
                I_RegimenID = trabajadorEntity.I_RegimenID,
                I_EstadoID = trabajadorEntity.I_EstadoID,
                I_VinculoID = trabajadorEntity.I_VinculoID,
                I_UserID = userID
            };

            var result = grabarDocente.Execute();

            return Mapper.Result_To_Response(result);
        }
    }
}
