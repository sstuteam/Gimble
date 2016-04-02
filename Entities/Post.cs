using System;

namespace Entities
{
    /// <summary>
    /// Сущность, описывающая пост.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Фотография Пользователя
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        /// Тип файла фотографии пользователя
        /// </summary>
        public string MimeTypeAvatar { get; set; }

        /// <summary>
        /// Id аккаунта автора поста.
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Уникальный идентификатор поста
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Текстовое содержание поста
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Имя поста
        /// </summary>
        public string NamePost { get; set; }

        /// <summary>
        /// Основная картинка поста
        /// </summary>
        public byte[] Image { get; set; }
        
        /// <summary>
        /// мime туре основной картинки поста.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Список меточек к данному посту
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// Имя автора поста
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Дата создания. Нужна для выборок на главной.
        /// </summary>
        public DateTime CreatedTime { get; set; }

       

       

        /// <summary>
        /// Колчиество лайков данного поста.
        /// </summary>
        public int Rating { get; set; }
    }
}
