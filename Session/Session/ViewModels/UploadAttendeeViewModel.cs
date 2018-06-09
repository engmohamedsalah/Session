using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Session.ViewModels
{
    /// <summary>
    /// view model for file CSV upload 
    /// </summary>
    public class UploadAttendeeViewModel
    {
        [Required]
        public HttpPostedFileBase File { get; set; }

       
    }
}