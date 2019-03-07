using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace ExpensesCoreAPI.Test.Integration
{
    public class Test_UserEndpoints : CustomWebApplicationFactory<ExpensesCoreAPI.Startup>
    {
        private readonly CustomWebApplicationFactory<ExpensesCoreAPI.Startup> _factory;
        private readonly HttpClient _client;

        public Test_UserEndpoints()
        {
            _factory = new CustomWebApplicationFactory<ExpensesCoreAPI.Startup>();
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData("api/users/", 1)]
        [InlineData("api/users/", 2)]
        public async Task Get_User_ReturnsCorrectResponseProperties(string url, int userId)
        {
            var expectedResult = SeedData.GetTestUsers()[userId - 1];

            var response = await _client.GetAsync(url + userId);

            var content = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Models.User>(content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(actualResult.Username, expectedResult.Username);
        }

        [Theory]
        [InlineData("api/users/")]
        public async Task Get_UsersAll_ReturnsCorrectResponseProperties(string url)
        {
            var expectedResult = SeedData.GetTestUsers();

            var response = await _client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<List<Models.User>>(content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Contains(expectedResult, x => x.Username == expectedResult[0].Username);
            Assert.Contains(expectedResult, x => x.Username == expectedResult[1].Username);
        }

        [Theory]
        [InlineData("api/users/")]
        public async Task Post_User_ReturnsCorrectResponseProperties(string url)
        {
            var postData = SeedData.GetTestUsers()[0];
            postData.Username = "PostedUser";
            var content = new StringContent(JsonConvert.SerializeObject(postData));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync(url, content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("api/users/")]
        public async Task Put_User_ReturnsCorrectResponseProperties(string url)
        {
            var postData = SeedData.GetTestUsers()[0];
            postData.Username = "UpdatedUser";
            var content = new StringContent(JsonConvert.SerializeObject(postData));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PutAsync(url, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("api/users/", 1)]
        [InlineData("api/users/", 2)]
        public async Task Delete_User_ReturnsCorrectResponseProperties(string url, int userId)
        {
            var response = await _client.DeleteAsync(url + userId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
