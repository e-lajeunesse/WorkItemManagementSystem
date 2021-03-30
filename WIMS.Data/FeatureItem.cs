using System;
using System.Collections.Generic;
using System.Text;

namespace WIMS.Data
{
    public class FeatureItem : IWorkItem
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public Size Size { get; set; }
        public DateTime DateCreated { get; set; }
        public int DaysPending { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCompleted { get; set; }
        public Guid CreatorId { get; set; }
        public Guid OwnderId { get; set; }
    }
}
