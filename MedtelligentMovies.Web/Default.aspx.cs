using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedtelligentMovies.Common.Models;
using MedtelligentMovies.Common.Repositories;
using MedtelligentMovies.Common.Services;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace MedtelligentMovies.Web
{
    public partial class Default : Page
    {
        //WebForms don't play well with constructor injection or ServiceLocator.  Had to use
        //a 3rd party package to handle the buildup of dependencies so that things would fire at the correct
        //point in the page lifecycle.  Unfortunately, this couples Unity to our web page.  Still beats direct instantiation.
        [Dependency]
        public IMovieService MovieService { get; set; }
        [Dependency]
        public IGenreService GenreService { get; set; }

        private const int NumMoviesPerGenre = 5;
        private Dictionary<int, List<Movie>> _topMoviesByGenreIds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var genres = GenreService.GetGenres(0, 5);
                var genreIds = genres.Select(g => g.Id).ToArray();
                var topMoviesByGenreIds = MovieService.GetTopMoviesByGenreIds(genreIds, NumMoviesPerGenre);
                _topMoviesByGenreIds = topMoviesByGenreIds;
                gvGenres.DataSource = genres;
                gvGenres.DataBind();
            }
        }

        protected void gvGenres_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var genreId = ((Genre)e.Row.DataItem).Id;
            var topMovies = _topMoviesByGenreIds.Single(m => m.Key == genreId).Value;
            var gvTopMovies = (GridView)e.Row.FindControl("gvTopMovies");
            gvTopMovies.DataSource = topMovies;
            gvTopMovies.DataBind();

        }
    }
}