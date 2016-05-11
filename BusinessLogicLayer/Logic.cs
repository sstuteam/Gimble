using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using Entities;
using InterfacesLibrary;
using System.Text;

namespace BusinessLogicLayer
{    
    public class Logic : IBusinessLogicLayer
    {
        #region Fields
        private readonly string _basixCity = "Не указан город";
        private readonly string _basixCountry = "Не указана страна";
        private readonly byte[] _basixAvatar = { 0, 0, 0, 25, 1, 0, 4 };
        private readonly string _basixMimeType = "image/jpeg";
        private readonly IDataAccessLayer _data = new DataBase();
        #endregion

        public bool CreateAccount(Account account)
        {
            if (GetAccountByLogin(account.Login, true) != null)
                return false;

            account.Id = Guid.NewGuid();
            account.CreatedTime = DateTime.Now;

            if (account.Avatar == null)
                account.Avatar = _basixAvatar;
            if (account.City == null)
                account.City = _basixCity;
            if (account.Country == null)
                account.Country = _basixCountry;
            if (account.MimeType == null)
                account.MimeType = _basixMimeType;
            _data.CreateAccount(account);
            return true;
        }

        public IEnumerable<Account> GetAllAccounts()
            =>_data.GetAllAccounts();

        public Account GetAccountByLogin(string name, bool checking)
            =>_data.GetAccountByLogin(name, checking);

        public Account GetUserById(Guid id)
            => _data.GetAccountById(id);

        public string[] GetAllRoles()
            => _data.GetAllRoles();
                
        public Dictionary<Guid, string[]> GetRolesOfAccounts()
            => _data.GetRolesOfAccounts();

        public bool UpdateCityAndCountry(Guid id, string newCity, string newCountry)
            => _data.UpdateCityAndCountry(id, newCity, newCountry);

        public bool DeleteAccount(string name)
            => _data.DeleteAccount(name);

        public bool UpdateMail(Guid id, string newMail)
            => _data.UpdateMail(id, newMail);

        public bool UpdatePassword(Guid id, string password)
            => _data.UpdatePassword(id, password);

        public bool UpdateName(Guid id, string newName)
            => _data.UpdateName(id, newName);
            

        public bool CreatePost(Post post)
        {
            post.Rating = 0;
            post.PostId = Guid.NewGuid();
            var atrAdd = "";
            post.CreatedTime = DateTime.Now;
            var tagsLength = post.Tags.Length;

            //Конвертация меточек в строку для бд
            for (int i = 0; i < tagsLength; i++)
            {
                if (i < tagsLength - 1)
                    atrAdd += post.Tags[i] + ",";
                else
                    atrAdd += post.Tags[i];
            }
            post.tmp = atrAdd;
            return _data.CreatePost(post);
        }

        public Post GetPost(Guid postid)
            => _data.GetPost(postid);

        public bool UpdateAvatar(Guid accountId, byte[] Avatar, string mimetype)
            => _data.UpdateAvatar(accountId, Avatar, mimetype);

        public IEnumerable<Post> GetLatestPosts()
        {
            var data = _data.GetAllPosts();

            var result = from item in data
                         orderby item.CreatedTime
                         select item;

            var list = result.ToList();

            return result.ToList();         
        }

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

        public IEnumerable<Post> GetUsersPosts(string currentUser)
        {
            var data = _data.GetAllPosts();

            var result = from item in data
                         orderby item.CreatedTime descending
                         where item.AuthorName == currentUser
                         select item;

            return result;
        }

        public IEnumerable<Post> GetByTag(string currentTag)
        {
            var data = _data.GetAllPosts();

            var result = from item in data
                         orderby item.CreatedTime descending
                         where item.Tags.Contains<string>(currentTag)
                         select item;

            return result;
        }
        
       
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

        public Photo GetAvatar(string name)
        {
            var account = _data.GetAccountByLogin(name, false); //false потому что получаем полную информацию об
            var bytes = account.Avatar;                         //аккаунте
            var mimeType = account.MimeType;

            return new Photo { Image = bytes, MimeType = mimeType };
        }

        
        public Photo GetPostsSource(Guid postId)
        {
            var postData = _data.GetAllPosts();
            //var post = _data.GetPost(postId); 
            var post = postData.Where(x => x.PostId == postId);
            var bytes = post.FirstOrDefault().Image;           
            var mimeType = post.FirstOrDefault().MimeType;

            return new Photo { Image = bytes, MimeType = mimeType };
        }

        public List<Comment> GetComments(Guid postId)
            => _data.GetComents(postId);

        public bool CreateComment(Comment comment)
        {
            comment.ComId = Guid.NewGuid();
            comment.CreatedTime = DateTime.Now;

            return _data.CreateComment(comment);
        }

        public Guid GetIdByName(string login)
            => _data.GetIdByName(login);

        public Account GetAccountById(Guid accountId)
            => _data.GetAccountById(accountId);

        public Dictionary<bool, int> GetLikes(Guid postId, Guid accountId)
            => _data.GetLikes(postId, accountId);

        public bool SetLike(Guid postId, Guid accountId)
        {
            var successful = false;
            var dict = GetLikes(postId, accountId);
            foreach (var item in dict)
            {
                if (item.Key == false)
                {
                    successful = _data.SetLike(postId, accountId);
                }
            }      

            return successful;            
        }

        public void DeleteComment(Guid comid)
            => _data.DeleteComment(comid);

        public List<Post> GetBookmarks(string modelName)
        {
            var listOfPosts = new List<Post>();
            var collection = _data.GetLikedByUser(modelName);

            foreach (var item in collection)
            {
                listOfPosts.Add(GetPost(item));
            }

            return listOfPosts;
        }

        public bool UpdateRole(Guid accountId, int roleCode)
        {
            if (roleCode == 2)
                return _data.UpdateRole(accountId, 2); //делаем и3 пользователя модератора
            return false;
        }

        public void CreateAdmin(Account account)
        {
            account.Avatar = _basixAvatar;
            account.CreatedTime = DateTime.Now;
            account.Id = Guid.NewGuid();
            account.MimeType = _basixMimeType;
            var hash = this.GetSHA256(account.Password);
            account.Password = hash;
            _data.CreateAdmin(account);
        }

        public int RegisterRoles()
         => _data.RegisterRoles();       
    }
}

