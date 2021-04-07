using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models.NoteModels
{
    public class NoteCreate
    {
        [Required]
        public string NoteText { get; set; }

    }
}
