using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Entities;
using InterfacesLibrary;

namespace BusinessLogicLayer
{
    public class Logic
    {
        //TODO: разместить в Dependency Resolver
        readonly IDataAccessLayer _data = new DataBase();
        
        public bool CreateAccount(Account account)
        {
            if (GetAccountByLogin(account.Login) != null)
                return false;

            _data.CreateAccount(account);
            return true;
        }

        public IEnumerable<Account> GetAllAccounts()
            =>_data.GetAllAccounts();

        public Account GetAccountByLogin(string name)
            =>_data.GetAccountByLogin(name);

        public Account GetUserById(Guid id)
            => _data.GetAccountById(id);

        public string[] GetAllRoles()
            => _data.GetAllRoles();

        public Dictionary<int, List<string>> GetRolesOfAccounts()
            => _data.GetRolesOfAccounts();

        public bool UpdateCityAndCountry(Guid id, string newCity, string newCountry)
            => _data.UpdateCityAndCountry(id, newCity, newCountry);

        public bool DeleteAccount(Guid id)
            => _data.DeleteAccount(id);

        public bool UpdateMail(Guid id, string newMail)
            => _data.UpdateMail(id, newMail);

        public bool UpdatePassword(Guid id, string password)
            => _data.UpdatePassword(id, password);

        public bool Update(Account account)
            => _data.Update(account);

        public bool CreatePost(Post post)
            => _data.CreatePost(post);

        public Post GetPost(Guid postid)
            => _data.GetPost(postid);

        public bool UpdateAvatar(Guid accountId, byte[] Avatar, string mimetype)
            => _data.UpdateAvatar(accountId, Avatar, mimetype);
    }
}

