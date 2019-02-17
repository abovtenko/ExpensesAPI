using ExpensesCoreAPI.Data;
using ExpensesCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpensesCoreAPI.Services
{
    public class TransactionService : IService<Transaction>
    {
        private readonly ExpensesContext _context;

        public TransactionService(ExpensesContext context)
        {
            _context = context;
        }

        public void Create(Transaction model)
        {
            _context.Transactions.Add(model);
        }

        public Transaction Get(int id)
        {
            return _context.Transactions.Where(x => x.TransactionID == id).SingleOrDefault();
        }

        public IEnumerable<Transaction> GetWhere(Expression<Func<Transaction, bool>> predicate)
        {
            return _context.Transactions.Where(predicate).ToList();
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions.ToList();
        }

        public void Update(Transaction model)
        {
            var target = _context.Transactions.Where(x => x.TransactionID == model.TransactionID).SingleOrDefault();

            if (target != null)
            {
                target.TransactionAccount.AccountID = model.TransactionAccount.AccountID;
                target.TransactionDate = model.TransactionDate;
                target.Description = model.Description;
                target.CreditAmount = model.CreditAmount;
                target.DebitAmount = model.DebitAmount;
            }
        }

        public void Remove(int id)
        {
            var target = _context.Transactions.Where(x => x.TransactionID == id).SingleOrDefault();
            if (target != null)
            {
                _context.Transactions.Remove(target);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}