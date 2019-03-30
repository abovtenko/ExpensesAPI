using System;
using System.Collections.Generic;

namespace ExpensesCoreAPI.Test.Integration
{
    public class SeedData
    {
        public List<Models.AppUser> Users { get; }
        public List<Models.Transaction> Transactions { get; }
        public List<Models.Account> Accounts { get; }

        public SeedData()
        {
            Transactions = new List<Models.Transaction>
            {
                new Models.Transaction
                {
                    TransactionDate = "2019-01-01",
                    Description = "test",
                    CreditAmount = 0.00,
                    DebitAmount = 50.00
                }
            };
            Accounts = new List<Models.Account>
            {
                new Models.Account
                {
                    Provider = "",
                    Balance = 100.00,
                    Type = "test",
                    DateOpened = new DateTime(2020, 01, 01),
                    DateClosed = null
                }
            };
            Users = new List<Models.AppUser>
            {
                new Models.AppUser { UserName = "UserOne" },
                new Models.AppUser { UserName = "UserTwo" }
            };
        }

        

        public static void InitializeTestDB(ExpensesCoreAPI.Data.ExpensesContext db)
        {
            var users = GetTestUsers();
            var transactions = GetTestTransactions();
            var accounts = GetTestAccounts();

            db.Transactions.AddRange(transactions);
            db.Accounts.AddRange(accounts);
            db.AppUsers.AddRange(users);
            db.SaveChanges();
        }

        public static List<Models.AppUser> GetTestUsers()
        {
            return new List<Models.AppUser>
            {
                new Models.AppUser { UserName = "UserOne" },
                new Models.AppUser { UserName = "UserTwo" }
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

        public static List<Models.Account> GetTestAccounts()
        {
            return new List<Models.Account>
            {
                new Models.Account
                {
                    Provider = "",
                    Balance = 100.00,
                    Type = "test",
                    DateOpened = new DateTime(2020, 01, 01),
                    DateClosed = null
                }
            };
        }
    }
}
