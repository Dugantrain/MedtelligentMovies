using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedtelligentMovies.Common.Models;
using Microsoft.Practices.Unity;
using MedtelligentMovies.Common.Services;

namespace MedtelligentMovies.Web.Admin
{
    public partial class ManageMovies : Page
    {
        [Dependency]
        public IMovieService MovieService { get; set; }
        [Dependency]
        public IGenreService GenreService { get; set; }
        private IEnumerable<Movie> _movies;
        private IEnumerable<Genre> _genres; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _movies = MovieService.GetMoviesWithGenre(0, Int32.MaxValue);
                _genres = GenreService.GetGenres(0, Int32.MaxValue);
                ViewState["Movies"] = _movies;
                ViewState["Genres"] = _genres;

                ddlGenre.DataSource = _genres;
                ddlGenre.DataValueField = "Id";
                ddlGenre.DataTextField = "Title";
                ddlGenre.DataBind();
            }
            else
            {
                _movies = (IEnumerable<Movie>)ViewState["Movies"];
                _genres = (IEnumerable<Genre>) ViewState["Genres"];
            }
            
            gvMovies.DataSource = _movies;
            gvMovies.DataBind();
        }

        protected void PopulateFieldsForUpdate(object sender, EventArgs e)
        {
            var lnkUpdate = (Button)sender;
            var movieId = Convert.ToInt32(lnkUpdate.CommandArgument);
            var movie = MovieService.GetMovieById(movieId);
            hdnId.Value = movie.Id.ToString();
            txtTitle.Text = movie.Title;
            txtDescription.Text = movie.Description;
            ddlGenre.SelectedValue = movie.GenreId.ToString();
            InsertUpdateMoviePanel.Update();
        }

        protected void DeleteMovie(object sender, EventArgs e)
        {
            var lnkRemove = (Button)sender;
            var movieId = Convert.ToInt32(lnkRemove.CommandArgument);
            MovieService.DeleteMovie(movieId);
            _movies = MovieService.GetMovies(0, Int32.MaxValue);
            ViewState["Movies"] = _movies;
            gvMovies.DataSource = _movies;
            gvMovies.DataBind();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (
               String.IsNullOrEmpty(txtDescription.Text) ||
               String.IsNullOrEmpty(txtTitle.Text)) { return; }

            if (!String.IsNullOrEmpty(hdnId.Value))
            {
                //Update
                var movie = MovieService.GetMovieById(Convert.ToInt32(hdnId.Value));
                if (movie.Title != txtTitle.Text) MovieService.ChangeTitle(movie.Id, txtTitle.Text);
                if (movie.Description != txtDescription.Text) MovieService.ChangeDescription(movie.Id, txtDescription.Text);
            }
            else
            {
                //Create
                var movie = new Movie
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    GenreId = Convert.ToInt32(ddlGenre.SelectedValue),
                };
                MovieService.Create(movie);
            }

            _movies = MovieService.GetMovies(0, Int32.MaxValue);
            ViewState["Movies"] = _movies;
            gvMovies.DataSource = _movies;
            gvMovies.DataBind();

            txtTitle.Text = String.Empty;
            txtDescription.Text = String.Empty;
            InsertUpdateMoviePanel.Update();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            hdnId.Value = String.Empty;
            txtTitle.Text = String.Empty;
            txtDescription.Text = String.Empty;

            InsertUpdateMoviePanel.Update();
        }
    }
}