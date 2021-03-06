using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WIMS.Data;

namespace WIMS.Models
{
    public class CompletedItemListItem
    {
        [Display(Name ="Id")]
        public int ItemId { get; set; }
        public Priority Priority { get; set; }
        public Size Size { get; set; }
        public ItemType Type { get; set; }
        public string Description { get; set; }

        [Display(Name ="Completed")]
        public DateTime? DateCompleted { get; set; }
        [Display(Name = "Completed By")]
        public string CompletedByName { get; set; }
    }
}
