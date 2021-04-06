using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models.TeamModels
{
    public class TeamEdit
    {
        public int TeamId { get; set; }

        [Required]
        [Display(Name ="Team Name")]
        public string TeamName { get; set; }
        public List<string> Employees { get; set; } = new List<string>();
    }
}
