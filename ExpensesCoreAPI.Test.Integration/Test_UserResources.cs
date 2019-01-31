using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace ExpensesCoreAPI.Test.Integration
{
    public class Test_UserResources : IClassFixture<WebApplicationFactory<ExpensesCoreAPI.Startup>>
    {
        private readonly WebApplicationFactory<ExpensesCoreAPI.Startup> _factory;

        public Test_UserResources(WebApplicationFactory<ExpensesCoreAPI.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]


        [InlineData("api/users")]
        public async Task Get_Users_ReturnsSuccessAndCorrectContent(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
