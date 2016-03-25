using System.Web.Mvc;
using System.Web.Routing;

namespace GridMoment.UI.WebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);  
        }
    }
}
