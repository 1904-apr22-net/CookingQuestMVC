using System;
using System.Collections.Generic;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class PlayerLootModel
    {
        public int PlayerLootId { get; set; }
        public int PlayerId { get; set; }
        public int LootId { get; set; }
        public int Quantity { get; set; }
    }
}
