using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExpensesCoreAPI.Models
{
    public class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
        }

        public int UserID { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public ICollection<Account> Accounts { get; set; }
    }
}