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

namespace MedtelligentMovies.Web
{
    public partial class Default : Page
    {
        //WebForms don't play well with constructor injection so we'll just use ServiceLocator.
        //Nothing here is mockable anyway, so we're not missing out on a whole lot.
        private IMovieService _movieService;
        private IGenreService _genreService;
        private const int NumMoviesPerGenre = 5;
        private Dictionary<int, List<Movie>> _topMoviesByGenreIds;
        protected void Page_Load(object sender, EventArgs e)
        {
            _movieService = ServiceLocator.Current.GetInstance<IMovieService>();
            _genreService = ServiceLocator.Current.GetInstance<IGenreService>();

            if (!Page.IsPostBack)
            {
                var genres = _genreService.GetGenres(0, 5);
                var genreIds = genres.Select(g => g.Id).ToArray();
                var topMoviesByGenreIds = _movieService.GetTopMoviesByGenreIds(genreIds, NumMoviesPerGenre);
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