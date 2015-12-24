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
        {
            return _data.GetAllAccounts();
        }

        public Account GetAccountByLogin(string name)
        {
            return _data.GetAccountByLogin(name);
        }

        public Account GetUserById(int id)
        {
            return _data.GetAccountById(id);
        }

        public string[] GetAllRoles()
        {
            return _data.GetAllRoles();
        }

        public Dictionary<int, List<string>> GetRolesOfAccounts()
        {
            var result = _data.GetRolesOfAccounts();

            return result;
        }
    }
}
}
