using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IUsuarioServiceFacade
    {
        Response GrabarUsuario(Operacion operacion, UsuarioModel usuarioModel, int userID);

        List<UsuarioModel> ListarUsuarios();

        UsuarioModel ObtenerUsuario(int userID);

        Response CambiarEstado(int userID, bool estaHabilitado, int currentUserID);

        Response ReestablecerPassword(int userID);
    }
}