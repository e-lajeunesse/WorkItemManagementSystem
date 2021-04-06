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
        public Size Size { get; set; }
        


    }
}
