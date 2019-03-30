using ExpensesCoreAPI.Data;
using ExpensesCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpensesCoreAPI.Services
{
    public class UserService : IService<AppUser>
    {
        private readonly ExpensesContext _context;

        public UserService(ExpensesContext context)
        {
            _context = context;
        }

        public void Create(AppUser model)
        {
            _context.Users.Add(model);
        }

        public AppUser Get(int id)
        {
            return _context.AppUsers.Where(x => x.UserID == id).SingleOrDefault();
        }

        public IEnumerable<AppUser> GetWhere(Expression<Func<AppUser, bool>> predicate)
        {
            return _context.AppUsers.Where(predicate).ToList();
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _context.AppUsers.ToList();
        }

        public void Update(AppUser model)
        {
            var target = _context.AppUsers.Where(x => x.UserID == model.UserID).SingleOrDefault();

            if (target != null)
            {
                target.UserName = model.UserName;
            }
        }

        public void Remove(int id)
        {
            var target = _context.AppUsers.Where(x => x.UserID == id).SingleOrDefault();
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