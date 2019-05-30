using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class LootModel
    {
        public int LootId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression("^[a-zA-Z -]*$", ErrorMessage = "Only Letters from the Alphabet")]
        public string Name { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Please enter correct value (Between 1-100000")]
        public int Price { get; set; } = 1;

        [Required]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression("^[a-zA-Z -]*$", ErrorMessage = "Only Letters from the Alphabet")]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Please enter correct value (Between 1-1000")]
        public int Quantity { get; set; } = 1;

        [Required]
        [Range(1, 1000, ErrorMessage = "Please enter correct value (Between 1-1000")]
        public int DropRate { get; set; } = 1;
        public int LocationLootId { get; set; }
        public FlavorModel Flavor { get; set; }
        public int PlayerLootId { get; set; }
        public int FlavorLootId { get; set; }
    }
}
