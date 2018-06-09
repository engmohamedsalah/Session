using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Session.ViewModels
{
    public class SessionTopicViewModel
    {
       [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Topic Name")]
        public string Name { get; set; }

    }
}