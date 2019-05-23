using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingQuest.Web.API_Models
{
    public class LocationModel
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public int Difficulty { get; set; }
        public string Description { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Description))
            {
                return false;
            }
            if (Difficulty <= 0)
            {
                return false;
            }
            return true;

        }
    }
}
