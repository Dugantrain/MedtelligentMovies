using System;
using MedtelligentMovies.Common.Services;
using Microsoft.Practices.Unity;

namespace MedtelligentMovies.Web.Admin
{
    
    public partial class ManageUsers : System.Web.UI.Page
    {
        [Dependency]
        public IUserService UserService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var users = UserService.GetUsers(0, Int32.MaxValue);
                gvUsers.DataSource = users;
                gvUsers.DataBind();
            }
        }
    }
}