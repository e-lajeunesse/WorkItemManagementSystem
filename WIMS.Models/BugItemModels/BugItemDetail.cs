using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WIMS.Data;

namespace WIMS.Models.BugItemModels
{
    public class BugItemDetail
    {
        [Display(Name ="Item Id")]
        public int ItemId { get; set; }
        public string Description { get; set; }
        public ItemType Type { get; set; }
        public Size Size { get; set; }

        [Display(Name ="Created On")]
        public DateTime DateCreated { get; set; }

        [Display(Name ="Days Pending")]
        public int DaysPending { get; set; }

        [Display(Name = "Created By")]
        public string CreatorName { get; set; }
        public bool IsComplete { get; set; }
        public string ApplicationUserId { get; set; }

        [Display(Name = "Assigned To")]
        public string FullName { get; set; }
    }
}
