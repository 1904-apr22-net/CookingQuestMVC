using System;
using System.Collections.Generic;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class StoreEquipmentModel
    {
        public int StoreEquipmentId { get; set; }
        public int StoreId { get; set; }
        public int EquipmentId { get; set; }
    }
}
