using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WIMS.Data
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<BugItem> BugItems { get; set; }
        public virtual List<FeatureItem> FeatureItems { get; set; }
    }
}
