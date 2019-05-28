using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class PlayerModel
    {
        public int PlayerId { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [RegularExpression("^[a-zA-Z -]*$", ErrorMessage = "Only Letters from the Alphabet")]
        public string Name { get; set; }
        [Required]
        [Range(1, 1000000, ErrorMessage = "Please enter correct value (Between 1-1000000")]
        public int Gold { get; set; }
    }
}
