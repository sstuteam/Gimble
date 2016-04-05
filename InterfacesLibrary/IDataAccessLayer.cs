using System;
using System.Collections.Generic;
using Entities;

namespace InterfacesLibrary
{
    public interface IDataAccessLayer
    {
        bool CreateAccount(Account account);

        Account GetAccountById(Guid id);

        IEnumerable<Account> GetAllAccounts();

        string[] GetAllRoles();

        Dictionary<Guid, List<string>> GetRolesOfAccounts();

        Account GetAccountByLogin(string username, bool checkExisting);
        
        bool UpdatePassword(Guid id, string password);

        bool UpdateMail(Guid id, string newMail);
      
        bool UpdateCityAndCountry(Guid id, string newCity, string newCountry);

        bool DeleteAccount(Guid id);

        bool CreatePost(Post post);

        Post GetPost(Guid postId);

        List<Post> GetAllPosts();

        bool UpdateAvatar(Guid accountId, byte[] avatar, string mimetype);

        List<Comment> GetComents(Guid postId);

        bool CreateComment(Comment comment);

        bool UpdateComment(Comment comment);

        Guid GetIdByName(string login);

        bool SetLike(Guid postId, Guid accountId); 

        IEnumerable<Guid> GetLikedByUser(Guid accountId);

        Dictionary<bool, int> GetLikes(Guid postId, Guid currentUser);
    }
}
