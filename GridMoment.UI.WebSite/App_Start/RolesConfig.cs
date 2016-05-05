using GridMoment.UI.WebSite.Controllers;

namespace GridMoment.UI.WebSite.App_Start
{
    public class RolesConfig
    {
        private static bool Registered = false;
        private static string[] roles;

        private RolesConfig()
        {  }

        public static void Init()
        {
            var countRoles = 2;
            var rolesTmp = Adapter.GetAllRoles();
            roles = new string [] { "User", "User,Moder", "User,Moder,Admin" };
            if (!Registered && rolesTmp.Length <= 0)
            {
                Adapter.RegisterRoles();
                Registered = true;
            }
            else
            {
                for (int i = 0; i <= countRoles; i++)
                {
                    if (!roles[i].Equals(rolesTmp[i]))
                        throw new System.Exception();
                }
            }
        }
    }
}