using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WIMS.Data
{
    public class BugItem : IWorkItem
    {
        [Key]
        public int ItemId { get; set; }
        public string Description { get; set; }
        public string Type
        {
            get
            {
                return this.GetType().ToString();
            }
        }
        public Size Size { get; set; }
        public DateTime DateCreated { get; set; }
        public int DaysPending 
        {
            get 
            {
                double days = (DateCreated - DateTime.Now).TotalDays;
                return (int)days;
            }
        }
        public bool IsComplete { get; set; }
        public DateTime DateCompleted { get; set; }
        public string CreatorId { get; set; }
        
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
