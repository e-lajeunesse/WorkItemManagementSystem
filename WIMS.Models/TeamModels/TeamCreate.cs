using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models.TeamModels
{
    public class TeamCreate
    {
        [Required]
        public string TeamName { get; set; }        
    }
}
