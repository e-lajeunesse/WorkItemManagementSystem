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
        public async Task<bool> AddFeatureItem(FeatureItemCreate model, string userId, string userName)
        {
            FeatureItem item = new FeatureItem
            {
                Description = model.Description,
                Size = model.Size,
                DateCreated = DateTime.Now,
                IsComplete = false,
                CreatorName = userName,
                ApplicationUserId = userId,
                
            };
            await _context.FeatureItems.AddAsync(item);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        //Gets all feature items
        public async Task<IEnumerable<WorkItemListItem>> GetFeatureItems()
        {            
            return await _context.FeatureItems.Select(i => new WorkItemListItem
            {
                ItemId = i.ItemId,
                Description = i.Description,
                Type = i.Type,
                Size = i.Size,
                DaysPending = i.DaysPending,
                OwnerName = i.ApplicationUser.UserName
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
                Type = i.Type,
                Size = i.Size,
                DaysPending = i.DaysPending,
                OwnerName = user.UserName
            });
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
                    Type = i.Type,
                    Size = i.Size,
                    DaysPending = i.DaysPending,
                    OwnerName = i.ApplicationUser.UserName
                });
        }

        // Gets feature item by item id
        public async Task<FeatureItemDetail> GetFeatureItemById(int id)
        {
            FeatureItem item = await _context.FeatureItems.SingleAsync(i => i.ItemId == id);
            return new FeatureItemDetail
            {
                ItemId = item.ItemId,
                Description = item.Description,
                Type = item.Type,
                Size = item.Size,
                DateCreated = item.DateCreated,
                DaysPending = item.DaysPending,
                CreatorName = item.CreatorName,
                ApplicationUserId = item.ApplicationUserId,
                UserName = item.ApplicationUser.UserName
            };
        }

        //Edits feature item
        public async Task<bool> EditFeatureItem(FeatureItemEdit model)
        {
            FeatureItem itemToEdit = await _context.FeatureItems.SingleAsync(i => i.ItemId == model.ItemId);
            itemToEdit.Description = model.Description;
            itemToEdit.Size = model.Size;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        //Deletes feature item
        public async Task<bool> DeleteFeatureItem(int itemId)
        {
            FeatureItem itemToDelete = await _context.FeatureItems.SingleAsync(i => i.ItemId == itemId);
            _context.FeatureItems.Remove(itemToDelete);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }
    }
}
