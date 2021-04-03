using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WIMS.Data
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<BugItem> BugItems { get; set; }
        public virtual List<FeatureItem> FeatureItems { get; set; }
        public bool IsManager { get; set; }
        
        [ForeignKey(nameof(Team))]
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
