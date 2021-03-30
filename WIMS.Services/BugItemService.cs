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
        public async Task<bool> AddBugItem (BugItemCreate model)
        {
            BugItem item = new BugItem
            {
                Description = model.Description,
                Size = model.Size
            };
            await _context.BugItems.AddAsync(item);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<IEnumerable<WorkItemListItem>> GetBugItems()
        {
            List<BugItem> items = await _context.BugItems.ToListAsync();
            IEnumerable<WorkItemListItem> listItems = items.Select(i => new WorkItemListItem
            {
                ItemId = i.ItemId,
                Description = i.Description,
                Size = i.Size
            });
            return listItems;
        }
    }
}
