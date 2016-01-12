using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GridMoment.UI.WebSite.Models;
using WebMatrix.WebData;

namespace GridMoment.UI.WebSite.Controllers
{
    public class AccountController : Controller
    {
        // GET: информация о странице
        public ActionResult Index(int id)
        {
            return View();
        }

        public ActionResult Login()
        {
            //Выйти, если пользователь залогинен
            if (WebSecurity.IsAuthenticated)
            {
                WebSecurity.Logout();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.IsAccountLockedOut(model.Login, 5, 600))
                {
                    ModelState.AddModelError("Password",
                       "Вы пытались ввести пароль слишком много раз. Вам придеться подождать час, прежде чем попытаться войти снова.");
                }

                if (WebSecurity.Login(model.Login, model.Password, persistCookie: true))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Вы ввели неправильный логин или пароль!");
                }
            }

            //TODO: сделать вывод ошибки 
            //return RedirectToAction("Index", "Home");
            return View(model);
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

            if (WebSecurity.UserExists(model.Login))
            {
                ModelState.AddModelError("Login", "Пользователь с таким логином уже существует!");
            }

            //TODO: добавить проверку существования почты
            //if ()
            //{
            //    ModelState.AddModelError("Email", "Пользователь с таким email уже существует!");
            //}

            //Чтобы показать новые ошибки
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            WebSecurity.CreateUserAndAccount(model.Login, model.Password, new
            {
                Email = model.Email,
                //AddTime = DateTime.Now
            });
            WebSecurity.Login(model.Login, model.Password, persistCookie: true);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        //подозрение, что этого метода не должно быть здесь
        static string GetSHA256(string password, string salt = "Vuhgmfgz")
        {
            string inputString = password + salt;
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(inputString), 0, Encoding.UTF8.GetByteCount(inputString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}