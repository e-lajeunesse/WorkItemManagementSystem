using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models.UserModels
{
    public class UserRoleEdit
    {
        [Display(Name ="Role Id")]
        public string RoleId { get; set; }
        [Required] 
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; } = new List<string>();
    }
}
