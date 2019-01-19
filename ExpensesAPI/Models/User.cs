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
            Transactions = new HashSet<Transaction>();
        }

        public int UserID { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}