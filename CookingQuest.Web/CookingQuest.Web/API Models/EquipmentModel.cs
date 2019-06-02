using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class EquipmentModel
    {
        public int EquipmentId { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Please enter correct value (Between 1-1000")]
        public int Modifier { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string Name { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Please enter correct value (Between 1-1000")]
        public int Price { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string Type { get; set; }

        [Required]
        [Range(1, 9, ErrorMessage = "Please enter correct value (Between 1-1000")]
        public int Difficulty { get; set; }
        public int PlayerEquipmentId { get; set; }
    }
}
