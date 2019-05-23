using System;
using System.Collections.Generic;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class LootModel
    {
        public int LootId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; } = 0;
    }
}
