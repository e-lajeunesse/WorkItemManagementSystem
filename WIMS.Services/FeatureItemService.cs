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

        public async Task<bool> EditFeatureItem(FeatureItemEdit model)
        {
            FeatureItem itemToEdit = await _context.FeatureItems.SingleAsync(i => i.ItemId == model.ItemId);
            itemToEdit.Description = model.Description;
            itemToEdit.Size = model.Size;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<bool> DeleteFeatureItem(int itemId)
        {
            FeatureItem itemToDelete = await _context.FeatureItems.SingleAsync(i => i.ItemId == itemId);
            _context.FeatureItems.Remove(itemToDelete);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }
    }
}
