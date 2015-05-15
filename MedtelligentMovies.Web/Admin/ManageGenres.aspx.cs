using System;
using System.Linq;
using Microsoft.Practices.Unity;
using MedtelligentMovies.Common.Services;

namespace MedtelligentMovies.Web.Admin
{
    public partial class ManageGenres : System.Web.UI.Page
    {
        [Dependency]
        public IGenreService GenreService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var genres = GenreService.GetGenres(0, Int32.MaxValue).ToList().OrderBy(g=>g.Title);
                gvGenres.DataSource = genres;
                gvGenres.DataBind();
            }
        }
    }
}