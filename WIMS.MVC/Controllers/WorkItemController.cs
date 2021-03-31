using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models;
using WIMS.Models.BugItemModels;
using WIMS.Models.FeatureItemModels;
using WIMS.MVC.Data;
using WIMS.Services;

namespace WIMS.MVC.Controllers
{
    public class WorkItemController : Controller
    {
        private readonly IBugItemService _bugService;
        private readonly IFeatureItemService _featureService;
        private readonly UserManager<ApplicationUser> _userManager;


        public WorkItemController(IBugItemService bugService, IFeatureItemService featureService, UserManager<ApplicationUser> userManager)
        {
            _bugService = bugService;
            _featureService = featureService;
            _userManager = userManager;           
            
        }
        //GET: /WorkItem
        public async Task<IActionResult> Index()
        {
            
            IEnumerable<WorkItemListItem> bugItems =  await _bugService.GetBugItems();
            List<WorkItemListItem> bugItemList = bugItems.ToList();
            IEnumerable<WorkItemListItem> featureItems = await _featureService.GetFeatureItems();
            List<WorkItemListItem> featureItemList = featureItems.ToList();
            bugItemList.AddRange(featureItemList);           
            return View(bugItemList);
        }

        //Bug Item Methods

        //GET: /WorkItem/CreateBugItem
        public IActionResult CreateBugItem()
        {
            return View();
        }

        //POST: WorkItem/CreateBugItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBugItem(BugItemCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
/*            string ownerName = user.Result.UserName;
            var userId = _userManager.GetUserId(HttpContext.User);*/
            bool wasAdded = await _bugService.AddBugItem(model, user.Id, user.UserName);
            if (wasAdded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Unable to add bug");
            return View(model);
        }


        //Feature Item Methods

        //GET: WorkItem/CreateFeatureItem
        public IActionResult CreateFeatureItem()
        {
            return View();
        }

        //POST: WorkItem/CreateFeatureItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFeatureItem(FeatureItemCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            bool wasAdded = await _featureService.AddFeatureItem(model,user.Id, user.UserName);
            if (wasAdded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Unable to add feature");
            return View(model);
        }
    }
}
