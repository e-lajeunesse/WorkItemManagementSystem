using System;
using System.Collections.Generic;
using System.Text;

namespace WIMS.Data
{
    public enum Size { SM, MD, LG, XL}

    public enum ItemType { Bug, Feature}
    public interface IWorkItem
    {
        int ItemId { get; set; }
        string Description { get; set; }
        Size Size { get; set; }
        DateTime DateCreated { get; set; }

        public ItemType Type { get; }
        int DaysPending {  get; }
        bool IsComplete { get; set; }
        DateTime? DateCompleted { get; set; }        
        string CreatorName { get; set; }
        string CompletedByName { get; set; }
        
        string ApplicationUserId { get; set; }
        abstract ApplicationUser ApplicationUser { get; set; }

    }
}
