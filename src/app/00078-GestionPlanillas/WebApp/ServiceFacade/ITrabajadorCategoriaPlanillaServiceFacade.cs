using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface ITrabajadorCategoriaPlanillaServiceFacade
    {
        Response GrabarTrabajadorCategoriaPlanilla(Operacion operacion, TrabajadorCategoriaPlanillaModel trabajadorCategoriaPlanillaModel, int userID);

        List<TrabajadorCategoriaPlanillaModel> ListarCategoriaPlanillaPorTrabajador(int trabajadorID);

        List<TrabajadorCategoriaPlanillaModel> ListarTrabajadoresAptos(int anio, int mes, int categoriaPlanillaID);

        FileContent ListarTrabajadoresAptos(int año, int mes, int categoriaPlanillaID, FormatoArchivo formatoArchivo);

        List<TrabajadorCategoriaPlanillaModel> ListarTrabajadoresAsignadosCategoria(int categoriaPlanillaID);

        TrabajadorCategoriaPlanillaModel ObtenerTrabajadorCategoriaPlanilla(int trabajadorCategoriaPlanillaID);

        Response CambiarEstado(int trabajadorCategoriaPlanillaID, bool estaHabilitado, int userID);

        Response Eliminar(int trabajadorCategoriaPlanillaID, int userID);

        CategoriaPlanilla ObtenerCategoriaPlanillaSegunVinculo(int vinculoID);       
    }
}