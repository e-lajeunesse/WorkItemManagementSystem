using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WIMS.Data;

namespace WIMS.Models.BugItemModels
{
    public class BugItemCreate
    {
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        public string DetailedDescription { get; set; }

        [Required]
        public Priority Priority { get; set; }



        [Required]
        public Size Size { get; set; }
    }
}
