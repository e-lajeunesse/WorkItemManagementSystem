using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIMS.Models.UserModels
{
    public class UserRoleCreate
    {
        [Required]
        public string RoleName { get; set; }
    }
}
