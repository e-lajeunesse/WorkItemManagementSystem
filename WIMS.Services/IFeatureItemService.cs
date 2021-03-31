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
    }
}
