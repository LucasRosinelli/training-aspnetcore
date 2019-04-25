using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreTraining.IntegrationTests
{
    public class ToDoRouteShould : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public ToDoRouteShould(TestFixture fixture)
        {
            this._client = fixture.Client;
        }

        [Fact]
        public async Task ChallengeAnonymousUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/ToDo");

            var response = await this._client.SendAsync(request);

            // Assert: user redirected to login page
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("http://localhost:8888/Identity/Account/Login?ReturnUrl=%2FToDo".ToLowerInvariant(), response.Headers.Location.ToString().ToLowerInvariant());
        }
    }
}