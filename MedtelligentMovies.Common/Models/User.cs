using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedtelligentMovies.Common.Models
{
    //TODO:  Immutability!  All up on your tonsils!
    /// <summary>
    /// Summary description for User
    /// </summary>
    public class User : IAuditable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        public string EncryptedPassword { get; set; }
        public bool IsAdministrator { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}