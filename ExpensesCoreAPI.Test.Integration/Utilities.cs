using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpensesCoreAPI.Test.Integration
{
    class Utilities
    {
        public static void InitializeTestDB(ExpensesCoreAPI.Data.ExpensesContext db)
        {
            var users = GetTestUsers();
            var transactions = GetTestTransactions();

            db.Transactions.AddRange(transactions);
            db.Users.AddRange(users);
            db.SaveChanges();
        }

        public static List<Models.User> GetTestUsers()
        {
            return new List<Models.User>
            {
                new Models.User { Username = "UserOne" },
                new Models.User { Username = "UserTwo" }
            };
        }

        public static List<Models.Transaction> GetTestTransactions()
        {
            return new List<Models.Transaction>
            {
                new Models.Transaction
                {
                    TransactionDate = "2019-01-01",
                    Description = "test",
                    CreditAmount = 0.00,
                    DebitAmount = 50.00
                }
            };
        }
    }
}
