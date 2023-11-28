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
    public interface ITrabajadorCategoriaPlanillaService
    {
        Response GrabarTrabajadorCategoriaPlanilla(Operacion operacion, TrabajadorCategoriaPlanillaEntity trabajadorCategoriaPlanillaEntity, int userID);

        List<TrabajadorCategoriaPlanillaDTO> ListarCategoriaPlanillaPorTrabajador(int trabajadorID);

        List<TrabajadorCategoriaPlanillaDTO> ListarTrabajadoresCategoriaPlanilla(int? categoriaPlanillaID = null);

        TrabajadorCategoriaPlanillaDTO ObtenerTrabajadorCategoriaPlanilla(int trabajadorCategoriaPlanillaID);

        TrabajadorCategoriaPlanillaDTO ObtenerTrabajadorPorDocumentoYCategoria(int tipoDocumentoID, string numDocumento, int categoriaPlanillaID);

        Response CambiarEstado(int trabajadorCategoriaPlanillaID, bool estaHabilitado, int userID);

        Response Eliminar(int trabajadorCategoriaPlanillaID, int userID);

        CategoriaPlanilla ObtenerCategoriaPlanillaSegunVinculo(int vinculoID);
    }
}
