using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpensesAPI.Models
{
    public class User
    {
        public User()
        {
            UserTransactions = new HashSet<Transaction>();
        }

        public int UserID { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public virtual ICollection<Transaction> UserTransactions { get; set; }
    }
}