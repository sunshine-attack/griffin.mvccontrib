using System.Web.Mvc;

namespace SunshineAttack.Localization.Admin.TestProject.Areas.TestArea
{
    public class TestAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TestArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TestArea_default",
                "TestArea/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                new[] {GetType().Namespace + ".Controllers"}
                );
        }
    }
}
