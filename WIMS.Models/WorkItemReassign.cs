using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models
{
    public class WorkItemReassign
    {
        [Required]
        [Display(Name ="Reassign To:")]
        public string ApplicationUserId { get; set; }
        
    }
}
