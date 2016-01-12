using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace InterfacesLibrary
{
    public interface IDataAccessLayer
    {
        bool CreateAccount(Account obj);
        bool Update(Account obj);
        bool DeleteAccount(int id);
        Account GetAccountById(int id);
        Account GetAccountByLogin(string login);
        IEnumerable<Account> GetAllAccounts();
        string[] GetAllRoles();
        Dictionary<int, List<string>> GetRolesOfAccounts();
    }
}
