using System;
using System.Collections.Generic;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class PlayerLocationModel
    {
        public int PlayerLocationId { get; set; }
        public int PlayerId { get; set; }
        public int LocationId { get; set; }
    }
}
