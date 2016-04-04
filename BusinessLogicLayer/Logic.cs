using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using Entities;
using InterfacesLibrary;
using System.Text;

namespace BusinessLogicLayer
{
    /// <summary>
    /// Основная логика приложения.
    /// </summary>
    public class Logic
    {
        //TODO: разместить в Dependency Resolver
        readonly IDataAccessLayer _data = new DataBase();
        
        /// <summary>
        /// Создание аккаунта
        /// </summary>
        /// <param name="account">Экземпляр класса Account</param>
        /// <returns>Успешность операции</returns>
        public bool CreateAccount(Account account)
        {
            if (GetAccountByLogin(account.Login, true) != null)
                return false;

            _data.CreateAccount(account);
            return true;
        }

        /// <summary>
        /// Получить перечиление всех аккаунтов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Account> GetAllAccounts()
            =>_data.GetAllAccounts();

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
        public Account GetAccountByLogin(string name, bool checking)
            =>_data.GetAccountByLogin(name, checking);

        /// <summary>
        /// Получение полного аккаунта по id
        /// </summary>
        /// <param name="id">Guid, соответсвующий id пользователя</param>
        /// <returns></returns>
        public Account GetUserById(Guid id)
            => _data.GetAccountById(id);

        /// <summary>
        /// Получение всх ролей
        /// </summary>
        /// <returns>Список возможный ролей участников проекта</returns>
        public string[] GetAllRoles()
            => _data.GetAllRoles();

        /// <summary>
        /// Возвращает словарь всех аккаунтов со списком их ролей
        /// </summary>
        /// <returns>словарь всех аккаунтов со списком их ролей</returns>
        public Dictionary<Guid, List<string>> GetRolesOfAccounts()
            => _data.GetRolesOfAccounts();

        /// <summary>
        /// Возвможность в настройках поменять свои географические 
        /// данные
        /// </summary>
        /// <param name="id">Guid пользоваетеля</param>
        /// <param name="newCity">Новый город</param>
        /// <param name="newCountry">Новая страна</param>
        /// <returns></returns>
        public bool UpdateCityAndCountry(Guid id, string newCity, string newCountry)
            => _data.UpdateCityAndCountry(id, newCity, newCountry);

        /// <summary>
        /// Полное удаление аккаунта по id,
        /// с удалением постов данного пользователя
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <returns></returns>
        public bool DeleteAccount(Guid id)
            => _data.DeleteAccount(id);

        /// <summary>
        /// Возможность сменить адрес почты в своём аккаунте
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="newMail">Новый адрес почты</param>
        /// <returns></returns>
        public bool UpdateMail(Guid id, string newMail)
            => _data.UpdateMail(id, newMail);

        /// <summary>
        /// Функция смены пароля. Использовать, если пользователь авторизован в системе.
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="password">Новый пароль</param>
        /// <returns>Успешность операции</returns>
        public bool UpdatePassword(Guid id, string password)
            => _data.UpdatePassword(id, password);

        /// <summary>
        /// Создание поста. Принимается аргумент - экзмепляр класса Post
        /// </summary>
        /// <param name="post">Экзмепляр класса Post</param>
        /// <returns>успешность операции</returns>
        public bool CreatePost(Post post)
            => _data.CreatePost(post);

        /// <summary>
        /// Возвращает экземпляр класса Post, имеющий данный id.
        /// </summary>
        /// <param name="postid">PostId - уникальный идентификатор поста.</param>
        /// <returns>Экзмепляр класса Post</returns>
        public Post GetPost(Guid postid)
            => _data.GetPost(postid);

        /// <summary>
        /// Возможность вмены аватара пользователя.
        /// Требуется указать Guid пользователя, массив байтов, 
        /// и mime type данного файла.
        /// </summary>
        /// <param name="accountId">Guid пользователя</param>
        /// <param name="Avatar">Массив байтов</param>
        /// <param name="mimetype">mime type файла</param>
        /// <returns></returns>
        public bool UpdateAvatar(Guid accountId, byte[] Avatar, string mimetype)
            => _data.UpdateAvatar(accountId, Avatar, mimetype);

        /// <summary>
        /// Для главной. Выбираются последние посты, созданные не более 
        /// чем за 7 дней до текущего момента с наибольшим рейтингом.
        /// </summary>
        /// <returns>Перечиление постов</returns>
        public IEnumerable<Post> GetLatestPosts()
        {
            var data = _data.GetAllPosts();

            var result = from item in data
                         orderby item.CreatedTime
                         select item;
            return result.ToList();         
        }

        /// <summary>
        /// Для главной. Выбираются лучшие посты, созданные не более 
        /// чем за 30 дней до текущего момента с наибольшим рейтингом.
        /// </summary>
        /// <returns>Перечиление постов</returns>
        public IEnumerable<Post> Get30DaysPopular()
        {
            var currentData = DateTime.Now;

            var dateTimeComparate = currentData.Day - 30;

            var data = _data.GetAllPosts();

            var result = from item in data
                         orderby item.Rating descending
                         where item.CreatedTime.Day >= dateTimeComparate
                         select item;

            return result;
        }

        /// <summary>
        /// Для главной. Выбираются лучшие посты, созданные не более 
        /// чем за 7 дней до текущего момента с наибольшим рейтингом.
        /// </summary>
        /// <returns>Перечиление постов</returns>
        public IEnumerable<Post> Get7DaysPopular()
        {
            var currentData = DateTime.Now;

            var dateTimeComparate = currentData.Day - 7;

            var data = _data.GetAllPosts();

            var result = from item in data
                         orderby item.Rating descending
                         where item.CreatedTime.Day >= dateTimeComparate
                         select item;

            return result;
        }

        /// <summary>
        /// При просмотре постов профиля выберутся те, которые принадлежат этому пользователю
        /// </summary>
        /// <param name="currentUser">Имя пользователя</param>
        /// <returns>Перечиление постов</returns>
        public IEnumerable<Post> GetUsersPosts(string currentUser)
        {
            var data = _data.GetAllPosts();

            var result = from item in data
                         orderby item.CreatedTime descending
                         where item.AuthorName == currentUser
                         select item;

            return result;
        }

        /// <summary>
        /// При нажатии на тег выберутся посты ,содержащие такой тег
        /// </summary>
        /// <param name="currentTag">Тег</param>
        /// <returns>Перечисление постов</returns>
        public IEnumerable<Post> GetByTag(string currentTag)
        {
            var data = _data.GetAllPosts();

            var result = from item in data
                         orderby item.CreatedTime descending
                         where item.Tags.Contains<string>(currentTag)
                         select item;

            return result;
        }
        
        /// <summary>
        /// Получить хэш SHA256
        /// </summary>
        /// <param name="password">Исходный пароль</param>
        /// <returns>Хэш SHA256</returns>
        public string GetSHA256(string password)
        {
            var salt = "Vuhgmfgz";
            var inputString = password + salt;
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();

            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(inputString), 0, Encoding.UTF8.GetByteCount(inputString));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }

        /// <summary>
        /// Получить связку массив байтов и mime type 
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns>Данные изображения</returns>
        public Photo GetAvatar(string name)
        {
            var account = _data.GetAccountByLogin(name, false); //false потому что получаем полную информацию об
            var bytes = account.Avatar;                         //аккаунте
            var mimeType = account.MimeType;

            return new Photo { Image = bytes, MimeType = mimeType };
        }

        /// <summary>
        /// Получение изображения поста.
        /// </summary>
        /// <param name="postId">Идентификатор поста</param>
        /// <returns>Данные изображения</returns>
        public Photo GetPostsSource(Guid postId)
        {
            var postData = _data.GetAllPosts();
            //var post = _data.GetPost(postId); 
            var post = postData.Where(x => x.PostId == postId);
            var bytes = post.FirstOrDefault().Image;           
            var mimeType = post.FirstOrDefault().MimeType;

            return new Photo { Image = bytes, MimeType = mimeType };
        }

        /// <summary>
        /// Получение коллекции комментариев к данному посту
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>Коллекцию комментариев</returns>
        public List<Comment> GetComments(Guid postId)
            => _data.GetComents(postId);

        /// <summary>
        /// Создание комментария
        /// </summary>
        /// <param name="comment">Экземпляр класса Comment</param>
        /// <returns>Успешность операции</returns>
        public bool CreateComment(Comment comment)
            => _data.CreateComment(comment);

        /// <summary>
        /// Обновление комментария
        /// </summary>
        /// <param name="comment">Экзмемпляр класса Comment</param>
        /// <returns>Успешность операции</returns>
        public bool UpdateComment(Comment comment)
            => _data.UpdateComment(comment);

        /// <summary>
        /// Получить Идентификатор пользователя по имени 
        /// аутентификационных данных
        /// </summary>
        /// <param name="login">User.Identity.Name</param>
        /// <returns>Уникальный идентификатор пользоваетля</returns>
        public Guid GetIdByName(string login)
            => _data.GetIdByName(login);
    }
}
