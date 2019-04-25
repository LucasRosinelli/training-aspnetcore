using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

namespace AspNetCoreTraining.IntegrationTests
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _server;

        public HttpClient Client { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((context, config) =>
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(),
                        "..\\..\\..\\..\\..\\src\\AspNetCoreTraining");
                    config.SetBasePath(path);

                    config.AddJsonFile("appsettings.json");
                });

            this._server = new TestServer(builder);

            this.Client = this._server.CreateClient();
            this.Client.BaseAddress = new Uri("http://localhost:8888/");
        }

        public void Dispose()
        {
            this.Client.Dispose();
            this._server.Dispose();
        }
    }
}