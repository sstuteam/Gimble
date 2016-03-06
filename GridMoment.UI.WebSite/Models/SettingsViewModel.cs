using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridMoment.UI.WebSite.Models
{
    public class SettingsViewModel
    {
        public System.Guid Id { get; set; }

        [Required, Display(Name = "Адресс электронной почты"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required, StringLength(100, MinimumLength = 3), Display(Name = "Пароль"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, StringLength(100, MinimumLength = 3), Display(Name = "Пароль"), DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, StringLength(100, MinimumLength = 3), Display(Name = "Пароль"), DataType(DataType.Password)]
        public string NewPasswordRepeat { get; set; }
    }
}