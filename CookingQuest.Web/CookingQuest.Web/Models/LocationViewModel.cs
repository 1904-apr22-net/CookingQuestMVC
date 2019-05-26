using CookingQuest.Web.API_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingQuest.Web.Models
{
    public class LocationViewModel
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public int Difficulty { get; set; }
        public string Description { get; set; }

        public IEnumerable<LootModel> Loot { get; set; }
    }
}
