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
    public interface IUsuarioService
    {
        Response GrabarUsuario(Operacion operacion, UsuarioEntity usuarioEntity, int currentUserID);

        List<UsuarioDTO> ListarUsuarios(bool incluirDeshabilitados = false);

        UsuarioDTO ObtenerUsuario(int userID);

        Response CambiarEstado(int userID, bool estaHabilitado, int currentUserID);

        Response ReestablecerPassword(int userID);
    }
}
