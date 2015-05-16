using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedtelligentMovies.Common.Extensions;
using MedtelligentMovies.Common.Services;
using Microsoft.Practices.Unity;

namespace MedtelligentMovies.Web.Account
{
    public partial class Login : Page
    {
        [Dependency]
        public IUserService UserService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(HttpContext.Current.User.Identity.IsAuthenticated)Response.Redirect("~/Admin/ManageUsers.aspx");
        }

        protected void OnAuthenticate(object sender, AuthenticateEventArgs e)
        {
            var isAuthenticated = false;
            var user = UserService.GetUserByUsername(lgMainLogin.UserName);
            if (user != null)
            {
                if (user.EncryptedPassword == lgMainLogin.Password.Encrypt())
                {
                    isAuthenticated = true;
                    FormsAuthentication.SetAuthCookie(user.UserName,lgMainLogin.RememberMeSet);
                }
            }
            e.Authenticated = isAuthenticated;
            if (isAuthenticated) Response.Redirect("~/Admin/ManageUsers.aspx");
        }
    }
}