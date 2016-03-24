using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GridMoment.UI.WebSite.Models
{
    public class PostViewModel
    {
        public byte[] Avatar { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор поста. 
        /// </summary>
        public Guid PostId { get; set; }

        [StringLength(250), Display(Name = "Текст поста")]
        public string Text { get; set; }

        [Required, StringLength(250), Display(Name = "Заголовок поста")]
        public string NamePost { get; set; }

        /// <summary>
        /// Основная картеначка
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Тип картинки
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Меточки для т. .
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Меточки для т. .
        /// </summary>
        public string TagsAddiction { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string Author { get; set; }
    }
}