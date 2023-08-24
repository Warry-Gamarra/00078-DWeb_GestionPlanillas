using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IValorExternoConceptoServiceFacade
    {
        Tuple<string, List<ValorExternoLecturaProcesadoDTO>> ObtenerListaValoresDeConceptos(HttpPostedFileBase file);

        Response GrabarValoresExternos(string fileName, int userID);

        List<ValorExternoConceptoModel> ListarValoresExternos(int anio, int mes, int categoriaPlanillaID);

        ValorExternoConceptoModel ObtenerValorExterno(int conceptoExternoValorID);

        Response ActualizarValorExternoConcepto(int conceptoExternoValorID, decimal valorConcepto, int userID);

        Response Eliminar(int conceptoExternoValorID, int userID);

        FileContent ObtenerResultadoLectura(FormatoArchivo formatoArchivo, string fileName);
    }
}