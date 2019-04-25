using System.Net.Http;
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
    }
}