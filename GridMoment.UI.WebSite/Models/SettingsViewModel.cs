using System;
using System.ComponentModel.DataAnnotations;

namespace GridMoment.UI.WebSite.Models
{
    public class SettingsViewModel
    {
        public Guid Id { get; set; }

        [Required, Display(Name = "Имя"), StringLength(500, MinimumLength = 3)]
        public string Name { get; set; }

        [Required, Display(Name = "Адрес электронной почты"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, Display(Name = "Город"), StringLength(500, MinimumLength = 3)]
        public string City { get; set; }

        [Required, Display(Name = "Страна"), StringLength(500, MinimumLength = 3)]
        public string Country { get; set; }

        [Required, StringLength(100, MinimumLength = 3), Display(Name = "Пароль"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, StringLength(100, MinimumLength = 3), Display(Name = "Пароль"), DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, StringLength(100, MinimumLength = 3), Display(Name = "Пароль"), DataType(DataType.Password)]
        public string NewPasswordRepeat { get; set; }
    }
}