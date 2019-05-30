using CookingQuest.Web.API_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingQuest.Web.Models
{
    public class StoreViewModel
    {
        public IEnumerable<LootModel> Loot { get; set; }
        public StoreModel Store { get; set; }
        public PlayerModel Player { get; set; }
        public IEnumerable<EquipmentModel> InStock { get; set; }
        public IEnumerable<EquipmentModel> Vouchers { get; set; }
        public IEnumerable<EquipmentModel> PlayerEquipment { get; set; }
        public IEnumerable<StoreModel> storeModels { get; set; }
        public int HighestVoucher { get; set; }
        public EquipmentModel NextVoucher { get; set; }
        public EquipmentModel CurrentVoucher { get; set; }
        public int StoreId { get; set; }

    }
}