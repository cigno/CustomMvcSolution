using System.Web;
using System.Web.Security;
using CustomMvcSolution.WebUI.Infrastructure.Abstract;

namespace CustomMvcSolution.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        private readonly SettingProvider _settings = new SettingProvider();

        public bool Authenticate(string username, string password)
        {
            MembershipProvider provider = Membership.Providers[GetMembershipProvider()];

            bool isAuthenticated = provider != null && provider.ValidateUser(username, password);

            if (isAuthenticated)
            {
                FormsAuthentication.SetAuthCookie(username, false);

                // Add here other cookie settings
                SetupCookie();
            }

            return isAuthenticated;
        }

        private void SetupCookie()
        {
            // Setup Default Culture
            HttpCookie cultureCookie = HttpContext.Current.Request.Cookies["_culture"];

            if (cultureCookie == null)
            {
                cultureCookie = new HttpCookie("_culture") { Value = _settings.GetSetting<string>("DefaultLanguage")};
                HttpContext.Current.Response.Cookies.Add(cultureCookie);
            }
        }

        private string GetMembershipProvider()
        {
            if (_settings.GetSetting<bool>("Auth_DefaultIsActive"))
                return _settings.GetSetting<string>("Auth_DefaultMembershipProvider");

            if (_settings.GetSetting<bool>("Auth_AdamIsActive"))
                return _settings.GetSetting<string>("Auth_AdamMembershipProvider");

            return null;
        }
    }
}