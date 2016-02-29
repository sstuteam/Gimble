using BusinessLogicLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridMoment.UI.WebSite.Controllers
{
    public class Adapter
    {        
        public static Account GetAccount(string name)
        {
            Logic logic = new Logic();

            var toReturn = logic.GetAccountByLogin(name);

            return toReturn;
        }

        public static Account GetAccount(Guid id)
        {
            Logic logic = new Logic();

            var toReturn = logic.GetUserById(id);

            return toReturn;
        }

        public static bool CreateUserAndAccount(Account account)
        {
            Logic logic = new Logic();

            if (logic.CreateAccount(account))
            {
                return true;
            }

            return false;
        }
    }
}
