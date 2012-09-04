using System.Web.Mvc;

namespace CustomMvcSolution.WebUI.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}