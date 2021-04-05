using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    [Authorize]
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
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            ViewBag.IsManager = user.IsManager;
            if (user.IsManager && user.TeamId != null)
            {
                List<WorkItemListItem> bugItemList =  _bugService.GetBugItemsByTeam(user.TeamId).ToList();
                List<WorkItemListItem> featureItemList = _featureService.GetFeatureItemsByTeam(user.TeamId).ToList();
                bugItemList.AddRange(featureItemList);
                return View(bugItemList);
            }
            else
            {
                IEnumerable<WorkItemListItem> bugItems = await _bugService.GetBugItemsByUser(user.Id);
                List<WorkItemListItem> bugItemList = bugItems.ToList();
                IEnumerable<WorkItemListItem> featureItems = await _featureService.GetFeatureItemsByUser(user.Id);
                List<WorkItemListItem> featureItemList = featureItems.ToList();
                bugItemList.AddRange(featureItemList);           
                return View(bugItemList);
            }
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

            bool wasAdded = await _bugService.AddBugItem(model, user.Id, user.FullName);
            if (wasAdded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Unable to add bug");
            return View(model);
        }

        //GET: WorkItem/Detail
        [ActionName("BugItemDetails")]
        public async Task<IActionResult> BugItemDetails(int id)
        {
            BugItemDetail bugItem = await _bugService.GetBugItemById(id);
            return View(bugItem);
        }

        //GET: WorkItem/Delete/BugItem
        [ActionName("DeleteBugItem")]
        public IActionResult DeleteBugItem(int id)
        {
            BugItemDetail bugItem = _bugService.GetBugItemById(id).Result;
            return View(bugItem);
        }

        //POST: WorkItem/Delete/BugItem
        [ActionName("DeleteBugItem")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBugItemPost(int id)
        {
            if (await _bugService.DeleteBugItem(id))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        
        //GET: WorkItem/EditBugItem
        [ActionName("EditBugItem")]
        public IActionResult EditBugItem(int id)
        {
            BugItemDetail bugItem = _bugService.GetBugItemById(id).Result;
            BugItemEdit model = new BugItemEdit
            {
                ItemId = bugItem.ItemId,
                Description = bugItem.Description,
                Size = bugItem.Size
            };
            return View(model);
        }

        //POST: WorkItem/EditBugItem
        [ActionName("EditBugItem")]
        [HttpPost]
        public async Task<IActionResult> EditBugItem(int id, BugItemEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.ItemId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            bool wasUpdated = await _bugService.EditBugItem(model);
            if (wasUpdated)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Unable to update item");
            return View(model);
        }

        //GET: WorkItem/ReassignItem/{id}
        [ActionName("ReassignBugItem")]
        public IActionResult ReassignBugItem(int id)
        {
            var users = _userManager.Users;
            var usersList = new List<SelectListItem>();
            foreach (var user in users)
            {
                usersList.Add(new SelectListItem
                {
                    Text = user.FullName,
                    Value = user.Id
                });
            }
            ViewBag.users = usersList;
            return View();
        }

        //POST: WorkItem/ReassignBugItem/{id}
        [HttpPost]
        public async Task<IActionResult> ReassignBugItem(int id, WorkItemReassign model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ReassignBugItem",id);
            }
            bool wasReassigned = await _bugService.ReassignBugItem(id, model);
            if (wasReassigned)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //GET: WorkItem/CompleteBugItem/{id}
        public async Task<IActionResult> CompleteBugItem(int id)
        {
            BugItemDetail bugItem = await _bugService.GetBugItemById(id);
            return View(bugItem);
        }

        //POST: WorkItem/CompleteBugItem/{id}
        [ActionName("CompleteBugItem")]
        [HttpPost]
        public async Task<IActionResult> CompleteBugItemPost(int id)
        {
            bool wasCompleted = await _bugService.CompleteBugItem(id);
            if (wasCompleted)
            {
                return RedirectToAction("Index");
            }
            return View();
        }





        // Feature Item Methods

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
            bool wasAdded = await _featureService.AddFeatureItem(model,user.Id, user.FullName);
            if (wasAdded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Unable to add feature");
            return View(model);
        }

        //GET: WorkItem/FeatureItemDetails
        [ActionName("FeatureItemDetails")]
        public async Task<IActionResult> FeatureItemDetails(int id)
        {
            FeatureItemDetail featureItem = await _featureService.GetFeatureItemById(id);
            return View(featureItem);
        }

        //GET: WorkItem/EditFeatureItem
        [ActionName("EditFeatureItem")]
        public IActionResult EditFeatureItem(int id)
        {
            FeatureItemDetail featureItem = _featureService.GetFeatureItemById(id).Result;
            FeatureItemEdit model = new FeatureItemEdit
            {
                ItemId = featureItem.ItemId,
                Description = featureItem.Description,
                Size = featureItem.Size
            };
            return View(model);
        }

        //POST: WorkItem/EditFeatureItem
        [ActionName("EditFeatureItem")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFeatureItem(int id, FeatureItemEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.ItemId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            bool wasUpdated = await _featureService.EditFeatureItem(model);
            if (wasUpdated)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Unable to update item");
            return View();
        }

        //GET: WorkItem/Delete/FeatureItem
        [ActionName("DeleteFeatureItem")]
        public IActionResult DeleteFeatureItem(int id)
        {
            FeatureItemDetail featureItem = _featureService.GetFeatureItemById(id).Result;
            return View(featureItem);
        }

        //POST: WorkItem/Delete/FeatureItem
        [ActionName("DeleteFeatureItem")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFeatureItemPost(int id)
        {
            if ( await _featureService.DeleteFeatureItem(id))
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET: WorkItem/ReassignFeatureItem/{id}
        [ActionName("ReassignFeatureItem")]
        public IActionResult ReassignFeatureItem(int id)
        {
            var users = _userManager.Users;
            var usersList = new List<SelectListItem>();
            foreach (var user in users)
            {
                usersList.Add(new SelectListItem
                {
                    Text = user.FullName,
                    Value = user.Id
                });
            }
            ViewBag.users = usersList;
            return View();
        }

        //POST: WorkItem/ReassignBugItem/{id}
        [HttpPost]
        public async Task<IActionResult> ReassignFeatureItem(int id, WorkItemReassign model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ReassignFeatureItem",id);
            }
            bool wasReassigned = await _featureService.ReassignFeatureItem(id, model);
            if (wasReassigned)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //GET: WorkItem/CompleteFeatureItem/{id}
        public async Task<IActionResult> CompleteFeatureItem(int id)
        {
            FeatureItemDetail featureItem = await _featureService.GetFeatureItemById(id);
            return View(featureItem);
        }

        //POST: WorkItem/CompleteBugItem/{id}
        [ActionName("CompleteFeatureItem")]
        [HttpPost]
        public async Task<IActionResult> CompleteFeatureItemPost(int id)
        {
            bool wasCompleted = await _featureService.CompleteFeatureItem(id);
            if (wasCompleted)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
