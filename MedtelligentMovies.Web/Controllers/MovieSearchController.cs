﻿using System;
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
    public class MovieSearchController : ApiController, IMovieSearchController
    {
        private readonly IMovieService _movieService;
        public MovieSearchController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // GET api/<controller>
        public IEnumerable<Movie> Get(string searchText)
        {
            return _movieService.GetMoviesBySearchText(searchText);
        }
    }
}