using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session.Model
{
    [Table("SessionClass")]
    public class SessionClass : AuditableEntity<int>
    {
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }


        [Required]
        
        [Display(Name = "Session Topic")]
        public int SessionTopicID { get; set; }

        [Required]
       
        [Display(Name = "Session Room")]
        public int SessionRoomID { get; set; }

        [ForeignKey("SessionTopicID")]
        public virtual SessionTopic SessionTopic { get; set; }
        [ForeignKey("SessionRoomID")]
        public virtual SessionRoom SessionRoom { get; set; }


    }
}
