using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpensesCoreAPI.Test.Integration
{
    public class Test_TransactionEndpoints : CustomWebApplicationFactory<ExpensesCoreAPI.Startup>
    {
        private readonly CustomWebApplicationFactory<ExpensesCoreAPI.Startup> _factory;
        private readonly HttpClient _client;

        public Test_TransactionEndpoints()
        {
            _factory = new CustomWebApplicationFactory<ExpensesCoreAPI.Startup>();
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData("api/transactions/", 1)]
        public async Task Get_Transaction_ReturnsCorrectResponseProperties(string url, int userId)
        {
            var expectedResult = Utilities.GetTestTransactions()[userId - 1];

            var response = await _client.GetAsync(url + userId);

            var content = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Models.Transaction>(content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(actualResult.TransactionDate, expectedResult.TransactionDate);
            Assert.Equal(actualResult.Description, expectedResult.Description);
            Assert.Equal(actualResult.CreditAmount, expectedResult.CreditAmount);
            Assert.Equal(actualResult.DebitAmount, expectedResult.DebitAmount);
        }

        [Theory]
        [InlineData("api/transactions/")]
        public async Task Get_TransactionsAll_ReturnsCorrectResponseProperties(string url)
        {
            var expectedResult = Utilities.GetTestTransactions();

            var response = await _client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<List<Models.Transaction>>(content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult[0].TransactionDate, actualResult[0].TransactionDate);
            Assert.Equal(expectedResult[0].Description, actualResult[0].Description);
            Assert.Equal(expectedResult[0].DebitAmount, actualResult[0].DebitAmount);
            Assert.Equal(expectedResult[0].CreditAmount, actualResult[0].CreditAmount);
        }
   
        [Theory]
        [InlineData("api/transactions/")]
        public async Task Post_Transaction_ReturnsCorrectResponseProperties(string url)
        {
            var postData = Utilities.GetTestTransactions()[0];
            postData.CreditAmount = 100.00;
            var content = new StringContent(JsonConvert.SerializeObject(postData));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync(url, content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("api/transactions/")]
        public async Task Put_Transaction_ReturnsCorrectResponseProperties(string url)
        {
            var postData = Utilities.GetTestTransactions()[0];
            postData.CreditAmount = 100.00;
            var content = new StringContent(JsonConvert.SerializeObject(postData));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PutAsync(url, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("api/transactions/", 1)]
        public async Task Delete_Transaction_ReturnsCorrectResponseProperties(string url, int userId)
        {
            var response = await _client.DeleteAsync(url + userId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
