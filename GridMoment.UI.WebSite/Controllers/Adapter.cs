using BusinessLogicLayer;
using Entities;
using GridMoment.UI.WebSite.Models;
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
            Logic _logic = new Logic();

            var toReturn = _logic.GetAccountByLogin(name, false);

            return toReturn;
        }

        public static Account CheckAccount(string name)
        {
            Logic _logic = new Logic();

            var toReturn = _logic.GetAccountByLogin(name, true);

            return toReturn;
        }

        public static Account GetAccount(Guid id)
        {
            Logic _logic = new Logic();

            var toReturn = _logic.GetUserById(id);

            return toReturn;
        }

        public static bool CreateUserAndAccount(Account account)
        {
            Logic _logic = new Logic();

            if (_logic.CreateAccount(account))
            {
                return true;
            }

            return false;
        }

        public static bool ChangePassword(Guid id, string password)
        {
            Logic _logic = new Logic();

            return _logic.UpdatePassword(id, password);
        }

        public static bool ChangeMail(Guid id, string mail)
        {
            Logic _logic = new Logic();

            return _logic.UpdateMail(id, mail);
        }

        public static bool CreatePost(Post post)
        {
            Logic _logic = new Logic();

            return _logic.CreatePost(post);
        }

        public static Post GetPost(Guid posid)
        {
            Logic _logic = new Logic();

            return _logic.GetPost(posid);
        }

        public static bool ChangeAvatar(Guid accountId, byte[] avatar, string mimetype)
        {
            Logic _logic = new Logic();

            return _logic.UpdateAvatar(accountId, avatar, mimetype);
        }
    }
}
