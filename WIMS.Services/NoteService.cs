using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models.NoteModels;
using WIMS.MVC.Data;

namespace WIMS.Services
{
    public class NoteService : INoteService
    {
        private ApplicationDbContext _context;
        public NoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Creates Note For Bug Item
        public async Task<bool> CreateBugNote(NoteCreate model, string userId)
        {
            Note note = new Note
            {
                NoteText = model.NoteText,
                DateCreated = DateTime.Now,
                ApplicationUserId = userId,
                BugItemId = model.ItemId
            };
            _context.Notes.Add(note);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        // Creates Note for Feature item
        public async Task<bool> CreateFeatureNote(NoteCreate model, string userId)
        {
            Note note = new Note
            {
                NoteText = model.NoteText,
                DateCreated = DateTime.Now,
                ApplicationUserId = userId,
                FeatureItemId = model.ItemId
            };
            _context.Notes.Add(note);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        //Gets Note by Note Id
        public async Task<NoteDetail> GetNoteById(int id, ItemType type)
        {
            Note noteToGet = await _context.Notes.FindAsync(id);
            NoteDetail detail = new NoteDetail
            {
                NoteId = noteToGet.NoteId,
                NoteText = noteToGet.NoteText,
                DateCreated = noteToGet.DateCreated,
                DateModified = noteToGet.DateModified,
                AuthorName = noteToGet.ApplicationUser.FullName,
                AuthorId = noteToGet.ApplicationUserId
            };
            detail.ItemId = type == ItemType.Bug ? noteToGet.BugItemId : noteToGet.FeatureItemId;
            return detail;           
        }

        //Deletes Note
        public async Task<bool> DeleteNote(int id)
        {
            Note noteToDelete = await _context.Notes.FindAsync(id);
            _context.Remove(noteToDelete);
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        //Edits Note
        public async Task<bool> EditNote(int id, NoteEdit model)
        {
            Note noteToEdit = await _context.Notes.FindAsync(id);
            noteToEdit.NoteText = model.NoteText;
            noteToEdit.DateModified = DateTime.Now;
            int changes = await _context.SaveChangesAsync();
            return changes == 1;
        }
    }
}
