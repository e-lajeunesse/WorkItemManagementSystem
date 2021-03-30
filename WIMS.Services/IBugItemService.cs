using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models;
using WIMS.Models.BugItemModels;
using WIMS.MVC.Models;

namespace WIMS.Services
{
    public interface IBugItemService
    {
        Task<bool> AddBugItem (BugItemCreate model);
        Task<IEnumerable<WorkItemListItem>> GetBugItems();
    }
}
