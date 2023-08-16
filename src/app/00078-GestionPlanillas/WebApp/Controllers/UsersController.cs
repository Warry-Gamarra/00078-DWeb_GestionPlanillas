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

        public UsersController()
        {
            _usuarioServiceFacade = new UsuarioServiceFacade();
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
    }
}
