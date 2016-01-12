using System.ComponentModel.DataAnnotations;

namespace GridMoment.UI.WebSite.Models
{
    public class RegisterInputModel
    {
        [Required, StringLength(100, MinimumLength = 3), EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100, MinimumLength = 3), Display(Name = "Логин")]
        public string Login { get; set; }

        [Required, StringLength(100, MinimumLength = 3), Display(Name = "Пароль"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}