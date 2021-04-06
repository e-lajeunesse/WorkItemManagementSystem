using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models.TeamModels
{
    public class TeamDetail
    {
        [Display(Name ="Team Id")]
        public int TeamId { get; set; }

        [Display(Name ="Team Name")]
        public string TeamName { get; set; }

        [Display(Name = "Manager")]
        public string ManagerName { get; set; }

        [Display(Name = "Team Members")]
        public List<string> EmployeeNames { get; set; } = new List<string>();
    }
}
