namespace GridMoment.UI.WebSite.Models
{
    public class AccountsPrevievViewModel
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string[] Role { get; set; }

        public string Login { get; set; }
    }
}