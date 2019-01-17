using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ExpensesAPI.Models;

namespace ExpensesAPI.Data
{
    public class ExpensesContext : DbContext
    {
        public ExpensesContext() : base("DefaultConnection")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}