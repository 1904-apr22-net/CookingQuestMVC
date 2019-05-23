using System;
using System.Collections.Generic;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class EquipmentModel
    {
        public int EquipmentId { get; set; }
        public int Modifier { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public int Difficulty { get; set; }
    }
}
