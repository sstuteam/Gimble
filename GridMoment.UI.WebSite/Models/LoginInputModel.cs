using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridMoment.UI.WebSite.Models
{
    public class LoginInputModel
    {
        [Required, Display(Name = "Логин")]
        public string Login { get; set; }

        [Required, Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}