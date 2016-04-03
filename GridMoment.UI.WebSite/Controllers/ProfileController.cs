using GridMoment.UI.WebSite.Models;
using System.Web.Mvc;

namespace GridMoment.UI.WebSite.Controllers
{
    public class ProfileController : Controller
    {
       
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var account = Adapter.GetAccount(User.Identity.Name);
                var model = new ProfileShowNameViewModel { Name = account.Name, City = account.City, Role = account.Role };
                return View(model);
            }

         return RedirectToAction("Index", "Home");
            
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