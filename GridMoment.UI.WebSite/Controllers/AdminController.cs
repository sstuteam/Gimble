using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridMoment.UI.WebSite.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Adapter.CheckRulesAdmin(User.Identity.Name))
            {
                var accounts = Adapter.GetAllAccounts();
                return View(accounts);
            }
            return RedirectToAction("Home", "Index");            
        }

        public ActionResult Delete(string name)
        {            
            Adapter.DeleteAccount(name);
            
            return RedirectToAction("Home", "Index");
        }
    }
}