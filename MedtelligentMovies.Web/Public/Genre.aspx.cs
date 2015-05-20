using System;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using MedtelligentMovies.Common.Services;

namespace MedtelligentMovies.Web.Public
{
    public partial class Genre : System.Web.UI.Page
    {
        [Dependency]
        public IMovieService MovieService { get; set; }
        [Dependency]
        public IGenreService GenreService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //So here's something:  With webforms, no url routing w/out a special handler.
            //Hanging the id off the querystring like I'm doing here is not optimized for SEO.
            if (!Page.IsPostBack)
            {
                var genreId = Convert.ToInt32(Request.QueryString["g"]);
                var genre = GenreService.GetGenreById(genreId);
                lblTitle.Text = genre.Title;
                var movies = MovieService.GetMoviesByGenreId(genreId).ToList();
                movies = movies.OrderBy(m => m.Title).ToList();
                gvMovies.DataSource = movies;
                gvMovies.DataBind();
            }
        }

        protected void gvMovies_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover",
                    "ChangeMouseOverRowColor('" + gvMovies.ClientID + "','" + (e.Row.RowIndex + 1) + "','" + hdnSelectedMovieId.ClientID + "')");
                var clickPostback = Page.ClientScript.GetPostBackClientHyperlink(gvMovies, "Select$" + e.Row.RowIndex);
                e.Row.Attributes.Add("onClick",
                    "ChangeSelectedRowColorOnClick('" + gvMovies.ClientID + "','" + (e.Row.RowIndex + 1) + "','" + hdnSelectedMovieId.ClientID + "');" + clickPostback);
                e.Row.Attributes.Add("onmouseout",
                    "PreserveClickedRowStyleOnMouseOut('" + gvMovies.ClientID + "','" + hdnSelectedMovieId.ClientID + "')");
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvMovies.Rows)
            {
                if (row.RowIndex == gvMovies.SelectedIndex)
                {
                    row.CssClass = "selected-row";
                }
                else if ((row.DataItemIndex%2) == 0)
                {
                    row.CssClass = "row";
                }
                else
                {
                    row.CssClass = "alt-row";
                }
            }
            MovieUpdatePanel.Update();
        }
    }
}