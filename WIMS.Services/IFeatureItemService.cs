using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models;
using WIMS.Models.FeatureItemModels;

namespace WIMS.Services
{
    public interface IFeatureItemService
    {
        Task<bool> AddFeatureItem(FeatureItemCreate model, string userId, string userName);
        Task<IEnumerable<WorkItemListItem>> GetFeatureItems();
        Task<IEnumerable<WorkItemListItem>> GetFeatureItemsByUser(string userId);
        Task<FeatureItemDetail> GetFeatureItemById(int id);
        Task<bool> EditFeatureItem(FeatureItemEdit model);
        Task<bool> DeleteFeatureItem(int itemId);
        IEnumerable<WorkItemListItem> GetFeatureItemsByTeam(int? teamId);
    }
}
