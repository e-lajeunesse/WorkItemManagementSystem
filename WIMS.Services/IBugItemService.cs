using Microsoft.AspNetCore.Identity;
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
        Task<bool> AddBugItem (BugItemCreate model, string userId, string userName);
        Task<IEnumerable<WorkItemListItem>> GetBugItems();
        Task<BugItemDetail> GetBugItemById(int id);
        Task<bool> EditBugItem(BugItemEdit model);
        Task<bool> DeleteBugItem(int itemId);
    }
}
