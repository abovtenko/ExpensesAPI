using ExpensesCoreAPI.Data;
using ExpensesCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpensesCoreAPI.Services
{
    public class AccountService : IService<Account>
    {
        private readonly ExpensesContext _context;

        public AccountService(ExpensesContext context)
        {
            _context = context;
        }

        public void Create(Account model)
        {
            _context.Accounts.Add(model);
        }

        public Account Get(int id)
        {
            return _context.Accounts.Where(x => x.AccountID == id).SingleOrDefault();
        }

        public IEnumerable<Account> GetWhere(Expression<Func<Account, bool>> predicate)
        {
            return _context.Accounts.Where(predicate).ToList();
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }

        public void Update(Account model)
        {
            var target = _context.Accounts.Where(x => x.AccountID == model.AccountID).SingleOrDefault();

            if (target != null)
            {
                target.Provider = model.Provider;
                target.DateClosed = model.DateClosed;
                target.DateOpened = model.DateOpened;
                target.Balance = model.Balance;
            }
        }

        public void Remove(int id)
        {
            var target = _context.Accounts.Where(x => x.AccountID == id).SingleOrDefault();
            if (target != null)
            {
                _context.Accounts.Remove(target);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
