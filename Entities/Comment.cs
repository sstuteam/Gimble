using System;

namespace Entities
{
    public class Comment
    {
        /// <summary>
        /// Идентификатор комментария
        /// </summary>
        public Guid ComId { get; set; }

        /// <summary>
        /// Содержимое комментария
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дота создания
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Идентификатор поста, к которому привязан данный комментарий
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Имя автора коментария
        /// </summary>
        public string AuthorName { get; set; }
    }
}
