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
using WIMS.Models.NoteModels;
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
                DetailedDescription = model.DetailedDescription,
                Size = model.Size,
                Priority = model.Priority,
                Status = Status.Open,                
                DateCreated = DateTime.Now,
                DateCompleted = null,
                CreatorName = fullName,
                ApplicationUserId = userId,                
            };
            
            await _context.BugItems.AddAsync(item);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        // Gets all Pending Bug Items
        public async Task<List<WorkItemListItem>> GetBugItems()
        {

            return await _context.BugItems.Where(i => i.Status != Status.Complete)
                .Select(i => new WorkItemListItem
            {
                ItemId = i.ItemId,
                Description = i.Description,
                Status = i.Status,
                Priority = i.Priority,
                Type = i.Type,
                Size = i.Size,
                DaysPending = i.DaysPending,
                OwnerName = i.ApplicationUser.FullName
            }).ToListAsync();
            
        }

        //Gets all completed Bug Items
        public async Task<List<CompletedItemListItem>> GetCompletedBugItems()
        {
            return await _context.BugItems.Where(i => i.Status == Status.Complete)
                .Select(i => new CompletedItemListItem
                {
                    ItemId = i.ItemId,
                    Description = i.Description,
                    Priority = i.Priority,
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
                Status = i.Status,
                Priority = i.Priority,
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
                    Status = i.Status,
                    Priority = i.Priority,
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
                DetailedDescription = item.DetailedDescription,
                Status = item.Status,
                Priority = item.Priority,
                Type = item.Type,
                Size = item.Size,
                DateCreated = item.DateCreated,
                DaysPending = item.DaysPending,                
                CreatorName = item.CreatorName,
                Notes = item.Notes.Select(n => new NoteDetail
                {
                    NoteText = n.NoteText,
                    NoteId = n.NoteId,
                    DateCreated = n.DateCreated,
                    DateModified = n.DateModified,
                    AuthorName = n.ApplicationUser.FullName
                }).ToList()
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
            itemToEdit.Priority = model.Priority;
            itemToEdit.Status = model.Status;
            itemToEdit.DetailedDescription = model.DetailedDescription;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        // Delete Bug Item
        public async Task<bool> DeleteBugItem(int itemId)
        {
            BugItem itemToDelete = await _context.BugItems.SingleAsync(i => i.ItemId == itemId);
            if (itemToDelete.Notes.Any())
            {
                foreach (var note in itemToDelete.Notes)
                {
                    _context.Notes.Remove(note);
                }
            }
            _context.BugItems.Remove(itemToDelete);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        //Reassign Bug Item
        public async Task<bool> ReassignBugItem(int itemId, WorkItemReassign model)
        {
            BugItem item = await _context.BugItems.FindAsync(itemId);
            if (item.Status == Status.Complete)
            {
                item.Status = Status.Reopened;
            }            
            item.ApplicationUserId = model.ApplicationUserId;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        //Complete Bug Item
        public async Task<bool> CompleteBugItem(int itemId, string userId)
        {
            BugItem item = await _context.BugItems.FindAsync(itemId);
            item.Status = Status.Complete;
            item.DateCompleted = DateTime.Now;
            if (userId == item.ApplicationUserId)
            {
                item.CompletedByName = item.ApplicationUser.FullName;
            }
            else
            {
                var currentUser = await _context.Users.FindAsync(userId);
                item.CompletedByName = currentUser.FullName;
            }
            item.ApplicationUserId = null;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }
    }
}
