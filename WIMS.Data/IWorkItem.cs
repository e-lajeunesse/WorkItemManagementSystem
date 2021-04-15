using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Data
{
    public enum Size { SM, MD, LG, XL}

    public enum ItemType { Bug, Feature}

    public enum Status { Open, [Display(Name="In Progress")]InProgress, Complete, Reopened}
    public enum Priority { High, Med, Low}
    public interface IWorkItem
    {
        int ItemId { get; set; }
        string Description { get; set; }
        Size Size { get; set; }
        Status Status { get; set; }
        Priority Priority { get; set; }
        DateTime DateCreated { get; set; }

        public ItemType Type { get; }
        int DaysPending {  get; }
        
        DateTime? DateCompleted { get; set; }        
        string CreatorName { get; set; }
        string CompletedByName { get; set; }
        
        string ApplicationUserId { get; set; }
        abstract ApplicationUser ApplicationUser { get; set; }

    }
}
