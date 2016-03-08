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
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public System.Guid Id;
        
        /// <summary>
        /// Идентификатор поста. 
        /// </summary>
        public System.Guid PostId;

        [Required, StringLength(250), Display(Name = "Текст поста")]
        public string Text;

        [Required, StringLength(250), Display(Name = "Заголовок поста")]
        public string NamePost;
                
        public Image Source;
    }
}