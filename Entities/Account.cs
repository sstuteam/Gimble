namespace Entities
{
    /// <summary>
    /// Сущность, описывающая аккаунт пользователя, его возможности
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Фотография профиля пользователя
        /// </summary>
        public byte [] Avatar { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// Почтовый адрес пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Логин для входа в систему
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Страна/реион пользователя
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Город пользователя
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// список ролей данного пользователя
        /// </summary>
        public string[] Role { get; set; }       
    }
}