using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomMvcSolution.Web.Infrastructure.Abstract;
using CustomMvcSolution.Web.Models;

namespace CustomMvcSolution.Web.Controllers
{
    public class AccountController : BaseController
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
            return Redirect(returnUrl ?? Url.Action("Index", "Home"));
        }

        public ActionResult SignOut(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }

                ModelState.AddModelError("", "Incorrect username or password");
                return View();
            }

            return View();
        }
    }
}
