using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Data
{
    public class BugItem : IWorkItem
    {
        [Key]
        public int ItemId { get; set; }
        public string Description { get; set; }
        public Size Size { get; set; }
        public DateTime DateCreated { get; set; }
 //       public int DaysPending { get; set; }
        public bool IsComplete { get; set; }
//        public DateTime DateCompleted { get; set; }
/*        public Guid CreatorId { get; set; }
        public Guid OwnderId { get; set; }*/
    }
}
