using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models;
using WebApp.ViewModels;
using WebMatrix.WebData;

namespace WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UsersModel _usersModel;

        public AccountController()
        {
            _usersModel = new UsersModel();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Dashboard", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [Route("account/login")]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!Roles.RoleExists("Administrador"))
                Roles.CreateRole("Administrador");

            if (!WebSecurity.UserExists("administrador"))
            {
                WebSecurity.CreateUserAndAccount("administrador", "admin@OCRH", new { B_CambiaPassword = false, B_Habilitado = true, B_Eliminado = false });

                if (!Roles.GetRolesForUser("administrador").Contains("Administrador"))
                    Roles.AddUsersToRoles(new[] { "administrador" }, new[] { "Administrador" });
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(new LoginViewModel());
        }

        [Route("logout")]
        public ActionResult LogOut()
        {
            WebSecurity.Logout();

            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("account/login")]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                WebSecurity.Logout();
            }

            var usuarioResponse = _usersModel.GetUserState(model.UserName);
            if (string.IsNullOrEmpty(usuarioResponse.Message))
            {
                if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "El nombre de usuario o la contraseña son incorrectos.");
                }
            }
            ModelState.AddModelError("", usuarioResponse.Message);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        [Route("seguridad/cambiar-password")]
        public ActionResult CambiarPassword()
        {
            return PartialView("_ChangePassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("seguridad/cambiar-password")]
        public ActionResult CambiarPassword(ChangePasswordViewModel model)
        {
            Response result = new Response();

            if (ModelState.IsValid)
            {
                try
                {
                    result.Success = WebSecurity.ChangePassword(User.Identity.Name, model.CurrentPassword, model.NewPassword);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }

                if (result.Success)
                {
                    result.Message = "Contraseña actualizada correctamente.";
                }
                else
                {
                    if (string.IsNullOrEmpty(result.Message))
                    {
                        result.Message = "El valor ingresado en el campo contraseña no corresponde a la contraseña actual.";
                    }
                }
            }
            else
            {
                string details = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        details += error.ErrorMessage + " / ";
                    }
                }

                result.Message = "Ha ocurrido un error con el envio de datos. " + details;
            }

            return PartialView("_MsgPartial", result);
        }
    }
}