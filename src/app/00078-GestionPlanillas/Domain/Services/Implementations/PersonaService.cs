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
        public PersonaDTO ObtenerPersona(int tipoDocumentoID, string numDocumento)
        {
            PersonaDTO personaDTO;

            var table = TC_Persona.FindByNumDocumento(tipoDocumentoID, numDocumento);

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

        public List<PersonaDTO> ListarPersonasPorDocIdentidad(int tipoDocumentoID, string numDocumento)
        {
            var lista = TC_Persona.ListByNumDocumento(tipoDocumentoID, numDocumento); ;

            var result = lista.Select(x => Mapper.TC_Persona_To_PersonaDTO(x)).ToList();

            return result;
        }

        public List<SexoDTO> ListarSexos(bool incluirDeshabilitados = false)
        {
            var lista = TC_Sexo.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_Sexo_To_SexoDTO(x))
                .ToList();

            return result;
        }
    }
}
