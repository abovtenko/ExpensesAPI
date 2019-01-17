using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesAPI.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public string TransactionDate { get; set; }
        public string Description { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }        
    }
}
