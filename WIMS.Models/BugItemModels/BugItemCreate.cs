﻿using System;
using System.Collections.Generic;
using System.Text;
using WIMS.Data;

namespace WIMS.Models.BugItemModels
{
    public class BugItemCreate
    {
        public string Description { get; set; }
        public Size Size { get; set; }
    }
}
