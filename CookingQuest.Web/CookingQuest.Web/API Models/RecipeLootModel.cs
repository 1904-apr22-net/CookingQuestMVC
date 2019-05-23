using System;
using System.Collections.Generic;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class RecipeLootModel
    {
        public int RecipeLootId { get; set; }
        public int RecipeId { get; set; }
        public int LootId { get; set; }
    }
}
