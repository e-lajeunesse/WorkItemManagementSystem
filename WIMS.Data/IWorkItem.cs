using System;
using System.Collections.Generic;
using System.Text;

namespace WIMS.Data
{
    public enum Size { SM, MD, LG, XL}
    public interface IWorkItem
    {
        int ItemId { get; set; }
        string Description { get; set; }
        Size Size { get; set; }
        DateTime DateCreated { get; set; }
 //       int DaysPending { get; set; }
        bool IsComplete { get; set; }
/*        DateTime DateCompleted { get; set; }
        Guid CreatorId { get; set; }
        Guid OwnderId { get; set; }*/
    }
}
