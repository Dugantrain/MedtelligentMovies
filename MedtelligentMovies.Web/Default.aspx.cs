using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedtelligentMovies.Common.Repositories;
using MedtelligentMovies.Common.Services;
using Microsoft.Practices.ServiceLocation;

namespace MedtelligentMovies.Web
{
    public partial class Default : Page
    {
        //WebForms don't play well with constructor injection so we'll just use ServiceLocator.
        //Nothing here is mockable anyway, so we're not losing out on a whole lot.
        private static readonly IMovieService MovieService =
            ServiceLocator.Current.GetInstance<IMovieService>();
        private static readonly IGenreService GenreService =
            ServiceLocator.Current.GetInstance<IGenreService>();
        private const int NumMoviesPerGenre = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var genres = GenreService.GetGenres(0, 5);
                var genreIds = genres.Select(g => g.Id).ToArray();
                var topMovies = MovieService.GetTopMoviesByGenreIds(genreIds, NumMoviesPerGenre);
            }
        }
    }
}