﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingQuest.Web.API_Models
{
    public class FlavorModel
    {
        public int FlavorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Bonus { get; set; }
    }
}
