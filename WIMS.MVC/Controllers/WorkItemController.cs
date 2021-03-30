using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIMS.Models;
using WIMS.Services;

namespace WIMS.MVC.Controllers
{
    public class WorkItemController : Controller
    {
        private readonly IBugItemService _bugService;
        private readonly IFeatureItemService _featureService;
        public WorkItemController(IBugItemService bugService,IFeatureItemService featureService)
        {
            _bugService = bugService;
            _featureService = featureService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<WorkItemListItem> bugItems =  await _bugService.GetBugItems();
            List<WorkItemListItem> bugItemList = bugItems.ToList();
            //IEnumerable<WorkItemListItem> featureItems = await _featureService.GetFeatureItems();
            //return bugItemList.AddRange(featureItemList);
            return View(bugItemList);
        }
    }
}
