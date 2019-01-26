using ExpensesAPI.Data;
using ExpensesAPI.Models;
using ExpensesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Newtonsoft.Json;

namespace ExpensesAPI.Test.Integration
{
    public class Test_TransactionService : IDisposable
    {
        private ExpensesContext _context;
        private TransactionService _testService;
        private User _testUser;
        private List<Models.Transaction> _testTransactions;

        public Test_TransactionService()
        {
            _context = new ExpensesContext();
            _context.Database.CreateIfNotExists();

            _testService = new TransactionService(); 
            
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

            _testService = null;
            _testUser = null;
            _testTransactions = null;
        }

        [Fact]
        public void Create_PersistedRecordFieldsMatchTestUserProperties()
        {
            Transaction actualModel;
            var testModel = new Transaction
            {
                TransactionDate = "2021-01-31",
                Description = "candy",
                CreditAmount = 0,
                DebitAmount = 7.99
            };

            _testService.Create(testModel);
            _testService.Save();
            using (var context = new ExpensesContext())
            {
                actualModel = context.Transactions.Find(3);
            }

            var testModelJson = JsonConvert.SerializeObject(testModel);
            var actualModelJson = JsonConvert.SerializeObject(actualModel);

            Assert.Equal(testModelJson, actualModelJson);
        }

        [Fact]
        public void Update_UpdatedRecordFieldsMatchTestUserProperties()
        {
            Transaction actualModel;
            var updatedModel = _context.Transactions.ToList().FirstOrDefault();
            updatedModel.Description = "newspaper";

            _testService.Update(updatedModel);
            _testService.Save();
            using (var context = new ExpensesContext())
            {
                actualModel = context.Transactions.ToList().FirstOrDefault();
            }

            var testModelJson = JsonConvert.SerializeObject(updatedModel);
            var actualModelJson = JsonConvert.SerializeObject(actualModel);

            Assert.Equal(testModelJson, actualModelJson);
        }

        [Fact]
        public void Remove_RemovedRecordIsNull()
        {
            Transaction actualModel;
            var removedModel = _context.Transactions.ToList().FirstOrDefault();

            _testService.Remove(removedModel.TransactionID);
            _testService.Save();
            using (var context = new ExpensesContext())
            {
                actualModel = context.Transactions.Find(removedModel.TransactionID);
            }

            Assert.Null(actualModel);
        }

        [Fact]
        public void GetAll_CountMatches()
        {
            var count = _testService.GetAll().ToList().Count;

            Assert.Equal(2, count);
        }

        [Fact]
        public void GetById_ModelIsNotNull()
        {
            var model = _testService.Get(1);

            Assert.NotNull(model);
        }

        [Fact]
        public void GetWhere_CountMatches()
        {
            var count = _testService.GetWhere(x => x.Description == "drinks").ToList().Count;

            Assert.Equal(1, count);
        }
    }
}
