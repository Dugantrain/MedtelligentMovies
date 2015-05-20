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

        protected void DeleteGenre(object sender, EventArgs e)
        {
            var lnkRemove = (ImageButton)sender;
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
            InsertUpdateGenrePanel.Update();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            hdnId.Value = String.Empty;
            txtTitle.Text = String.Empty;
            txtDescription.Text = String.Empty;
            InsertUpdateGenrePanel.Update();
        }

        protected void gvGenres_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var clickPostback = Page.ClientScript.GetPostBackClientHyperlink(gvGenres, "Select$" + e.Row.RowIndex);
                e.Row.Attributes.Add("onmouseover",
                    "ChangeMouseOverRowColor('" + gvGenres.ClientID + "','" + (e.Row.RowIndex + 1) + "','" + hdnSelectedGenreId.ClientID + "')");
                e.Row.Attributes.Add("onClick",
                    "ChangeSelectedRowColorOnClick('" + gvGenres.ClientID + "','" + (e.Row.RowIndex + 1) + "','" + hdnSelectedGenreId.ClientID + "');" + clickPostback);
                e.Row.Attributes.Add("onmouseout",
                    "PreserveClickedRowStyleOnMouseOut('" + gvGenres.ClientID + "','" + hdnSelectedGenreId.ClientID + "')");
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvGenres.Rows)
            {
                if (row.RowIndex == gvGenres.SelectedIndex)
                {
                    row.CssClass = "selected-row";
                    var genreId = gvGenres.DataKeys[row.RowIndex]["Id"];
                    var genre = GenreService.GetGenreById(Convert.ToInt32(genreId));
                    hdnId.Value = genre.Id.ToString();
                    txtTitle.Text = genre.Title;
                    txtDescription.Text = genre.Description;
                    InsertUpdateGenrePanel.Update();
                }
            }
        }
    }
}