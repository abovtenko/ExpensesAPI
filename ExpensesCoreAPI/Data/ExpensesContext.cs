using Microsoft.EntityFrameworkCore;
using ExpensesCoreAPI.Models;

namespace ExpensesCoreAPI.Data
{
    public class ExpensesContext : DbContext
    {
        public ExpensesContext(DbContextOptions<ExpensesContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}