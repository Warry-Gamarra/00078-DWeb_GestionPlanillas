﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPersonaService
    {
        PersonaDTO ObtenerPersona(int tipoDocumentoID, string numDocumento);

        List<PersonaDTO> ListarPersonasPorDocIdentidad(int tipoDocumentoID, string numDocumento);

        List<SexoDTO> ListarSexos(bool incluirDeshabilitados = false);
    }
}
