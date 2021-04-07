using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models.BugItemModels;
using WIMS.Models.FeatureItemModels;
using WIMS.Models.NoteModels;
using WIMS.Services;

namespace WIMS.MVC.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public NoteController(INoteService service, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        //POST:Note/CreateBugItemNote
        [HttpPost]
        public async Task<IActionResult> CreateBugItemNote(BugItemDetail model)
        {
            var noteCreate = new NoteCreate
            {
                ItemId = model.ItemId,
                NoteText = model.NoteText
            };
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            bool wasAdded = await _service.CreateBugNote(noteCreate, user.Id);
            if (wasAdded)
            {
                return RedirectToAction("BugItemDetails", "WorkItem", new { id = model.ItemId } );
            }
            return View(model);
        }

        //POST: Note/CreateFeatureItemNote
        [HttpPost]
        public async Task<IActionResult> CreateFeatureItemNote(FeatureItemDetail model)
        {
            var noteCreate = new NoteCreate
            {
                ItemId = model.ItemId,
                NoteText = model.NoteText
            };
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            bool wasAdded = await _service.CreateFeatureNote(noteCreate, user.Id);
            if (wasAdded)
            {
                return RedirectToAction("FeatureItemDetails", "WorkItem", new { id = model.ItemId });
            }
            return RedirectToAction("FeatureItemDetails", "WorkItem", new { id = model.ItemId });

        }
    }
}
