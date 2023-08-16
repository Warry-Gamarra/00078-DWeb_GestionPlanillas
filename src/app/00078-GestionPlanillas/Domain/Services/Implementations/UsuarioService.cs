using Data.Connection;
using Data.Views;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        public Response CambiarEstado(int userID, bool estaHabilitado, int currentUserID)
        {
            Result result;

            try
            {
                result = null;
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    Message = ex.Message
                };
            }

            return Mapper.Result_To_Response(result);
        }

        public Response GrabarUsuario(Operacion operacion, UsuarioEntity usuarioEntity, int currentUserID)
        {
            Result result;

            try
            {
                switch(operacion)
                {
                    case Operacion.Registrar:

                        string password = RandomPassword.Generate(8, RandomPassword.PASSWORD_CHARS_ALPHANUMERIC);
                        result = new Result();
                        break;

                    case Operacion.Actualizar:
                        result = new Result();
                        break;

                    default:
                        result = new Result()
                        {
                            Message = "No se reconoce la operación a realizar."
                        };

                        break;
                }
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    Message = ex.Message
                };
            }

            return Mapper.Result_To_Response(result);
        }

        public List<UsuarioDTO> ListarUsuarios(bool incluirDeshabilitados = false)
        {
            var lista = VW_Usuarios.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.VW_Usuarios_To_UsuarioDTO(x))
                .ToList();

            return result;
        }

        public UsuarioDTO ObtenerUsuario(int userID)
        {
            UsuarioDTO usuarioDTO;

            var view = VW_Usuarios.FindByID(userID);

            if (view == null)
            {
                usuarioDTO = null;
            }
            else
            {
                usuarioDTO = Mapper.VW_Usuarios_To_UsuarioDTO(view);
            }

            return usuarioDTO;
        }
    }
}
