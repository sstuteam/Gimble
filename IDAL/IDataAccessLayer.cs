using Entities;
using System;
using System.Collections.Generic;

namespace InterfacesLibrary
{
    public interface IDataAccessLayer
    {
        /// <summary>
        /// Добавление аккаунта в базу данных
        /// </summary>
        /// <param name="account">Аккаунт пользователя(предполагается ваилдность)</param>
        /// <returns>Успешность операции</returns>
        bool CreateAccount(Account account);

        /// <summary>
        /// Получение экземпляра класса Account по Guid пользователя
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <returns>Account</returns>
        Account GetAccountById(Guid id);

        /// <summary>
        /// Получение Перечисления всех аккаунтов.
        /// </summary>
        /// <returns>Перечисление аккаунтов</returns>
        IEnumerable<Account> GetAllAccounts();

        /// <summary>
        /// Получение списка всех ролей
        /// </summary>
        /// <returns>Массив ролей</returns>
        string[] GetAllRoles();

        /// <summary>
        /// Функция для получения всех ролей аккаунтов.
        /// </summary>
        /// <returns></returns>
        Dictionary<Guid, string[]> GetRolesOfAccounts();

        /// <summary>
        /// Функция для получения данных аккаунта по логину
        /// </summary>
        /// <param name="username">Параметр - login пользователя</param>
        /// <param name="checkExisting">Флаг, если установить true, то результатом вызова будет аккаунт
        ///  с минимальным число полей, например, для проверки на существование.
        /// Значение флага false эквивлентно возврату всех полей аккаунта. </param>
        /// <returns>Аккаунт пользователя</returns>
        Account GetAccountByLogin(string username, bool checkExisting);

        /// <summary>
        /// Функция для смены пароля по имеющемуся id.
        /// Вызов подразумевается от аккаунта авторизованного пользователя.
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="password">Новый пароль</param>
        /// <returns>Успешность операции</returns>
        bool UpdatePassword(Guid id, string password);

        /// <summary>
        /// Функция для смены пароля по имеющемуся id.
        /// Вызов подразумевается от аккаунта авторизованного пользователя.
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="newName">Новое имя</param>
        /// <returns>Успешность операции</returns>
        bool UpdateName(Guid id, string newName);

        /// <summary>
        /// Функция для смены почтового ящика по имеющемуся id.
        /// Вызов подразумевается от аккаунта авторизованного пользователя.
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="newMail">Новый EMail</param>
        /// <returns>Успешность операции</returns>
        bool UpdateMail(Guid id, string newMail);

        /// <summary>
        /// Функция для смены города и страны.
        /// Вызов подразумевается от аккаунта авторизованного пользователя.
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <param name="newCity">Новый город</param>
        /// /// <param name="newCountry">Новыая страна</param>
        /// <returns>Успешность операции</returns>
        bool UpdateCityAndCountry(Guid id, string newCity, string newCountry);

        /// <summary>
        /// Удаление аккаунта из базы данных и его постов 
        /// </summary>
        /// <param name="id">Guid пользователя</param>
        /// <returns>Успешность операции</returns>
        bool DeleteAccount(string name);

        /// <summary>
        /// Функция добавления поста.
        /// </summary>
        /// <param name="post">Экзмемляр класса Post(Доменной модели)</param>
        /// <returns>Успешность операции</returns>
        bool CreatePost(Post post);

        /// <summary>
        /// Получение поста по Guid поста
        /// </summary>
        /// <param name="postId">Guid поста</param>
        /// <returns>Экземпляр класса Post</returns>
        Post GetPost(Guid postId);

        /// <summary>
        /// Получение всех постов.
        /// </summary>
        /// <returns>Список постов</returns>
        List<Post> GetAllPosts();

        /// <summary>
        /// Функция смены аватара на странице.
        /// </summary>
        /// <param name="accountId">Guid пользователя</param>
        /// <param name="Image">Массив байтов от фатографии</param>
        /// <param name="mimeType">mimeType для операции преобразования массива байтов в фотографию</param>
        /// <returns></returns>
        bool UpdateAvatar(Guid accountId, byte[] avatar, string mimetype);

        /// <summary>
        /// Получить список всех комментариев к данному посту
        /// </summary>
        /// <param name="postId">Уникальный идентификатор поста</param>
        /// <returns>Коллекцию всех комментариев</returns>
        List<Comment> GetComents(Guid postId);

        /// <summary>
        /// Добавление комментария
        /// </summary>
        /// <param name="comment">Экзменпляр класса Comment</param>
        /// <returns>Успешность операции</returns>
        bool CreateComment(Comment comment);

        /// <summary>
        /// Удаление комментария по guid комментария
        /// </summary>
        /// <param name="comid">Уник. идент. комментария</param>
        void DeleteComment(Guid comid);

        /// <summary>
        /// Возвращает Идентификатор пользователя по его логину
        /// </summary>
        /// <param name="login">Лоинг пользователя</param>
        /// <returns>Guid пользователя</returns>
        Guid GetIdByName(string login);

        /// <summary>
        /// Функция установки лайка
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        bool SetLike(Guid postId, Guid accountId);

        /// <summary>
        /// Получить список идентификаторов постов, лайнкутых пользователем
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        IEnumerable<Guid> GetLikedByUser(string modelName);

        /// <summary>
        /// Получить лайки к данному посту, и лайкнут ли пост данным пользователем
        /// </summary>
        /// <param name="postId">Идентификатор поста</param>
        /// <param name="accountId">Идентификатор пользователя</param>
        /// <returns>Пару - лайнкул ли этот пользователь и число лайков</returns>
        Dictionary<bool, int> GetLikes(Guid postId, Guid currentUser);

        /// <summary>
        /// Функция для мены типа учётной записи
        /// Предполагается для использования в панели администрации ресурса
        /// </summary>
        /// <param name="accountId">Идентификатор пользователя</param>
        /// <param name="roleCode">Тип роли - 1-пользователь, 2-модертаор, 3-админ</param>
        /// <returns></returns>
        bool UpdateRole(Guid accountId, int roleCode);

        /// <summary>
        /// Создание аакаунта админа
        /// </summary>
        /// <param name="account">Данные аккаунта админа</param>
        void CreateAdmin(Account account);

        /// <summary>
        /// Регистрация 3-х основных ролей участников системы
        /// </summary>
        int RegisterRoles();
    }
}
