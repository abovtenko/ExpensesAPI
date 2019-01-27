using ExpensesCoreAPI.Data;
using ExpensesCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpensesCoreAPI.Services
{
    public class UserService : IService<User>
    {
        private readonly ExpensesContext _context;

        public UserService(ExpensesContext context)
        {
            _context = context;
        }

        public void Create(User model)
        {
            _context.Users.Add(model);
        }

        public User Get(int id)
        {
            return _context.Users.Where(x => x.UserID == id).SingleOrDefault();
        }

        public IEnumerable<User> GetWhere(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Where(predicate).ToList();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Update(User model)
        {
            var target = _context.Users.Where(x => x.UserID == model.UserID).SingleOrDefault();

            if (target != null)
            {
                target.Username = model.Username;
            }
        }

        public void Remove(int id)
        {
            var target = _context.Users.Where(x => x.UserID == id).SingleOrDefault();
            if (target != null)
            {
                _context.Users.Remove(target);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}