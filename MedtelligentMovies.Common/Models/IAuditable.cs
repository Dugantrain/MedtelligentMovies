using System;

namespace Models
{
    /// <summary>
    /// Summary description for IAuditable
    /// </summary>
    public interface IAuditable : IModel
    {
        DateTime CreatedDate { get; }
        DateTime LastUpdatedDate { get; }
        int CreatedByUserId { get; }
        int LastUpdatedByUserId { get; }
    }
}