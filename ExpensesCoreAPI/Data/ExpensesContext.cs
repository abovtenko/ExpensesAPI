using Microsoft.EntityFrameworkCore;
using ExpensesCoreAPI.Models;
using System.Collections.Generic;

namespace ExpensesCoreAPI.Data
{
    public class ExpensesContext : DbContext
    {
        public ExpensesContext(DbContextOptions<ExpensesContext> options) : base(options)
        { }
                
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new User {
                UserID = 1,
                Username = "UserAlpha"
            });
            builder.Entity<Transaction>().HasData(new Transaction
            {
                TransactionID = 1,
                UserID = 1,
                CreditAmount = 0.00,
                DebitAmount = 34.50,
                Description = "misc",
                TransactionDate = "2019-01-01"
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}