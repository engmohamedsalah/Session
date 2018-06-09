using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session.Model
{
    /// <summary>
    /// Key value table contains the dynamic data for full attendee details 
    /// </summary>
    /// <remarks></remarks>
    [Table("AttendeeFullData")]
    public class AttendeeFullData : AuditableEntity<int>
    {
        [Required]
        [Display(Name = "Session Attendee ID")]
        public int SessionAttendeeID { get; set; }

        [ForeignKey("SessionAttendeeID")]
        public virtual SessionAttendee SessionAttendee { get; set; }

        [MaxLength(100)]
        public string Key { get; set; }
        [MaxLength(1000)]
        public string Value { get; set; }

    }
}
