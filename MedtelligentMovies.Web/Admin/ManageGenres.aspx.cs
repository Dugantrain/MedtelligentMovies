using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using MedtelligentMovies.Common.Extensions;
using Microsoft.Practices.Unity;
using MedtelligentMovies.Common.Services;
using MedtelligentMovies.Common.Models;
namespace MedtelligentMovies.Web.Admin
{
    public partial class ManageGenres : System.Web.UI.Page
    {
        [Dependency]
        public IGenreService GenreService { get; set; }
        private IEnumerable<Genre> _genres;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _genres = GenreService.GetGenres(0, Int32.MaxValue);
                ViewState["Genres"] = _genres;
            }
            else
            {
                _genres = (IEnumerable<Genre>)ViewState["Genres"];
            }
            gvGenres.DataSource = _genres;
            gvGenres.DataBind();
        }

        protected void PopulateFieldsForUpdate(object sender, EventArgs e)
        {
            var lnkUpdate = (Button)sender;
            var genreId = Convert.ToInt32(lnkUpdate.CommandArgument);
            var genre = GenreService.GetGenreById(genreId);
            hdnId.Value = genre.Id.ToString();
            txtTitle.Text = genre.Title;
            txtDescription.Text = genre.Description;
            InsertUpdateGenrePanel.Update();
        }

        protected void DeleteGenre(object sender, EventArgs e)
        {
            var lnkRemove = (Button)sender;
            var genreId = Convert.ToInt32(lnkRemove.CommandArgument);
            GenreService.DeleteGenre(genreId);
            _genres = GenreService.GetGenres(0, Int32.MaxValue);
            ViewState["Genres"] = _genres;
            gvGenres.DataSource = _genres;
            gvGenres.DataBind();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (
               String.IsNullOrEmpty(txtDescription.Text) ||
               String.IsNullOrEmpty(txtTitle.Text) ){ return; }

            if (!String.IsNullOrEmpty(hdnId.Value))
            {
                //Update
                var genre = GenreService.GetGenreById(Convert.ToInt32(hdnId.Value));
                if (genre.Title != txtTitle.Text) GenreService.ChangeTitle(genre.Id, txtTitle.Text);
                if (genre.Description != txtDescription.Text) GenreService.ChangeDescription(genre.Id, txtDescription.Text);
            }
            else
            {
                //Create
                var genre = new Genre
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text
                };
                GenreService.Create(genre);
            }

            _genres = GenreService.GetGenres(0, Int32.MaxValue);
            ViewState["Genres"] = _genres;
            gvGenres.DataSource = _genres;
            gvGenres.DataBind();

            txtTitle.Text = String.Empty;
            txtDescription.Text = String.Empty;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            hdnId.Value = String.Empty;
            txtTitle.Text = String.Empty;
            txtDescription.Text = String.Empty;
            InsertUpdateGenrePanel.Update();
        }
    }
}