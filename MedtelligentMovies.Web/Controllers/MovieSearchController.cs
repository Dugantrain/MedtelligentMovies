using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MedtelligentMovies.Common.Models;
using MedtelligentMovies.Common.Services;

namespace MedtelligentMovies.Web.Controllers
{
    public interface IMovieSearchController
    {
        
    }
    //WebApi!  All up on your tonsils!
    public class MovieSearchController : ApiController, IMovieSearchController
    {
        private readonly IMovieService _movieService;
        public MovieSearchController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // GET api/<controller>
        public IEnumerable<MenuMovie> Get(string searchText)
        {
            return _movieService.GetMoviesBySearchText(searchText).Select(m=>new MenuMovie{Id = m.Id,Title = m.Title});
        }
    }

    public class MenuMovie
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
