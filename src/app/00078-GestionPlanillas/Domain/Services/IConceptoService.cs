using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IConceptoService
    {
        Response GrabarConcepto(Operacion operacion, ConceptoEntity conceptoEntity, int userID);

        List<ConceptoDTO> ListarConceptos(bool incluirDeshabilitados = false);

        ConceptoDTO ObtenerConcepto(int conceptoID);

        ConceptoDTO ObtenerConcepto(string conceptoCod);

        Response CambiarEstado(int conceptoID, bool estaHabilitado, int userID);
    }
}
