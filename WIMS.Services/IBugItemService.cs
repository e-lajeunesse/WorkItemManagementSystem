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
        Task<bool> AddBugItem (BugItemCreate model, string userId, string fullName);
        Task<IEnumerable<WorkItemListItem>> GetBugItems();
        Task<IEnumerable<WorkItemListItem>> GetBugItemsByUser(string userId);
        
        Task<BugItemDetail> GetBugItemById(int id);
        Task<bool> EditBugItem(BugItemEdit model);
        Task<bool> DeleteBugItem(int itemId);
        IEnumerable<WorkItemListItem> GetBugItemsByTeam(int? teamId);
        Task<bool> ReassignBugItem(int id, WorkItemReassign model);
        Task<bool> CompleteBugItem(int id);
    }
}
