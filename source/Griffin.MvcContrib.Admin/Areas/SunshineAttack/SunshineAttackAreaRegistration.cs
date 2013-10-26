using System.Web.Mvc;

namespace SunshineAttack.Localization.Areas.SunshineAttack
{
    public class SunshineAttackAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "SunshineAttack"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SunshineAttack_default",
                "SunshineAttack/{controller}/{action}/{id}",
                new { controller = "SunshineAttackHome", action = "Index", id = UrlParameter.Optional },
                new[] {GetType().Namespace + ".Controllers"}
                );
        }
    }
}