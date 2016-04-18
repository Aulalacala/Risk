using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Risk.Models;

namespace Risk.Controllers
{
    public class LoginController : Controller
    {

        Usuarios_BDDataContext usuario_BD = new Usuarios_BDDataContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tUsuario tUsuario)
        {
            if (ModelState.IsValid)
            {

                EncritPass encriptar = new EncritPass();
                string passencript = encriptar.Encrit(tUsuario.Clave);

                if (!String.IsNullOrEmpty(passencript))
                {
                    try
                    {

                        tUsuario tUsuarioOK = usuario_BD.tUsuarios.Single(u => u.Usuario == tUsuario.Usuario && u.Clave == passencript && u.Activo == true);

                        if (tUsuarioOK != null && tUsuarioOK.Bloqueado == false)
                        {
                            // OK usuario
                            Session["tUsuario"] = tUsuarioOK;
                            return RedirectToAction("Inicio", "Inicio");
                        }

                        else if (tUsuarioOK.Bloqueado == true)
                        {
                            // Enviar error usuario bloqueado
                            ViewBag.errorUsuarioBloqueado = "Su usuario está bloqueado. Póngase en contacto con el administrador";
                            return View("Login");
                        }

                    }
                    catch
                    {
                        ViewBag.errorNoExisteUsuario = "El usuario no existe.";
                        return View("Login");
                    }
                }
            } else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                return View("Login");
            }


            return View("Login");
        }
    }
}