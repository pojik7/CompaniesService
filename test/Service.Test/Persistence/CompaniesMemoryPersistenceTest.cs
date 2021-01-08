using PipServices3.Commons.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Companies.Persistence
{
    public class CompaniesMemoryPersistenceTest : IDisposable
    {
        public CompaniesMemoryPersistence _persistence;
        public CompaniesPersistenceFixture _fixture;

        public CompaniesMemoryPersistenceTest()
        {
            _persistence = new CompaniesMemoryPersistence();
            _persistence.Configure(new ConfigParams());

            _fixture = new CompaniesPersistenceFixture(_persistence);

            _persistence.OpenAsync(null).Wait();
        }

        public void Dispose()
        {
            _persistence.CloseAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }

        [Fact]
        public async Task TestGetWithFiltersAsync()
        {
            await _fixture.TestGetWithFiltersAsync();
        }
    }
}
