using Client.Clients.Version1;
using Companies.Logic;
using Companies.Persistence;
using PipServices3.Commons.Refer;
using System.Threading.Tasks;
using Xunit;

namespace Companies.Clients
{
    public class CompaniesDirectClientV1Test
    {
        private CompaniesMemoryPersistence _persistence;
        private CompaniesController _controller;
        private CompaniesDirectClientV1 _client;
        private CompaniesClientV1Fixture _fixture;

        public CompaniesDirectClientV1Test()
        {
            _persistence = new CompaniesMemoryPersistence();
            _controller = new CompaniesController();
            _client = new CompaniesDirectClientV1();

            IReferences references = References.FromTuples(
                new Descriptor("CompaniesService", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("CompaniesService", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("CompaniesService", "client", "direct", "default", "1.0"), _client
            );

            _controller.SetReferences(references);

            _client.SetReferences(references);

            _fixture = new CompaniesClientV1Fixture(_client);

            _client.OpenAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }
    }
}
