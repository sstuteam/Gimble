using GridMoment.UI.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridMoment.UI.WebSite.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            if (Adapter.CheckRulesAdmin(User.Identity.Name))
            {
                var model = Adapter.GetAllAccounts();
                var roles = Adapter.GetAllRolesOfAccounts();
                var joinModelAndRoles = from item in model
                                        join role in roles on item.Id equals role.Key
                                        select new AccountsPrevievViewModel()
                                        {
                                             Id = item.Id,
                                             Login = item.Login,
                                             Name = item.Name,
                                             Role = role.Value
                                        };
                if (Request.IsAjaxRequest())
                {
                    return PartialView(joinModelAndRoles);
                }
                return View(joinModelAndRoles);
            }
            return RedirectToAction("Index", "Home");            
        }

        [Authorize]
        public ActionResult Delete(string name)
        {            
            Adapter.DeleteAccount(name);
            
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult UpdateRole(Guid accountId)
        {
            if (Adapter.CheckRulesAdmin(User.Identity.Name))
            {
                var success = Adapter.UpdateRole(accountId, 2); //2 = код роли модератора
                return RedirectToAction("Index");                
            }
            return RedirectToAction("Index", "Home");                 
        }
    }
}