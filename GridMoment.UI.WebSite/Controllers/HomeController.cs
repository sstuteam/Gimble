using System.Web.Mvc;

namespace GridMoment.UI.WebSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }
    }
}