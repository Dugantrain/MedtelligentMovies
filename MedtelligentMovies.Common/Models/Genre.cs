﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedtelligentMovies.Common.Models
{
    /// <summary>
    /// Summary description for Genre
    /// </summary>
    public class Genre : IAuditable
    {
        public int Id { get; set; }
        [Index("IX_Genre_TitleDescription", 1)]
        public string Title { get; set; }
        [Index("IX_Genre_TitleDescription", 2)]
        public string Description { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}