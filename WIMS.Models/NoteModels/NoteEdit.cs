using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models.NoteModels
{
    public class NoteEdit
    {       
        [Required]
        public string NoteText { get; set; }
        public int? ItemId { get; set; }
    }
}
