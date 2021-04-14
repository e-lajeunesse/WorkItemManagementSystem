using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMS.Data;
using WIMS.Models.UserModels;
/*using X.PagedList;
using X.PagedList.Mvc.Core;*/

namespace WIMS.Models
{
    public class MainIndexDisplay
    {
        public UserDisplay User { get; set; }
        public IEnumerable<WorkItemListItem> WorkItems { get; set; }
    }
}
