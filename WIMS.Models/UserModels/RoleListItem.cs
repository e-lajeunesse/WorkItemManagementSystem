using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models.UserModels
{
    public class RoleListItem
    {
        [Display(Name="Role Id")]
        public string RoleId { get; set; }

        [Required]
        [Display(Name ="Role Name")]
        public string Name { get; set; }
    }
}
