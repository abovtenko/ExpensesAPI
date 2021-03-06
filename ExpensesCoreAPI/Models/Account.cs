﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ExpensesCoreAPI.Models
{
    public class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int AccountID { get; set; }
        public int UserID { get; set; }
        public string Provider { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }
        public DateTime? DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }

        [JsonIgnore]
        public AppUser AccountUser { get; set; }
        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
