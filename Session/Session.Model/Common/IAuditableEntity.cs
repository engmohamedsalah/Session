using System;

/// <summary>
/// Interface of the Auditable Entity
/// contains the 4 colums that used for tracking changes in the table
/// Creation or update
/// </summary>
namespace Session.Model
{
    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }

        string CreatedBy { get; set; }

        DateTime UpdatedDate { get; set; }

        string UpdatedBy { get; set; }
    }
}
