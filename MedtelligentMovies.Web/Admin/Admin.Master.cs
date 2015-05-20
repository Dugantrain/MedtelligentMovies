using System;
using System.Web;


namespace MedtelligentMovies.Web.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
               Response.Redirect("/Account/Login.aspx");
            }
        }
    }
}