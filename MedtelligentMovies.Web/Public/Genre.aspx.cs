using System;
using System.Linq;
using Microsoft.Practices.Unity;
using MedtelligentMovies.Common.Services;

namespace MedtelligentMovies.Web.Public
{
    public partial class Genre : System.Web.UI.Page
    {
        //WebForms don't play well with constructor injection or ServiceLocator.  Had to use
        //a 3rd party package to handle the buildup of dependencies so that things would fire at the correct
        //point in the page lifecycle.  Unfortunately, this couples Unity to our web page.  Still beats direct instantiation.
        [Dependency]
        public IMovieService MovieService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Another issue with webforms:  No url routing w/out a special handler.
            //Hanging the id off the querystring like I'm doing here is not optimized for SEO.
            if (!Page.IsPostBack)
            {
                var genreId = Convert.ToInt32(Request.QueryString["g"]);
                var movies = MovieService.GetMoviesByGenreId(genreId).ToList();
                gvMovies.DataSource = movies;
                gvMovies.DataBind();
            }
        }
    }
}