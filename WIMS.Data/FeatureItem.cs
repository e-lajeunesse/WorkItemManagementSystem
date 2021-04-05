using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WIMS.Data
{
    public class FeatureItem : IWorkItem
    {
        [Key]
        public int ItemId { get; set; }
        public string Description { get; set; }
        public Size Size { get; set; }
        public ItemType Type => ItemType.Feature;
        public DateTime DateCreated { get; set; }        
        public int DaysPending
        {
            get
            {
                double days = (DateTime.Now - DateCreated).TotalDays;
                return (int)days;
            }
        }
        public bool IsComplete { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string CompletedByName { get; set; }
        
        public string CreatorName { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
