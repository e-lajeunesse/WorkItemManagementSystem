using System;
using System.Collections.Generic;
using System.Text;
using WIMS.Data;

namespace WIMS.Models.FeatureItemModels
{
    public class FeatureItemEdit
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public Size Size { get; set; }
    }
}
