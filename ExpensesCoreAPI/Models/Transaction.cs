using Newtonsoft.Json;

namespace ExpensesCoreAPI.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public string TransactionDate { get; set; }
        public string Description { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }  

        [JsonIgnore]
        public virtual Account TransactionAccount { get; set; }
    }
}
