using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WIMS.Data;

namespace WIMS.Models
{
    public class WorkItemListItem
    {
        [Display(Name ="Id")]
        public int ItemId { get; set; }
        public Size Size { get; set; }
        public ItemType Type { get;set; }
        public string Description { get; set; }

        [Display(Name ="Assigned To")]
        public string OwnerName { get; set; }

        [Display(Name ="Days Pending")]
        public int DaysPending { get; set; }

    }
}
