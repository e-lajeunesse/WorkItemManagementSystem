﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WIMS.Models.TeamModels
{
    public class TeamMembersEdit
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
        public bool IsManager { get; set; }
    }
}
