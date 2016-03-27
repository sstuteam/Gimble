using BusinessLogicLayer;
using Entities;
using System;
using System.Collections.Generic;

namespace GridMoment.UI.WebSite.Controllers
{
    /// <summary>
    /// Класс для связывания контроллеров и логики приложения.
    /// </summary>
    public class Adapter
    {
        /// <summary>
        /// Экзземпляр логики приложения
        /// </summary>
        private static Logic _logic;

        /// <summary>
        /// закрытый конструктор X-DDDD
        /// </summary>
        private Adapter()
        {  }

        /// <summary>
        /// Инициализация класса логики(Логика будет только одна)
        /// </summary>
        public static void Init()
        {
            if (_logic == null)
                _logic = new Logic();
        }
               
        //cодержание биндера
        #region Предоставление логики контроллеру 
        public static Account GetAccount(string name)
            => _logic.GetAccountByLogin(name, false);

        public static Account CheckAccount(string name)
            => _logic.GetAccountByLogin(name, true); 

        public static Account GetAccount(Guid id)
            => _logic.GetUserById(id);

        public static bool CreateUserAndAccount(Account account)
        {
            if (_logic.CreateAccount(account))
                return true;
            return false;
        }

        public static bool ChangePassword(Guid id, string password)
            => _logic.UpdatePassword(id, password);

        public static bool ChangeMail(Guid id, string mail)       
            =>_logic.UpdateMail(id, mail);

        public static bool CreatePost(Post post)
            => _logic.CreatePost(post);

        public static Post GetPost(Guid posid)
            => _logic.GetPost(posid);
        
        public static bool ChangeAvatar(Guid accountId, byte[] avatar, string mimetype)
            => _logic.UpdateAvatar(accountId, avatar, mimetype);

        public static IEnumerable<Post> List7Times()
            => _logic.Get7DaysPopular();

        public static IEnumerable<Post> List30Times()
            => _logic.Get30DaysPopular();

        public static IEnumerable<Post> ListUsersPosts(string name)
            => _logic.GetUsersPosts(name);

        public static List<Post> ListOfLatestPosts()
        {
            List<Post> _resultList = new List<Post>();
            foreach (var item in _logic.GetLatestPosts())
            {
                _resultList.Add(item);
            }
            return _resultList;
        }
        #endregion   
    }
}
