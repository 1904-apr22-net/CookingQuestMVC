using CookingQuest.Web.API_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingQuest.Web.Models
{
    public class PlayerViewModel
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Gold { get; set; }

        public IEnumerable<LootModel> Loot;
        public IEnumerable<EquipmentModel> Equipment;
        public IEnumerable<LocationModel> Locations;
    }
}
