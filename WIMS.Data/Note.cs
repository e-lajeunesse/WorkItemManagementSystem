using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WIMS.Data
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        public string NoteText { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        [ForeignKey(nameof(BugItem))]
        public int? BugItemId { get; set; }

        [ForeignKey(nameof(FeatureItem))]
        public int? FeatureItemId { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
