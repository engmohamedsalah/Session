using Session.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Session.ViewModels
{
    /// <summary>
    /// this view model for session table
    /// </summary>
    public class SessionClassViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Topic")]
        public int SessionTopicID { get; set; }
        [Required]
        [Display(Name = "Room")]
        public int SessionRoomID { get; set; }

        [Display(Name = "Room")]
        public string RoomName { get; set; }
        [Display(Name = "Topic")]
        public string TopicName { get; set; }



    }
}