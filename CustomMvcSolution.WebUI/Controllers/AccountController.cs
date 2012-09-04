using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CustomMvcSolution.WebUI.Infrastructure.Abstract;
using CustomMvcSolution.WebUI.Infrastructure.Concrete;
using CustomMvcSolution.Resource;
using CustomMvcSolution.WebUI.Models;

namespace CustomMvcSolution.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider auth)
        {
            _authProvider = auth;
        }

        public ViewResult SignIn()
        {
            return View( new LogOnViewModel() );
        }

        [HttpPost]
        public ActionResult SignIn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }

                ModelState.AddModelError("", Resources.InvalidCredentials);
                return View(model);
            }

            return View(model);
        }


        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
