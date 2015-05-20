using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedtelligentMovies.Common.Models;
using MedtelligentMovies.Common.Services;
using Microsoft.Practices.Unity;

namespace MedtelligentMovies.Web.Public
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
                var genres = GenreService.GetGenres(0, Int32.MaxValue);
                var genreIds = genres.Select(g => g.Id).ToArray();
                var topMoviesByGenreIds = MovieService.GetTopMoviesByGenreIds(genreIds, NumMoviesPerGenre);
                _topMoviesByGenreIds = topMoviesByGenreIds;
                dlGenres.DataSource = genres;
                dlGenres.DataBind();
            }
        }

        protected void dlGenres_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            var genreId = ((Common.Models.Genre)e.Item.DataItem).Id;
            //Check it out.  I got EF to return each genre and their top 5 most recent movies
            //in one query.  The Linq query below just attaches the right grouping of movies to their corresponding genre.
            var topMovies = _topMoviesByGenreIds.SingleOrDefault(m => m.Key == genreId).Value;
            if (topMovies != null)
            {
                var gvTopMovies = (GridView)e.Item.FindControl("gvTopMovies");
                gvTopMovies.DataSource = topMovies;
                gvTopMovies.DataBind();
            }
        }

        protected void gvMovies_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hdnSelectedMovieId = ((HiddenField)e.Row.Parent.Parent.Parent.FindControl("hdnSelectedMovieId"));
                var gvTopMovies = (GridView) e.Row.Parent.Parent.Parent.FindControl("gvTopMovies");
                e.Row.Attributes.Add("onmouseover",
                    "ChangeMouseOverRowColor('" + gvTopMovies.ClientID + "','" + (e.Row.RowIndex + 1) + "','" + hdnSelectedMovieId.ClientID + "')");
                var clickPostback = Page.ClientScript.GetPostBackClientHyperlink(gvTopMovies, "Select$" + e.Row.RowIndex);
                e.Row.Attributes.Add("onClick",
                    "ChangeSelectedRowColorOnClick('" + gvTopMovies.ClientID + "','" + (e.Row.RowIndex + 1) + "','" + hdnSelectedMovieId.ClientID + "');" + clickPostback);
                e.Row.Attributes.Add("onmouseout",
                    "PreserveClickedRowStyleOnMouseOut('" + gvTopMovies.ClientID + "','" + hdnSelectedMovieId.ClientID + "')");
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Set every row in every cell back to their default color
            GridView gvTopMovies;
            foreach (DataListItem dataItem in dlGenres.Items)
            {
                gvTopMovies = (GridView) dataItem.FindControl("gvTopMovies");
                foreach (GridViewRow row in gvTopMovies.Rows)
                {
                    if ((row.DataItemIndex % 2) == 0)
                    {
                        row.CssClass = "row";
                    }
                    else
                    {
                        row.CssClass = "alt-row";
                    }
                }
                
            }

            //Set the selected row to the selected-row style
            gvTopMovies = (GridView)sender;
            foreach (GridViewRow row in gvTopMovies.Rows)
            {
                if (row.RowIndex == gvTopMovies.SelectedIndex)
                {
                    row.CssClass = "selected-row";
                }
            }
            var movieUpdatePanel = (UpdatePanel)gvTopMovies.Parent.Parent;
            movieUpdatePanel.Update();
 }
    }
}