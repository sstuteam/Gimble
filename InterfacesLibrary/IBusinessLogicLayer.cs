using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesLibrary
{
    public interface IBusinessLogicLayer
    {
        /// <summary>
        /// Создание аккаунта
        /// </summary>
        /// <param name="account">Экземпляр класса Account</param>
        /// <returns>Успешность операции</returns>
        bool CreateAccount(Account account);

        /// <summary>
        /// Получить перечиление всех аккаунтов
        /// </summary>
        /// <returns></returns>
        IEnumerable<Account> GetAllAccounts();

        /// <summary>
        /// Получение аакаунта по логину и возможность получения частичного аккаунта 
        /// по флагу. Например, параметр true возвращает только данные, необходимые для 
        /// аутентифифкации.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        /// <param name="checking">True для частичного аккаунта
        /// (только id, login, password),
        /// False для всего аккаунта.</param>
        /// <returns></returns>
        Account GetAccountByLogin(string name, bool checking);

        /// <summary>
        /// Получение полного аккаунта по id
        /// </summary>
        /// <param name="id">Guid, соответсвующий id пользователя</param>
        /// <returns></returns>
        Account GetUserById(Guid id);

        /// <summary>
        /// Получение всх ролей
        /// </summary>
        /// <returns>Список возможный ролей участников проекта</returns>
        string[] GetAllRoles();

        /// <summary>
        /// Возвращает словарь всех аккаунтов со списком их ролей
        /// </summary>
        /// <returns>словарь всех аккаунтов со списком их ролей</returns>
        Dictionary<Guid, List<string>> GetRolesOfAccounts();

        /// <summary>
        /// Возвможность в настройках поменять свои географические 
        /// данные
        /// </summary>
        /// <param name="id">Guid пользоваетеля</param>
        /// <param name="newCity">Новый город</param>
        /// <param name="newCountry">Новая страна</param>
        /// <returns></returns>
        bool UpdateCityAndCountry(Guid id, string newCity, string newCountry);

        /// <summary>
        /// Полное удаление аккаунта по id,
        /// с удалением постов данного пользователя
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <returns></returns>
        bool DeleteAccount(Guid id);

        /// <summary>
        /// Возможность сменить адрес почты в своём аккаунте
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="newMail">Новый адрес почты</param>
        /// <returns></returns>
        bool UpdateMail(Guid id, string newMail);

        /// <summary>
        /// Функция смены пароля. Использовать, если пользователь авторизован в системе.
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="password">Новый пароль</param>
        /// <returns>Успешность операции</returns>
        bool UpdatePassword(Guid id, string password);

        /// <summary>
        /// Создание поста. Принимается аргумент - экзмепляр класса Post
        /// </summary>
        /// <param name="post">Экзмепляр класса Post</param>
        /// <returns>успешность операции</returns>
        bool CreatePost(Post post);

        /// <summary>
        /// Возвращает экземпляр класса Post, имеющий данный id.
        /// </summary>
        /// <param name="postid">PostId - уникальный идентификатор поста.</param>
        /// <returns>Экзмепляр класса Post</returns>
        Post GetPost(Guid postid);

        /// <summary>
        /// Возможность вмены аватара пользователя.
        /// Требуется указать Guid пользователя, массив байтов, 
        /// и mime type данного файла.
        /// </summary>
        /// <param name="accountId">Guid пользователя</param>
        /// <param name="Avatar">Массив байтов</param>
        /// <param name="mimetype">mime type файла</param>
        /// <returns></returns>
        bool UpdateAvatar(Guid accountId, byte[] Avatar, string mimetype);

        /// <summary>
        /// Для главной. Выбираются последние посты, созданные не более 
        /// чем за 7 дней до текущего момента с наибольшим рейтингом.
        /// </summary>
        /// <returns>Перечиление постов</returns>
        IEnumerable<Post> GetLatestPosts();

        /// <summary>
        /// Для главной. Выбираются лучшие посты, созданные не более 
        /// чем за 30 дней до текущего момента с наибольшим рейтингом.
        /// </summary>
        /// <returns>Перечиление постов</returns>
        IEnumerable<Post> Get30DaysPopular();

        /// <summary>
        /// Для главной. Выбираются лучшие посты, созданные не более 
        /// чем за 7 дней до текущего момента с наибольшим рейтингом.
        /// </summary>
        /// <returns>Перечиление постов</returns>
        IEnumerable<Post> Get7DaysPopular();

        /// <summary>
        /// При просмотре постов профиля выберутся те, которые принадлежат этому пользователю
        /// </summary>
        /// <param name="currentUser">Имя пользователя</param>
        /// <returns>Перечиление постов</returns>
        IEnumerable<Post> GetUsersPosts(string currentUser);

        /// <summary>
        /// При нажатии на тег выберутся посты ,содержащие такой тег
        /// </summary>
        /// <param name="currentTag">Тег</param>
        /// <returns>Перечисление постов</returns>
        IEnumerable<Post> GetByTag(string currentTag);

        /// <summary>
        /// Получить хэш SHA256
        /// </summary>
        /// <param name="password">Исходный пароль</param>
        /// <returns>Хэш SHA256</returns>
        string GetSHA256(string password);

        /// <summary>
        /// Получить связку массив байтов и mime type 
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns>Данные изображения</returns>
        Photo GetAvatar(string name);

        /// <summary>
        /// Получение изображения поста.
        /// </summary>
        /// <param name="postId">Идентификатор поста</param>
        /// <returns>Данные изображения</returns>
        Photo GetPostsSource(Guid postId);

        /// <summary>
        /// Получение коллекции комментариев к данному посту
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>Коллекцию комментариев</returns>
        List<Comment> GetComments(Guid postId);

        /// <summary>
        /// Создание комментария
        /// </summary>
        /// <param name="comment">Экземпляр класса Comment</param>
        /// <returns>Успешность операции</returns>
        bool CreateComment(Comment comment);

        /// <summary>
        /// Получить Идентификатор пользователя по имени 
        /// аутентификационных данных
        /// </summary>
        /// <param name="login">User.Identity.Name</param>
        /// <returns>Уникальный идентификатор пользоваетля</returns>
        Guid GetIdByName(string login);

        /// <summary>
        /// Получение данных пользователя по его уникальному идентификатору
        /// </summary>
        /// <param name="accountId">Guid пользователя</param>
        /// <returns>Экзмпляр класса Account</returns>
        Account GetAccountById(Guid accountId);

        /// <summary>
        /// Возвращает число лайков и лайкнут ли 
        /// этот пост пользвателем с указанным иднтификатором
        /// </summary>
        /// <param name="postId">Уникальный идентификатор поста</param>
        /// <param name="accountId">Уникальный идентификатор пользователя</param>
        /// <returns>Пару - рейтинг/статус лайка для данного пользователя</returns>
        Dictionary<bool, int> GetLikes(Guid postId, Guid accountId);

        /// <summary>
        /// Добавление в избранное
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        bool SetLike(Guid postId, Guid accountId);

        /// <summary>
        /// Удаление комментария по guid комментария
        /// </summary>
        /// <param name="comid">Уник. идент. комментария</param>
        void DeleteComment(Guid comid);

        /// <summary>
        /// Получить все посты, лайкнутые пользователем
        /// </summary>
        /// <param name="accountId">Уникальный идентификатор пользователя</param>
        /// <returns>Список постов.</returns>
        List<Post> GetBookmarks(string modelName);
    }
}
