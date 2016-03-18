using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace GridMoment.UI.WebSite.Models
{
    public class PostViewModel
    {
        public string Avatar { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// Идентификатор поста. 
        /// </summary>
        public System.Guid PostId { get; set; }

        [StringLength(250), Display(Name = "Текст поста")]
        public string Text { get; set; }

        [Required, StringLength(250), Display(Name = "Заголовок поста")]
        public string NamePost { get; set; }

        /// <summary>
        /// Основная картеначка
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Меточки для т. .
        /// </summary>
        public List<string> Tags { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string Author { get; set; }
    }
}