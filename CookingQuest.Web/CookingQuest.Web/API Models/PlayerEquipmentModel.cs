using System;
using System.Collections.Generic;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class PlayerEquipmentModel
    {
        public int PlayerEquipmentId { get; set; }
        public int PlayerId { get; set; }
        public int EquipmentId { get; set; }
    }
}
