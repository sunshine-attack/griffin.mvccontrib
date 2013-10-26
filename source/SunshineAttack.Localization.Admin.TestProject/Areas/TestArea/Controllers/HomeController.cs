using System.Web.Mvc;
using SunshineAttack.Localization.Localization;

namespace SunshineAttack.Localization.Admin.TestProject.Areas.TestArea.Controllers
{
    [Localized]
    public class HomeController : Controller
    {
        //
        // GET: /TestArea/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
