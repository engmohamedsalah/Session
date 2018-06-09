using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
///abstract class the parent for tables contains the 4 audit columns
///created by : user id who create the record
///createdate : creation date
///updated by : user id who updated the record
///updated date : updated date
/// </summary>
namespace Session.Model
{


    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }


        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdatedDate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string UpdatedBy { get; set; }
    }
}
