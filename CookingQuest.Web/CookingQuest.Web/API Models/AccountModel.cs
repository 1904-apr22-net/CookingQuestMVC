using System;
using System.Collections.Generic;
using System.Text;

namespace CookingQuest.Web.API_Models
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int PlayerId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
