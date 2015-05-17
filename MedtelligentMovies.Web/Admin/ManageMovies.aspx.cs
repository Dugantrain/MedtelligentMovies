using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using MedtelligentMovies.Common.Services;

namespace MedtelligentMovies.Web.Admin
{
    public partial class ManageMovies : System.Web.UI.Page
    {
        [Dependency]
        public IMovieService MovieService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var movies = MovieService.GetMovies(0, Int32.MaxValue).ToList().OrderBy(m => m.Title);
                gvMovies.DataSource = movies;
                gvMovies.DataBind();
            }
        }
    }
}