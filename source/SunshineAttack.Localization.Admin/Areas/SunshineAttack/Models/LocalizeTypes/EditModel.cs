using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SunshineAttack.Localization.Areas.SunshineAttack.Models.LocalizeTypes
{
    public class EditModel
    {
        [AllowHtml]
        public string DefaultText { get; set; }
        public string TextKey { get; set; }
        public int LocaleId { get; set; }
        public string Path { get; set; }

        [Required, AllowHtml]
        public string Text { get; set; }
    }
}