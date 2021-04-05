using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models;
using WIMS.Models.BugItemModels;
using WIMS.MVC.Data;
using WIMS.MVC.Models;

namespace WIMS.Services
{
    public class BugItemService : IBugItemService
    {
        private ApplicationDbContext _context;
        public BugItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Creates new bug item
        public async Task<bool> AddBugItem (BugItemCreate model,string userId, string fullName)
        {
            BugItem item = new BugItem
            {
                Description = model.Description,
                Size = model.Size,
                IsComplete = false,
                DateCreated = DateTime.Now,
                CreatorName = fullName,
                ApplicationUserId = userId,                
            };
            
            await _context.BugItems.AddAsync(item);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        // Gets all Bug Items
        public async Task<IEnumerable<WorkItemListItem>> GetBugItems()
        {          
            
            return await _context.BugItems.Include(i => i.ApplicationUser).Select(i => new WorkItemListItem
            {
                ItemId = i.ItemId,
                Description = i.Description,
                Type = i.Type,
                Size = i.Size,
                DaysPending = i.DaysPending,
                OwnerName = i.ApplicationUser.FullName
            }).ToListAsync();            
        }

        //Gets all completed Bug Items
        public async Task<List<CompletedItemListItem>> GetCompletedBugItems()
        {
            return await _context.BugItems.Where(i => i.IsComplete)
                .Select(i => new CompletedItemListItem
                {
                    ItemId = i.ItemId,
                    Description = i.Description,
                    Type = i.Type,
                    Size = i.Size,
                    DateCompleted = i.DateCompleted,
                    CompletedByName = i.CompletedByName
                }).ToListAsync();
        }

        // Gets all Bug Items for User
        public async Task<IEnumerable<WorkItemListItem>> GetBugItemsByUser(string userId)
        {
            ApplicationUser user = await _context.Users.FindAsync(userId);
            return user.BugItems.Select(i => new WorkItemListItem
            {
                ItemId = i.ItemId,
                Description = i.Description,
                Type = i.Type,
                Size = i.Size,
                DaysPending = i.DaysPending,
                OwnerName = user.FullName
            }).ToList();
        }

        // Gets all Bug Items for a team
        public IEnumerable<WorkItemListItem> GetBugItemsByTeam(int? teamId)
        {
            if (teamId == null)
            {
                return null;
            }
            return _context.BugItems.Where(i => i.ApplicationUser.TeamId == teamId)
                .Select(i => new WorkItemListItem
                {
                    ItemId = i.ItemId,
                    Description = i.Description,
                    Type = i.Type,
                    Size = i.Size,
                    DaysPending = i.DaysPending,
                    OwnerName = i.ApplicationUser.FullName
                });  
        }

        // Gets Bug Item by item Id
        public async Task<BugItemDetail> GetBugItemById(int id)
        {
            BugItem item = await _context.BugItems.SingleAsync(i => i.ItemId == id);
            //_context.Entry(item).Reference(BugItem => BugItem.ApplicationUser).Load();
            BugItemDetail detail = new BugItemDetail
            {
                ItemId = item.ItemId,
                Description = item.Description,
                Type = item.Type,
                Size = item.Size,
                DateCreated = item.DateCreated,
                DaysPending = item.DaysPending,
                IsComplete = item.IsComplete,
                CreatorName = item.CreatorName,
/*                ApplicationUserId = item.ApplicationUserId,
                FullName = item.ApplicationUser.FullName*/
            };
            if (item.ApplicationUserId != null)
            {
                detail.ApplicationUserId = item.ApplicationUserId;
                detail.FullName = item.ApplicationUser.FullName;
            }
            return detail;
        }

        // Edit Bug Item
        public async Task<bool> EditBugItem(BugItemEdit model)
        {
            BugItem itemToEdit = await _context.BugItems.SingleAsync(i => i.ItemId == model.ItemId);
            itemToEdit.Description = model.Description;
            itemToEdit.Size = model.Size;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        // Delete Bug Item
        public async Task<bool> DeleteBugItem(int itemId)
        {
            BugItem itemToDelete = await _context.BugItems.SingleAsync(i => i.ItemId == itemId);
            _context.BugItems.Remove(itemToDelete);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        //Reassign Bug Item
        public async Task<bool> ReassignBugItem(int itemId, WorkItemReassign model)
        {
            BugItem item = await _context.BugItems.FindAsync(itemId);
            item.IsComplete = false;
            item.ApplicationUserId = model.ApplicationUserId;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        //Complete Bug Item
        public async Task<bool> CompleteBugItem(int itemId)
        {
            BugItem item = await _context.BugItems.FindAsync(itemId);
            item.IsComplete = true;
            item.DateCompleted = DateTime.Now;
            item.CompletedByName = item.ApplicationUser.FullName;
            item.ApplicationUserId = null;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }
    }
}
