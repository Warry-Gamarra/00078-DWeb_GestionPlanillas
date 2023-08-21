using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;
using WebMatrix.WebData;

namespace WebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IUsuarioServiceFacade _usuarioServiceFacade;
        private IRolServiceFacade _rolServiceFacade;
        private IDependenciaServiceFacade _dependenciaServiceFacade;

        public UsersController()
        {
            _usuarioServiceFacade = new UsuarioServiceFacade();
            _rolServiceFacade = new RolServiceFacade();
            _dependenciaServiceFacade = new DependenciaServiceFacade();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Usuarios";

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaUsuarios()
        {
            var result = new AjaxResponse();

            result.data = _usuarioServiceFacade.ListarUsuarios();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nuevo Usuario";

            ViewBag.Action = "Registrar";

            ViewBag.ListaRoles = _rolServiceFacade.ObtenerComboRoles();

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias();

            var usuarioModel = new UsuarioModel();

            return PartialView("_MantenimientoUsuario", usuarioModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(UsuarioModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _usuarioServiceFacade.GrabarUsuario(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarTrabajador", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle del Usuario";

            ViewBag.Action = "Actualizar";

            var usuarioModel = _usuarioServiceFacade.ObtenerUsuario(id);

            ViewBag.ListaRoles = _rolServiceFacade.ObtenerComboRoles(selectedItem: usuarioModel.roleId);

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias(selectedItem: usuarioModel.dependenciaID);

            return PartialView("_MantenimientoUsuario", usuarioModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(UsuarioModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _usuarioServiceFacade.GrabarUsuario(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarTrabajador", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CambiarEstado(int rowID, bool estaHabilitado)
        {
            var result = _usuarioServiceFacade.CambiarEstado(rowID, estaHabilitado, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResetPassword(int id)
        {
            var usuarioModel = _usuarioServiceFacade.ObtenerUsuario(id);

            var result = _usuarioServiceFacade.ReestablecerPassword(id);

            ViewBag.UserName = usuarioModel.userName;

            return PartialView("_ResetPassword", result);
        }
    }
}
