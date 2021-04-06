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
        [MaxLength(75)]
        public string Description { get; set; }

        [Required]
        public Size Size { get; set; }
    }
}
