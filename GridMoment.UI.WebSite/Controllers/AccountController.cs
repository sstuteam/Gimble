using System.Text;
using System.Web.Mvc;
using GridMoment.UI.WebSite.Models;
using System.Web.Security;
using Entities;

namespace GridMoment.UI.WebSite.Controllers
{
    public class AccountController : Controller
    {
        // GET: информация о странице
        public ActionResult Index(int id)
        {
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
                if ((Adapter.GetAccount(model.Login) != null) && Adapter.GetAccount(model.Login).Password == model.Password)
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
            //IndexShowNameViewModel viewModel = new IndexShowNameViewModel { Name = model.Login };

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

            if (Adapter.GetAccount(model.Login) != null)
            {
                ModelState.AddModelError("Login", "Пользователь с таким логином уже существует!");
            }

            //Чтобы показать новые ошибки
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Account account = new Account
            {
                Login = model.Login,
                Password = model.Password,
                Email = model.Email
            };

            Adapter.CreateUserAndAccount(account);

            FormsAuthentication.SetAuthCookie(account.Login, true);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //подозрение действительно есть, что этого метода не должно быть здесь
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