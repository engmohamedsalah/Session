using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Session.ViewModels
{
    /// <summary>
    /// this class contain the view of the result that come from
    /// attendee data after apply the filter
    /// </summary>
    public class SessionAttendeeViewModel
    {
        //header 
        public List<string>  DataHeader { get; set; }
        //body two dimentional array of string
        public List<List<string>>  DataValue { get; set; }
       
    }
}