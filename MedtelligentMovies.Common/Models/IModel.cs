using System.ComponentModel.DataAnnotations.Schema;

namespace MedtelligentMovies.Common.Models
{
    /// <summary>
    /// Summary description for IAuditable
    /// </summary>
    public interface IModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int Id { get; }
    }
}