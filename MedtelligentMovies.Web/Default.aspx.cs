using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedtelligentMovies.Common.Models;
using MedtelligentMovies.Common.Repositories;
using MedtelligentMovies.Common.Services;
using Microsoft.Practices.Unity;

namespace MedtelligentMovies.Web
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Admin/ManageMovies.aspx");
            }
            else
            {
                Response.Redirect("~/Public/Landing.aspx");
            }  
        }
    }
}