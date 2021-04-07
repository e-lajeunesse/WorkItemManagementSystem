using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WIMS.Models.NoteModels;

namespace WIMS.Services
{
    public interface INoteService
    {
        Task<bool> CreateBugNote(NoteCreate model, string userId);
        Task<bool> CreateFeatureNote(NoteCreate model, string userId);
    }
}
