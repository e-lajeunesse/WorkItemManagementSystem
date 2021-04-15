using System;
using System.Collections.Generic;
using System.Text;
using WIMS.Models.TeamModels;

namespace WIMS.Models.UserModels
{
    public class UserDisplay
    {
        public string FullName { get; set; }
        public bool IsManager { get; set; }
        public int? TeamId { get; set; }
        public TeamDetail Team { get; set; } 
    }
}
