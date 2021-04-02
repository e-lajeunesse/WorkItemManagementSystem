using System;
using System.Collections.Generic;
using System.Text;

namespace WIMS.Models.UserModels
{
    public class UsersInRoleEdit
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
