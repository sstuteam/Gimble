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

        Dictionary<int, List<string>> GetRolesOfAccounts();

        Account GetAccountByLogin(string username);

        bool Update(Account account);

        bool UpdatePassword(Guid id, string password);

        bool UpdateMail(Guid id, string newMail);
      
        bool UpdateCityAndCountry(Guid id, string newCity, string newCountry);

        bool DeleteAccount(Guid id);

        bool CreatePost(Post post);

        Post GetPost(Guid postId);
    }
}
