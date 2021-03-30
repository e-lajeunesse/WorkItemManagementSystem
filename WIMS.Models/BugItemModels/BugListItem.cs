using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIMS.Data;

namespace WIMS.MVC.Models
{
    public class BugListItem
    { 
        public int ItemId { get; set; }
        public Size Size { get; set; }
        public string Description { get; set; }
    }
}
