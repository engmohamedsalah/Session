using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Model
{
    [Table("SessionTopic")]
    public class SessionTopic: AuditableEntity<int>
    {
        
        [Required]
        [MaxLength(50)]
        [Display(Name = "Session Topic")]
        public string Name { get; set; }

        public virtual IEnumerable<SessionClass> SessionClasses { get; set; }
    }
}
