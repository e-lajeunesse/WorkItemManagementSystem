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
    }
}
