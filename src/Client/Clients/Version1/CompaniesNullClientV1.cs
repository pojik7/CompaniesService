using Companies.Data.Version1;
using PipServices3.Commons.Data;
using System.Threading.Tasks;

namespace Client.Clients.Version1
{
    public class CompaniesNullClientV1 : ICompaniesClientV1
    {
        public async Task<CompanyV1> CreateCompanyAsync(string correlationId, CompanyV1 customer)
        {
            return await Task.FromResult(new CompanyV1());
        }

        public async Task<CompanyV1> DeleteCompanyByIdAsync(string correlationId, string id)
        {
            return await Task.FromResult(new CompanyV1());
        }

        public async Task<DataPage<CompanyV1>> GetCompaniesAsync(string correlationId, FilterParams filter, PagingParams paging, SortParams sort)
        {
            return await Task.FromResult(new DataPage<CompanyV1>());
        }

        public async Task<CompanyV1> GetCompanyByIdAsync(string correlationId, string id)
        {
            return await Task.FromResult(new CompanyV1());
        }

        public async Task<CompanyV1> UpdateCompanyAsync(string correlationId, CompanyV1 customer)
        {
            return await Task.FromResult(new CompanyV1());
        }
    }
}
