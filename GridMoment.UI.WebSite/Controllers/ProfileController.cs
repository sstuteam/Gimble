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

                if (Request.IsAjaxRequest())
                {
                    return PartialView(model);
                }

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

            var model = new ProfileShowNameViewModel { Login = account.Login, Name = account.Name, City = account.City, Role = account.Role };

            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);
        }
    }
}