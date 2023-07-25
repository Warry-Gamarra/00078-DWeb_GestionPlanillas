using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IValorExternoConceptoService
    {
        Response GrabarValoresExternos(List<ValorConceptoEntity> valores, int userID);

        List<ValorExternoConceptoDTO> ListarValoresExternosConceptos(int anio, int mes, int categoriaPlanillaID);

        ValorExternoConceptoDTO ObtenerValorExternoConcepto(int conceptoExternoValorID);

        Response ActualizarValorExternoConcepto(int conceptoExternoValorID, decimal valorConcepto, int userID);
    }
}
