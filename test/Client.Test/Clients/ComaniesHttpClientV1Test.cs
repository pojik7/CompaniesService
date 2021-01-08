using Client.Clients.Version1;
using Companies.Logic;
using Companies.Persistence;
using Companies.Services.Version1;
using PipServices3.Commons.Config;
using PipServices3.Commons.Refer;
using System.Threading.Tasks;
using Xunit;

namespace Companies.Clients
{
    public class ComaniesHttpClientV1Test
    {
        private static readonly ConfigParams HttpConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "localhost",
            "connection.port", 8080
        );

        private CompaniesMemoryPersistence _persistence;
        private CompaniesController _controller;
        private CompaniesHttpClientV1 _client;
        private CompaniesHttpServiceV1 _service;
        private CompaniesClientV1Fixture _fixture;

        public ComaniesHttpClientV1Test()
        {
            _persistence = new CompaniesMemoryPersistence();
            _controller = new CompaniesController();
            _client = new CompaniesHttpClientV1();
            _service = new CompaniesHttpServiceV1();

            IReferences references = References.FromTuples(
                new Descriptor("CompaniesService", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("CompaniesService", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("CompaniesService", "client", "http", "default", "1.0"), _client,
                new Descriptor("CompaniesService", "service", "http", "default", "1.0"), _service
            );

            _controller.SetReferences(references);

            _service.Configure(HttpConfig);
            _service.SetReferences(references);

            _client.Configure(HttpConfig);
            _client.SetReferences(references);

            _fixture = new CompaniesClientV1Fixture(_client);

            _service.OpenAsync(null).Wait();
            _client.OpenAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }
    }
}
