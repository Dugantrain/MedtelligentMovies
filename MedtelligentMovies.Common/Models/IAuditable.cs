using System;

namespace MedtelligentMovies.Common.Models
{
    /// <summary>
    /// Summary description for IAuditable
    /// </summary>
    public interface IAuditable : IModel
    {
        DateTime CreatedDate { get; set; }
        DateTime LastUpdatedDate { get; set; }
        int CreatedByUserId { get; set; }
        int LastUpdatedByUserId { get; set; }
    }
}