using System.Web.Mvc;
using GridMoment.UI.WebSite.Models;
using System.Web.Security;
using Entities;
using AutoMapper;

namespace GridMoment.UI.WebSite.Controllers
{
    public class AccountController : Controller
    {
        // GET: информация о странице
        public ActionResult Index(int id)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }
          
        [AllowAnonymous]      
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginInputModel model, string stringUrl)
        {

            var checking = Adapter.CheckAccount(model.Login);

            if ((checking != null) && (Adapter.GetSHA256(model.Password) == checking.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);

                if (Url.IsLocalUrl(stringUrl))
                {
                    return Redirect(stringUrl);
                }
                else
                {
                    RedirectToAction("Index", "Home");
                }                    
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var acc = Adapter.CheckAccount(model.Login);
            if (acc.Login != null)
            {
                ModelState.AddModelError("Login", "Пользователь с таким логином уже существует!");
            }

            //Чтобы показать новые ошибки
            if (!ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView(model);
                }
                return View(model);
            }

            Account account = new Account
            {
                Login = model.Login,
                Password = Adapter.GetSHA256(model.Password),
                Email = model.Email
            };

            bool success = Adapter.CreateUserAndAccount(account);
            if (success)
            {
                FormsAuthentication.SetAuthCookie(account.Login, true);
            }           

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public FileResult ShowAvatar(string name)
        {
            var image = Mapper.Map<PhotoViewModel>(Adapter.GetAvatar(name));

            return File(image.Image, image.MimeType);
        }
    }       
}
