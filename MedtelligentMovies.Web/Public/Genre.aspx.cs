using System;
using System.Linq;
using Microsoft.Practices.Unity;
using MedtelligentMovies.Common.Services;

namespace MedtelligentMovies.Web.Public
{
    public partial class Genre : System.Web.UI.Page
    {
        [Dependency]
        public IMovieService MovieService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //So here's something:  With webforms, no url routing w/out a special handler.
            //Hanging the id off the querystring like I'm doing here is not optimized for SEO.
            if (!Page.IsPostBack)
            {
                var genreId = Convert.ToInt32(Request.QueryString["g"]);
                var movies = MovieService.GetMoviesByGenreId(genreId).ToList();
                movies = movies.OrderBy(m => m.Title).ToList();
                gvMovies.DataSource = movies;
                gvMovies.DataBind();
            }
        }
    }
}