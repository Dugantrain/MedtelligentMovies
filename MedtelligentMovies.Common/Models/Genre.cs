using System;

namespace MedtelligentMovies.Common.Models
{
    /// <summary>
    /// Summary description for Genre
    /// </summary>
    public class Genre : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}