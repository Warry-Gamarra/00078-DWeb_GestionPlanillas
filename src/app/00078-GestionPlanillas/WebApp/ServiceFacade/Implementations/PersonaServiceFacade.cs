﻿using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class PersonaServiceFacade : IPersonaServiceFacade
    {
        private IPersonaService _personaService;

        public PersonaServiceFacade()
        {
            _personaService = new PersonaService();
        }

        public PersonaModel ObtenerPersona(int tipoDocumentoID, string numDocumento)
        {
            var dto = _personaService.ObtenerPersona(tipoDocumentoID, numDocumento);

            var result = Mapper.PersonaDTO_To_PersonaModel(dto);

            return result;
        }
    }
}