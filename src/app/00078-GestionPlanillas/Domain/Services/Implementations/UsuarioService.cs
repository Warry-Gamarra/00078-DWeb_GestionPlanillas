using Data.Connection;
using Data.Procedures;
using Data.Tables;
using Data.Views;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebMatrix.WebData;

namespace Domain.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        public Response CambiarEstado(int userID, bool estaHabilitado, int currentUserID)
        {
            Result result;

            try
            {
                var cambiarEstado = new USP_U_ActualizarEstadoUsuario()
                { 
                    UserId = userID,
                    B_Habilitado = !estaHabilitado,
                    CurrentUserId = currentUserID
                };

                result = cambiarEstado.Execute();

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
            DateTime currentDateTime;

            try
            {
                currentDateTime = DateTime.Now;

                switch (operacion)
                {
                    case Operacion.Registrar:

                        if (!WebSecurity.UserExists(usuarioEntity.userName))
                        {
                            string password = RandomPassword.Generate(8, RandomPassword.PASSWORD_CHARS_ALPHANUMERIC);

                            WebSecurity.CreateUserAndAccount(
                                usuarioEntity.userName,
                                password,
                                new
                                {
                                    B_CambiaPassword = false,
                                    B_Habilitado = true,
                                    B_Eliminado = false,
                                    I_UsuarioCre = currentUserID,
                                    D_FecCre = currentDateTime,
                                    I_DependenciaID = usuarioEntity.dependenciaID
                                });

                            int userID = WebSecurity.GetUserId(usuarioEntity.userName);

                            string rolName = Webpages_Roles.FindByID(usuarioEntity.roleId).RoleName;

                            Roles.AddUserToRole(usuarioEntity.userName, rolName);

                            var grabarUsuario = new USP_I_RegistrarDatosUsuario()
                            {
                                UserId = userID,
                                N_NumDoc = usuarioEntity.numDoc,
                                T_NomPersona = usuarioEntity.nomPersona,
                                T_CorreoUsuario = usuarioEntity.correoUsuario,
                                CurrentUserId = currentUserID,
                                D_FecRegistro = currentDateTime
                            };

                            result = grabarUsuario.Execute();

                            result.Message += " Se generó la siguiente contraseña: <b>" + password + "</b>";
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = "El usuario " + usuarioEntity.userName + " ya existe."
                            };
                        }
                        
                        break;

                    case Operacion.Actualizar:

                        if (usuarioEntity.userId.HasValue && usuarioEntity.datosUsuarioID.HasValue)
                        {
                            if (WebSecurity.UserExists(usuarioEntity.userName))
                            {
                                var currentRol = Roles.GetRolesForUser(usuarioEntity.userName).ToArray();

                                Roles.RemoveUserFromRoles(usuarioEntity.userName, currentRol);

                                string newRolName = Webpages_Roles.FindByID(usuarioEntity.roleId).RoleName;

                                Roles.AddUserToRole(usuarioEntity.userName, newRolName);

                                var actualizarUsuario = new USP_U_ActualizarDatosUsuario()
                                {
                                    UserId = usuarioEntity.userId.Value,
                                    I_DatosUsuarioID = usuarioEntity.datosUsuarioID.Value,
                                    N_NumDoc = usuarioEntity.numDoc,
                                    T_NomPersona = usuarioEntity.nomPersona,
                                    T_CorreoUsuario = usuarioEntity.correoUsuario,
                                    I_DependenciaID = usuarioEntity.dependenciaID,
                                    CurrentUserId = currentUserID,
                                    D_FecMod = currentDateTime
                                };

                                result = actualizarUsuario.Execute();
                            }
                            else
                            {
                                result = new Result()
                                {
                                    Message = "El usuario " + usuarioEntity.userName + " no ya existe."
                                };
                            }
                        }
                        else
                        {
                            result = new Result()
                            {
                                Message = "Ocurrió un error al validar el identificador del usuario."
                            };
                        }
                            
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
