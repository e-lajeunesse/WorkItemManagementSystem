using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models.NoteModels
{
    public class NoteDetail
    {
        public int NoteId { get; set; }
        public string NoteText { get; set; }

        [Display(Name ="Author")]
        public string AuthorName { get; set; }
    }
}
