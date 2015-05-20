using System;
using System.Collections.Generic;
using System.Drawing;
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
        private bool _isGenreValid = false;
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

                ddlGenre.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                _movies = (IEnumerable<Movie>)ViewState["Movies"];
                _genres = (IEnumerable<Genre>) ViewState["Genres"];
            }
            
            gvMovies.DataSource = _movies;
            gvMovies.DataBind();
        }

        protected void DropDownGenreValidation(object source, ServerValidateEventArgs arguments)
        {
            var selectedValue = arguments.Value;
            if (selectedValue == "0")
            {
                arguments.IsValid = false;
                return;
            }
            _isGenreValid = true;
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
               String.IsNullOrEmpty(txtTitle.Text) ||
                ddlGenre.SelectedIndex == 0) { return; }

            if (!String.IsNullOrEmpty(hdnId.Value))
            {
                //Update
                var movie = MovieService.GetMovieById(Convert.ToInt32(hdnId.Value));
                if (movie.Title != txtTitle.Text) MovieService.ChangeTitle(movie.Id, txtTitle.Text);
                if (movie.Description != txtDescription.Text) MovieService.ChangeDescription(movie.Id, txtDescription.Text);
                var currentGenreId = Convert.ToInt32(ddlGenre.SelectedValue);
                if (movie.GenreId != currentGenreId)MovieService.ChangeGenre(movie.Id, currentGenreId);
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
            ddlGenre.SelectedIndex = 0;
            InsertUpdateMoviePanel.Update();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            hdnId.Value = String.Empty;
            txtTitle.Text = String.Empty;
            txtDescription.Text = String.Empty;
            ddlGenre.SelectedIndex = 0;
            InsertUpdateMoviePanel.Update();
        }

        protected void gvMovies_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var movie = (Movie) e.Row.DataItem;
                var genre = _genres.Single(g => g.Id == movie.GenreId);

                e.Row.Attributes.Add("onmouseover",
                    "ChangeMouseOverRowColor('" + gvMovies.ClientID + "','" + (e.Row.RowIndex + 1) + "','" + hdnSelectedMovieId.ClientID + "')");
                var clickPostback = Page.ClientScript.GetPostBackClientHyperlink(gvMovies, "Select$" + e.Row.RowIndex);
                e.Row.Attributes.Add("onClick",
                    "ChangeSelectedRowColorOnClick('" + gvMovies.ClientID + "','" + (e.Row.RowIndex + 1) + "','" + hdnSelectedMovieId.ClientID + "');" + clickPostback);
                e.Row.Attributes.Add("onmouseout",
                    "PreserveClickedRowStyleOnMouseOut('" + gvMovies.ClientID + "','" + hdnSelectedMovieId.ClientID + "')");

                //Man, there's got to be a better way to do this.
                e.Row.Cells[3].Text = genre.Title;
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvMovies.Rows)
            {
                if (row.RowIndex == gvMovies.SelectedIndex)
                {
                    row.CssClass = "selected-row";

                    var movieId = gvMovies.DataKeys[row.RowIndex]["Id"];
                    var movie = MovieService.GetMovieById(Convert.ToInt32(movieId));
                    hdnId.Value = movie.Id.ToString();
                    txtTitle.Text = movie.Title;
                    txtDescription.Text = movie.Description;
                    ddlGenre.SelectedValue = movie.GenreId.ToString();
                    InsertUpdateMoviePanel.Update();
                }
            }
        }
    }
}