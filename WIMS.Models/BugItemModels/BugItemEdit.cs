using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WIMS.Data;

namespace WIMS.Models.BugItemModels
{
    public class BugItemEdit
    {
        [Display(Name ="Item Id")]
        public int ItemId { get; set; }
        public string Description { get; set; }

        [Display(Name = "Details")]
        public string DetailedDescription { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public Size Size { get; set; }
        


    }
}
