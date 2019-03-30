using Microsoft.EntityFrameworkCore;
using ExpensesCoreAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExpensesCoreAPI.Data
{
    public class ExpensesContext : IdentityDbContext
    {
        public ExpensesContext(DbContextOptions<ExpensesContext> options) : base(options)
        { }             

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}