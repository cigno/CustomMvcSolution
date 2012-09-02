using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CustomMvcSolution.Helpers.Generic;
using CustomMvcSolution.Web.Infrastructure.Helper;

namespace CustomMvcSolution.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void ExecuteCore()
        {
            string cultureName;
            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
            {
                Debug.Assert(Request.UserLanguages != null, "Request.UserLanguages != null");
                cultureName = Request.UserLanguages[0]; // obtain it from HTTP header AcceptLanguages
            }

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            base.ExecuteCore();
        }

    }
}
