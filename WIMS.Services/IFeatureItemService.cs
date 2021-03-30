using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WIMS.Models;
using WIMS.Models.FeatureItemModels;

namespace WIMS.Services
{
    public interface IFeatureItemService
    {
        Task<bool> AddFeatureItem(FeatureItemCreate model, IdentityUser user);
        Task<IEnumerable<WorkItemListItem>> GetFeatureItems();
    }
}
