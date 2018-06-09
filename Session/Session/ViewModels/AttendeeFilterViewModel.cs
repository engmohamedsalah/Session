using Session.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Session.ViewModels
{
    public class AttendeeFilterViewModel
    {
       
        //filteration emelents
        public List<SessionFilterViewModel> AttendeeSessionFilter { get; set; }

        //radio button of condition for oring or anding condition 
        //for session
        public bool AndingSession { get; set; }
        //radio button of condition for oring or anding condition 
        //for time 
        public bool AndingTime { get; set; }


        public SessionAttendeeViewModel SessionAttendeeResultViewModel { get; set; }
}


    public class SessionFilterViewModel
    {
       
        public bool IsSelected { get; set; }
        public string SessionTopicName { get; set; }
        public int SessionTopicID { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }
    public class TimeSlot
    {
        public int SessionClassID { get; set; }
        public bool IsSelected { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:h:mm tt}")]
        public DateTime From { get; set; }
        [DisplayFormat(DataFormatString = "{0:h:mm tt}")]
        public DateTime To { get; set; }
    }




}