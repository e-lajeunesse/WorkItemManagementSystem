using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models;
using WIMS.Models.FeatureItemModels;
using WIMS.Models.NoteModels;
using WIMS.MVC.Data;

namespace WIMS.Services
{
    public class FeatureItemService : IFeatureItemService
    {
        private ApplicationDbContext _context;
        public FeatureItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Creates new feature item
        public async Task<bool> AddFeatureItem(FeatureItemCreate model, string userId, string fullName)
        {
            FeatureItem item = new FeatureItem
            {
                Description = model.Description,
                Size = model.Size,
                DetailedDescription = model.DetailedDescription,
                Priority = model.Priority,
                DateCreated = DateTime.Now,
                DateCompleted = null,                
                CreatorName = fullName,
                ApplicationUserId = userId
            };
            await _context.FeatureItems.AddAsync(item);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        //Gets all pending feature items
        public async Task<List<WorkItemListItem>> GetFeatureItems()
        {
            return await _context.FeatureItems.Where(i => i.Status != Status.Complete)
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

        //Gets all Completed Feature Items
        public async Task<List<CompletedItemListItem>> GetCompletedFeatureItems()
        {
            return await _context.FeatureItems.Where(i => i.Status == Status.Complete)
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

        //Gets all feature items for specific user        
        public async Task<IEnumerable<WorkItemListItem>> GetFeatureItemsByUser(string userId)
        {
            ApplicationUser user = await _context.Users.FindAsync(userId);
            return user.FeatureItems.Select(i => new WorkItemListItem
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

        //Gets all feature items for a team
        public IEnumerable<WorkItemListItem> GetFeatureItemsByTeam(int? teamId)
        {
            if (teamId == null)
            {
                return null;
            }
            return _context.FeatureItems.Where(i => i.ApplicationUser.TeamId == teamId)
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

        // Gets feature item by item id
        public async Task<FeatureItemDetail> GetFeatureItemById(int id)
        {
            FeatureItem item = await _context.FeatureItems.SingleAsync(i => i.ItemId == id);
            var detail = new FeatureItemDetail
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

        //Edits feature item
        public async Task<bool> EditFeatureItem(FeatureItemEdit model)
        {
            FeatureItem itemToEdit = await _context.FeatureItems.SingleAsync(i => i.ItemId == model.ItemId);
            itemToEdit.Description = model.Description;
            itemToEdit.DetailedDescription = model.DetailedDescription;
            itemToEdit.Status = model.Status;
            itemToEdit.Priority = model.Priority;
            itemToEdit.Size = model.Size;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        //Deletes feature item
        public async Task<bool> DeleteFeatureItem(int itemId)
        {
            FeatureItem itemToDelete = await _context.FeatureItems.SingleAsync(i => i.ItemId == itemId);
            if (itemToDelete.Notes.Any())
            {
                foreach (var note in itemToDelete.Notes)
                {
                    _context.Notes.Remove(note);
                }
            }
            _context.FeatureItems.Remove(itemToDelete);
            int changes = await _context.SaveChangesAsync();
            return changes > 0;
        }


        //Reassign Feature Item
        public async Task<bool> ReassignFeatureItem(int itemId, WorkItemReassign model)
        {
            FeatureItem item = await _context.FeatureItems.FindAsync(itemId);
            if (item.Status == Status.Complete)
            {
                item.Status = Status.Reopened;
            }
            item.ApplicationUserId = model.ApplicationUserId;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        //Complete Feature Item
        public async Task<bool> CompleteFeatureItem(int itemId, string userId)
        {
            FeatureItem item = await _context.FeatureItems.FindAsync(itemId);
            item.Status = Status.Complete;
            item.DateCompleted = DateTime.Now;
            if (userId == item.ApplicationUserId)
            {
                item.CompletedByName = userId;
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
