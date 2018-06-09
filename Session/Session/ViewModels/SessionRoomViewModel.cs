using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Session.ViewModels
{
    /// <summary>
    /// view model for room
    /// </summary>
    public class SessionRoomViewModel
    {
       [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Room Name")]
        public string Name { get; set; }

    }
}