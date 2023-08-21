using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class UsuarioServiceFacade : IUsuarioServiceFacade
    {
        private IUsuarioService _usuarioService;

        public UsuarioServiceFacade()
        {
            _usuarioService = new UsuarioService();
        }

        public Response CambiarEstado(int userID, bool estaHabilitado, int currentUserID)
        {
            var result = _usuarioService.CambiarEstado(userID, estaHabilitado, currentUserID);

            return result;
        }

        public Response GrabarUsuario(Operacion operacion, UsuarioModel usuarioModel, int userID)
        {
            Response response;

            try
            {
                var usuarioEntity = new UsuarioEntity()
                {
                    userId = usuarioModel.userId,
                    datosUsuarioID = usuarioModel.datosUsuarioID,
                    userName = usuarioModel.userName,
                    numDoc = usuarioModel.numDoc,
                    nomPersona = usuarioModel.nomPersona,
                    correoUsuario = usuarioModel.correoUsuario,
                    roleId = usuarioModel.roleId,
                    dependenciaID = usuarioModel.dependenciaID
                };

                response = _usuarioService.GrabarUsuario(operacion, usuarioEntity, userID);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message
                };
            }

            return response;
        }

        public List<UsuarioModel> ListarUsuarios()
        {
            var lista = _usuarioService.ListarUsuarios(true)
                .Select(x => Mapper.UsuarioDTO_To_UsuarioModel(x))
                .ToList();

            return lista;
        }

        public UsuarioModel ObtenerUsuario(int userID)
        {
            UsuarioModel usuarioModel;

            var usuarioDTO = _usuarioService.ObtenerUsuario(userID);

            if (usuarioDTO == null)
            {
                usuarioModel = null;
            }
            else
            {
                usuarioModel = Mapper.UsuarioDTO_To_UsuarioModel(usuarioDTO);
            }

            return usuarioModel;
        }

        public Response ReestablecerPassword(int userID)
        {
            var response = _usuarioService.ReestablecerPassword(userID);
            
            return response;
        }
    }
}