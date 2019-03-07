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
    public class Test_AccountEndpoints
    {
        private readonly CustomWebApplicationFactory<ExpensesCoreAPI.Startup> _factory;
        private readonly HttpClient _client;

        public Test_AccountEndpoints()
        {
            _factory = new CustomWebApplicationFactory<ExpensesCoreAPI.Startup>();
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData("api/accounts/", 1)]
        public async Task Get_Account_ReturnsCorrectResponseProperties(string url, int id)
        {
            var expectedResult = SeedData.GetTestAccounts()[id - 1];

            var response = await _client.GetAsync(url + id);

            var content = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Models.Account>(content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(actualResult.Balance, expectedResult.Balance);
            Assert.Equal(actualResult.Provider, expectedResult.Provider);
            Assert.Equal(actualResult.Type, expectedResult.Type);
            Assert.Equal(actualResult.DateClosed, expectedResult.DateClosed);
            Assert.Equal(actualResult.DateOpened, expectedResult.DateOpened);
        }

        [Theory]
        [InlineData("api/accounts/")]
        public async Task Get_AccountsAll_ReturnsCorrectResponseProperties(string url)
        {
            var expectedResult = SeedData.GetTestAccounts();

            var response = await _client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<List<Models.Account>>(content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(actualResult[0].Balance, expectedResult[0].Balance);
            Assert.Equal(actualResult[0].Provider, expectedResult[0].Provider);
            Assert.Equal(actualResult[0].Type, expectedResult[0].Type);
            Assert.Equal(actualResult[0].DateClosed, expectedResult[0].DateClosed);
            Assert.Equal(actualResult[0].DateOpened, expectedResult[0].DateOpened);
        }

        [Theory]
        [InlineData("api/accounts/")]
        public async Task Post_Account_ReturnsCorrectResponseProperties(string url)
        {
            var postData = SeedData.GetTestAccounts()[0];
            postData.Provider = "NewProvider";
            var content = new StringContent(JsonConvert.SerializeObject(postData));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync(url, content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("api/accounts/")]
        public async Task Put_Account_ReturnsCorrectResponseProperties(string url)
        {
            var postData = SeedData.GetTestAccounts()[0];
            postData.Balance = 0.00;
            var content = new StringContent(JsonConvert.SerializeObject(postData));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PutAsync(url, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("api/accounts/", 1)]
        public async Task Delete_Account_ReturnsCorrectResponseProperties(string url, int id)
        {
            var response = await _client.DeleteAsync(url + id);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
