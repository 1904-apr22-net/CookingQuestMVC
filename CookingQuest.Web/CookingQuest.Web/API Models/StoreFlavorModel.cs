using System;
using System.Collections.Generic;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class StoreFlavorModel
    {
        public int StoreFlavorId { get; set; }
        public int StoreId { get; set; }
        public int FlavorId { get; set; }
        public int Bonus { get; set; }
    }
}
