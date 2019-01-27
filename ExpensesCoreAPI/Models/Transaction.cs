using Newtonsoft.Json;

namespace ExpensesCoreAPI.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public string TransactionDate { get; set; }
        public string Description { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }  
        
        public int UserID { get; set; }

        [JsonIgnore]
        public virtual User TransactionUser { get; set; }
    }
}
