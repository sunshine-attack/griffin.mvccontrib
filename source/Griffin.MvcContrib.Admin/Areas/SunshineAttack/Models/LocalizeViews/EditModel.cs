using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SunshineAttack.Localization.Areas.SunshineAttack.Models.LocalizeViews
{
    public class EditModel
    {
        public string DefaultText { get; set; }
        public string TextKey { get; set; }
        public int LocaleId { get; set; }
        public string Path { get; set; }

        [AllowHtml, Required]
        public string Text { get; set; }
    }
}