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
    public class PersonaService : IPersonaService
    {
        public PersonaDTO ObtenerPersona(int I_TipoDocumentoID, string C_NumDocumento)
        {
            PersonaDTO personaDTO;

            var table = TC_Persona.FindByNumDocumento(I_TipoDocumentoID, C_NumDocumento);

            if (table == null)
            {
                personaDTO = null;
            }
            else
            {
                personaDTO = Mapper.TC_Persona_To_PersonaDTO(table);
            }

            return personaDTO;
        }
    }
}
