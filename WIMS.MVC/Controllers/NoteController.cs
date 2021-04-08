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

        //GET: Note/DeleteBugNote/{id}
        public async Task<IActionResult> DeleteBugNote(int id)
        {
            NoteDetail note = await _service.GetNoteById(id, ItemType.Bug);
            ViewBag.ItemId = id;
            return View(note);
        }

        //Post: Note/DeleteBugNote/{id}
        [ActionName("DeleteBugNote")]
        [HttpPost]
        public async Task<IActionResult> DeleteBugNotePost(int id)
        {
            NoteDetail note = await _service.GetNoteById(id, ItemType.Bug);
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.Id != note.AuthorId && !user.IsManager)
            {
                ViewBag.ErrorMessage = "Only the Author or a Manager is allowed to delete a note";
                return View(note);
            }

            if (await _service.DeleteNote(id))
            {
                return RedirectToAction("BugItemDetails", "WorkItem", new { id = note.ItemId });
            }
            return View();
        }

        //GET: Note/DeleteFeatureNote/{id}
        public async Task<IActionResult> DeleteFeatureNote(int id)
        {
            NoteDetail note = await _service.GetNoteById(id, ItemType.Feature);
            ViewBag.ItemId = id;
            return View(note);
        }

        //Post: Note/DeleteFeatureNote/{id}
        [ActionName("DeleteFeatureNote")]
        [HttpPost]
        public async Task<IActionResult> DeleteFeatureNotePost(int id)
        {
            NoteDetail note = await _service.GetNoteById(id, ItemType.Feature);
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.Id != note.AuthorId && !user.IsManager)
            {
                ViewBag.ErrorMessage = "Only the Author or a Manager is allowed to delete a note";
                return View(note);
            }
            if (await _service.DeleteNote(id))
            {
                return RedirectToAction("FeatureItemDetails", "WorkItem", new { id = note.ItemId });
            }
            return View(note);
        }

        //GET: Note/EditBugNote/{id}
        public async Task<IActionResult> EditBugNote(int id)
        {
            NoteDetail detail = await _service.GetNoteById(id, ItemType.Bug);
            NoteEdit model = new NoteEdit
            {
                NoteText = detail.NoteText,
                ItemId = detail.ItemId
            };
            return View(model);
        }


        //POST: Note/EditBugNote/{id}
        [HttpPost]
        public async Task<IActionResult> EditBugNote(int id, NoteEdit model)
        {
            NoteDetail detail = await _service.GetNoteById(id, ItemType.Bug);
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (user.Id != detail.AuthorId)
            {
                ViewBag.ErrorMessage = "Only the Author is allowed to edit a note";
                return View(model);
            }
            bool wasEdited = await _service.EditNote(id, model);
            if (wasEdited)
            {
                return RedirectToAction("BugItemDetails", "WorkItem", new { id = detail.ItemId });
            }
            ViewBag.ErrorMessage = "Unable to edit note";
            return View(model);
        }

        //GET: Note/EditFeatureNote/{id}
        public async Task<IActionResult> EditFeatureNote(int id)
        {
            NoteDetail detail = await _service.GetNoteById(id, ItemType.Feature);
            NoteEdit model = new NoteEdit
            {
                NoteText = detail.NoteText,
                ItemId = detail.ItemId
            };
            return View(model);
        }

        //POST: Note/EditFeatureNote/{id}
        [HttpPost]
        public async Task<IActionResult> EditFeatureNote(int id, NoteEdit model)
        {
            NoteDetail detail = await _service.GetNoteById(id, ItemType.Feature);
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (user.Id != detail.AuthorId)
            {
                ViewBag.ErrorMessage = "Only the Author is allowed to edit a note";
                return View(model);
            }
            bool wasEdited = await _service.EditNote(id, model);
            if (wasEdited)
            {
                return RedirectToAction("FeatureItemDetails", "WorkItem", new { id = detail.ItemId });
            }
            ViewBag.ErrorMessage = "Unable to edit note";
            return View(model);
        }

    }
}
