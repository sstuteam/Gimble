using GridMoment.UI.WebSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GridMoment.UI.WebSite.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            var account = Adapter.GetAccount(User.Identity.Name);

            var model = new SettingsViewModel() { Id = account.Id, Email = account.Email, Password = account.Password };
           
            return View(model);         
        }

        public ActionResult ChangePassword(SettingsViewModel model)
        {
            var account = Adapter.GetAccount(User.Identity.Name);

            if (model.Password.Equals(account.Password) &&
                model.NewPassword.Equals(model.NewPasswordRepeat))
            {
                Adapter.ChangePassword(account.Id, model.NewPassword);
            }

            return RedirectToAction("Index");

        }

        public ActionResult ChangeMail(SettingsViewModel model)
        {
            var account = Adapter.GetAccount(User.Identity.Name);

            Adapter.ChangeMail(account.Id, model.Email);

            return RedirectToAction("Index");

        }

        public ActionResult ChangeAvatar(HttpPostedFileBase uploadImage)
        {
            if (uploadImage == null)
            {
                return HttpNotFound();
            }

            var account = Adapter.GetAccount(User.Identity.Name);            
            string ext = uploadImage.ContentType;
            byte[] Image;    

            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                Image = binaryReader.ReadBytes(uploadImage.ContentLength);
            }

            Adapter.ChangeAvatar(account.Id, Image, ext);

            return RedirectToAction("Index");
            
        }
    }
}