using System.Web.Mvc;
using SunshineAttack.Localization.Localization;

namespace SunshineAttack.Localization.Admin.TestProject.Controllers
{
    [Localized]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
