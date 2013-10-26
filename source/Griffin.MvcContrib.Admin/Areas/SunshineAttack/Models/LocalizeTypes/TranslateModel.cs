using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SunshineAttack.Localization.Areas.SunshineAttack.Models.LocalizeTypes
{
    public class TranslateModel
    {
        [Required]
        public string TextKey { get; set; }

        [Required, AllowHtml]
        public string Text { get; set; }
    }
}