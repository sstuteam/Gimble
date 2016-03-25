using GridMoment.UI.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridMoment.UI.WebSite.Controllers
{
    public class ProfileController : Controller
    {
       
        public ActionResult Index()
        {
            var account = Adapter.GetAccount(User.Identity.Name);
            var model = new ProfileShowNameViewModel { Name = account.Name, City = account.City, Role = account.Role };
            return View(model);
        }

        
        public ActionResult ViewProfile(string login)
        {
            var account = Adapter.GetAccount(login);
           
            if (account == null)
            {
                return HttpNotFound();
            }

            var model = new ProfileShowNameViewModel { Name = account.Name, City = account.City, Role = account.Role };

            return View(model);
        }

        public ActionResult CreatePost()
        {
            return View();
        }
    }
}