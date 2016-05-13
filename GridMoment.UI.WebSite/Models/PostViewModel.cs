using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GridMoment.UI.WebSite.Models
{
    public class PostViewModel
    {      
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Идентификатор поста. 
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        [StringLength(250), Display(Name = "Текст поста")]
        public string Text { get; set; }

        /// <summary>
        /// Название поста
        /// </summary>
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
        /// Меточки для добавления
        /// </summary>
        [Display(Name = "Меточки")]
        public string TagsAddiction { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Имя автора поста
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Свойство для просмотра рейтинга
        /// </summary>
        public int Rating { get; set; }
    }
}
