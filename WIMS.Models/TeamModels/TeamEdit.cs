using System;
using System.Collections.Generic;
using System.Text;

namespace WIMS.Models.TeamModels
{
    public class TeamEdit
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public List<string> Employees { get; set; } = new List<string>();
    }
}
