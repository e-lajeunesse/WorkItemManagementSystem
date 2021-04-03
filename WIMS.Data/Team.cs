using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WIMS.Data
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }

/*        public string ApplicationUserId { get; set; }
        public ApplicationUser Manager { get; set; }*/
        public virtual List<ApplicationUser> Users { get; set; }

    }
}
