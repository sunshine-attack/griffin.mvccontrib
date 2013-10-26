using System.ComponentModel.DataAnnotations;

namespace SunshineAttack.Localization.Admin.TestProject.Models
{
    public class HomeViewModel
    {
        [Required()]
        [StringLength(40)]
        public string Name { get; set; }

        [Required, System.ComponentModel.DataAnnotations.Compare("Name", ErrorMessage = "Ange messy")]
        public int Age { get; set; }

        [Required, System.ComponentModel.DataAnnotations.Compare("Password2")]
        public string Password1 { get; set; }


        public string Password2 { get; set; }
    }
}