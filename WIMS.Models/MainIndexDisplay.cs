using System;
using System.Collections.Generic;
using System.Text;
using WIMS.Data;
using WIMS.Models.UserModels;

namespace WIMS.Models
{
    public class MainIndexDisplay
    {
        public UserDisplay User { get; set; }
        public List<WorkItemListItem> WorkItems { get; set; }
    }
}
