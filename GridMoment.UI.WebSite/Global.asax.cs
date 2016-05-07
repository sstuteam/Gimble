using GridMoment.UI.WebSite.App_Start;
using GridMoment.UI.WebSite.Controllers;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace GridMoment.UI.WebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Adapter.Init();
            RolesConfig.Init();
            var adminLogin = ConfigurationManager.AppSettings.GetValues("AdminDefault").FirstOrDefault();
            var adminPassword = ConfigurationManager.AppSettings.GetValues("AdminDefaultPassword").FirstOrDefault();
            if (Adapter.CheckAccount(adminLogin) == null)
            {
                Adapter.CreateAdmin(new Entities.Account()
                {
                    Login = adminLogin,
                    Password = adminPassword,
                    Email = "admin@admin.com",
                    Name = "Администрация Всемогущая",
                    City = "Саратов",
                    Country = "England",
                });
            }            
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MappingConfig.RegisterMappings();            
        }
    }
}
