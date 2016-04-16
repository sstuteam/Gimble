using BusinessLogicLayer;
using Entities;
using GridMoment.UI.WebSite.Models;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace GridMoment.UI.WebSite.Controllers
{
    /// <summary>
    /// Класс для связывания контроллеров и логики приложения.
    /// </summary>
    public class Adapter
    {
        /// <summary>
        /// Экземпляр логики приложения
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

        /// <summary>
        /// Проверить, если действует админ или модератер
        /// </summary>
        /// <param name="name">Имя(использовать User.Identity.Name)</param>
        /// <returns></returns>
        public static bool CheckRules(string name)
        {
            var account = Adapter.GetAccount(name);
            var hasRights = false;

            foreach (var item in account.Role)
            {
                if (item.Equals("Moder") || item.Equals("Admin"))
                    hasRights = true;
            }
            return hasRights;
        }

        //cодержание биндера
        #region Предоставление логики контроллеру 

        public static Account GetAccount(string name)
        {
            if (name != null)
               return _logic.GetAccountByLogin(name, false);

            return null;           
        }

        public static Account CheckAccount(string name)
            => _logic.GetAccountByLogin(name, true); 

        public static Account GetAccount(Guid accountId)
        {
            if (accountId != null && accountId != Guid.Empty)
                return _logic.GetAccountById(accountId);

            return null;
        }

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

        public static string GetSHA256(string source)
            => _logic.GetSHA256(source);

        public static Photo GetSourceOfPost(Guid postId)
           => _logic.GetPostsSource(postId);

        public static Photo GetAvatar(string name)
            => _logic.GetAvatar(name);

        public static List<CommentViewModel> GetComments(Guid postId)
            => Mapper.Map<List<CommentViewModel>>(_logic.GetComments(postId));
               
        public static bool CreateComment(CommentViewModel comment)
            => _logic.CreateComment(Mapper.Map<Comment>(comment));

        public static Guid GetIdByName(string name)
            => _logic.GetIdByName(name);

        public static Dictionary<bool, int> GetLikes(Guid postId, Guid accountId)
            => _logic.GetLikes(postId, accountId);

        public static bool SetLike(Guid postId, Guid accountId)
            => _logic.SetLike(postId, accountId);

        public static void DeleteComment(Guid comid)
            => _logic.DeleteComment(comid);

        public static List<PostViewModel> GetLikedPost(string modelName)
            => Mapper.Map<List<PostViewModel>>(_logic.GetBookmarks(modelName));

        #endregion
    }
}

