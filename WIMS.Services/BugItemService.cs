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
        public async Task<bool> AddBugItem (BugItemCreate model,string userId, string userName)
        {
            BugItem item = new BugItem
            {
                Description = model.Description,
                Size = model.Size,
                IsComplete = false,
                DateCreated = DateTime.Now,
                CreatorName = userName,
                ApplicationUserId = userId,
                
            };
            await _context.BugItems.AddAsync(item);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<IEnumerable<WorkItemListItem>> GetBugItems()
        {
            
            
            return await _context.BugItems.Select(i => new WorkItemListItem
            {
                ItemId = i.ItemId,
                Description = i.Description,
                Type = i.Type,
                Size = i.Size,
                DaysPending = i.DaysPending,
                OwnerName = i.ApplicationUser.UserName
            }).ToListAsync();
            
        }

        public async Task<BugItemDetail> GetBugItemById(int id)
        {
            BugItem item = await _context.BugItems.SingleAsync(i => i.ItemId == id);
            return new BugItemDetail
            {
                ItemId = item.ItemId,
                Description = item.Description,
                Type = item.Type,
                Size = item.Size,
                DateCreated = item.DateCreated,
                DaysPending = item.DaysPending,
                CreatorName = item.CreatorName,
                ApplicationUserId = item.ApplicationUserId,
                User = item.ApplicationUser
            };
        }

        public async Task<bool> EditBugItem(BugItemEdit model)
        {
            BugItem itemToEdit = await _context.BugItems.SingleAsync(i => i.ItemId == model.ItemId);
            itemToEdit.Description = model.Description;
            itemToEdit.Size = model.Size;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<bool> DeleteBugItem(int itemId)
        {
            BugItem itemToDelete = await _context.BugItems.SingleAsync(i => i.ItemId == itemId);
            _context.BugItems.Remove(itemToDelete);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }
    }
}
