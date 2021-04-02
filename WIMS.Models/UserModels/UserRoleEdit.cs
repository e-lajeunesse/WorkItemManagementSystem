using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models.UserModels
{
    public class UserRoleEdit
    {
        public string RoleId { get; set; }
        [Required] 
        public string RoleName { get; set; }
        public List<string> Users { get; set; } = new List<string>();
    }
}
