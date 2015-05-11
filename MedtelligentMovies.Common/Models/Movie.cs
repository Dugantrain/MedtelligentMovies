using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedtelligentMovies.Common.Models
{
    /// <summary>
    /// Summary description for Movie
    /// </summary>
    public class Movie : IAuditable
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Index("IX_Movie_TitleDescription", 1)]
        public string Title { get; set; }
        [MaxLength(500)]
        [Index("IX_Movie_TitleDescription", 2)]
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; }
        [Index]
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}

