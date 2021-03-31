using System;
using System.Collections.Generic;
using System.Text;
using WIMS.Data;

namespace WIMS.Models
{
    public class WorkItemListItem
    {
        public int ItemId { get; set; }
        public Size Size { get; set; }
        public ItemType Type { get;set; }
        public string Description { get; set; }
        public string OwnerName { get; set; }
        public int DaysPending { get; set; }
    }
}
