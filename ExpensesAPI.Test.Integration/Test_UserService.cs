using System;
using ExpensesCoreAPI.Services;
using ExpensesCoreAPI.Models;
using ExpensesCoreAPI.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExpensesCoreAPI.Test.Integration
{
    public class Test_UserService : IDisposable
    {
        private ExpensesContext _context;
        private UserService _userService;
        private User _testUser;
        private List<Models.Transaction> _testTransactions;

        public Test_UserService()
        {
            _context = new ExpensesContext();
            _context.Database.CreateIfNotExists();

            _userService = new UserService();
            
            _testTransactions = new List<Models.Transaction>
            {
                new Models.Transaction
                {
                    TransactionDate = "2020-01-01",
                    Description = "chips",
                    CreditAmount = 0,
                    DebitAmount = 3.99
                },
                new Models.Transaction
                {
                    TransactionDate = "2021-01-01",
                    Description = "drinks",
                    CreditAmount = 0,
                    DebitAmount = 14.56
                }
            };
            _testUser = new User();
            _testUser.Username = "UserOne";
            _testUser.UserTransactions = _testTransactions;
                       
            _context.Users.Add(_testUser);
            _context.SaveChanges();
        }

        
        public void Dispose()
        {            
            _context.Database.Delete();

            _userService = null;
            _testUser = null;
            _testTransactions = null;
        }

        [Fact]
        public void Create_PersistedRecordFieldsMatchTestUserProperties()
        {
            User userPersisted;
            var userTest = new User
            {
                Username = "UserTwo"
            };

            _userService.Create(userTest);
            _userService.Save();
            using (var context = new ExpensesContext())
            {
                userPersisted = context.Users.ToList()[1];
            }

            Assert.Equal(userTest.Username, userPersisted.Username);
        }

        [Fact]
        public void Update_UpdatedRecordFieldsMatchTestUserProperties()
        {
            User userPersisted;
            var userUpdated = _context.Users.ToList().FirstOrDefault();
            userUpdated.Username = "MyNewUsername";

            _userService.Update(userUpdated);
            _userService.Save();
            using (var context = new ExpensesContext())
            {
                userPersisted = context.Users.ToList().FirstOrDefault();
            }

            Assert.Equal(userUpdated.Username, userPersisted.Username);
        }

        [Fact]
        public void Remove_RemovedRecordIsNull()
        {
            User userPersisted;
            var userRemoved = _context.Users.ToList().FirstOrDefault();

            _userService.Remove(userRemoved.UserID);
            _userService.Save();
            using (var context = new ExpensesContext())
            {
                userPersisted = context.Users.ToList().FirstOrDefault();
            }

            Assert.Null(userPersisted);
        }

        [Fact]
        public void GetAll_CountMatches()
        {
            var userCount = _userService.GetAll().ToList().Count;

            Assert.Equal(1, userCount);
        }

        [Fact]
        public void GetById_CountMatches()
        {
            var user = _userService.Get(1);

            Assert.NotNull(user);
        }

        [Fact]
        public void GetWhere_CountMatches()
        {
            var userCount = _userService.GetWhere(x => x.Username == "UserOne").ToList().Count;

            Assert.Equal(1, userCount);
        }
    }
}
