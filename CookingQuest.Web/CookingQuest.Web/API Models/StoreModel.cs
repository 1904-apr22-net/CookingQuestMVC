﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class StoreModel
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public List<FlavorModel> Flavors { get; set; } = new List<FlavorModel>();
    }
}
