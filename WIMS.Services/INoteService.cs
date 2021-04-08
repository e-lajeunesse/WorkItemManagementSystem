using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models.NoteModels;

namespace WIMS.Services
{
    public interface INoteService
    {
        Task<bool> CreateBugNote(NoteCreate model, string userId);
        Task<bool> CreateFeatureNote(NoteCreate model, string userId);
        Task<bool> DeleteNote(int id);
        Task<NoteDetail> GetNoteById(int id, ItemType type);

        Task<bool> EditNote(int id, NoteEdit model);
    }
}
