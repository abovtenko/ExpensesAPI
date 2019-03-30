using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExpensesCoreAPI.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Accounts = new HashSet<Account>();
        }

        public int UserID { get; set; }

        [JsonIgnore]
        public ICollection<Account> Accounts { get; set; }
    }
}