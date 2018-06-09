using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Model
{
    [Table("SessionAttendee")]
    public class SessionAttendee : AuditableEntity<int>
    {
        [Required]
        [Display(Name = "Attendee ID")]
        public int AttendeeID { get; set; }
        [Required]
        [Display(Name = "Session Time ID")]
        public int SessionTimeID { get; set; }
        
        [ForeignKey("SessionTimeID")]
        public virtual SessionClass SessionClass { get; set; }

        public virtual ICollection<AttendeeFullData> AttendeeFullData { get; set; }




    }
}
