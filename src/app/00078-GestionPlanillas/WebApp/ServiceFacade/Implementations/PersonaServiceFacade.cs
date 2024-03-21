using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public List<PersonaModel> ListarPersonasPorDocIdentidad(int tipoDocumentoID, string numDocumento)
        {
            var lista = _personaService.ListarPersonasPorDocIdentidad(tipoDocumentoID, numDocumento);

            var result = lista.Select(x => Mapper.PersonaDTO_To_PersonaModel(x)).ToList();

            return result;
        }

        public SelectList ObtenerComboSexos(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _personaService.ListarSexos(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "sexoID", "sexoDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "sexoID", "sexoDesc");
            }
        }

        public SelectList ObtenerComboTipoCuentasBancarias(bool incluirDeshabilitados = false, int? selectedItem = null)
        {
            var lista = _personaService.ListarTipoCuentasBancarias(incluirDeshabilitados);

            if (selectedItem.HasValue)
            {
                return new SelectList(lista, "tipoCuentaBancariaID", "tipoCuentaBancariaDesc", selectedItem.Value);
            }
            else
            {
                return new SelectList(lista, "tipoCuentaBancariaID", "tipoCuentaBancariaDesc");
            }
        }
    }
}