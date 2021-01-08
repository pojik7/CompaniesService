using System.Threading.Tasks;
using Companies.Data.Version1;
using PipServices3.Commons.Data;
using PipServices3.Rpc.Clients;

namespace Client.Clients.Version1
{
    public class CompaniesHttpClientV1 : CommandableHttpClient, ICompaniesClientV1
    {
        public CompaniesHttpClientV1()
            : base("v1/companies")
        { }
        public async Task<CompanyV1> CreateCompanyAsync(string correlationId, CompanyV1 company)
        {
            return await CallCommandAsync<CompanyV1>(
                "create_company",
                correlationId,
                new
                {
                    company = company
                }
            );
        }

        public async Task<CompanyV1> DeleteCompanyByIdAsync(string correlationId, string id)
        {
            return await CallCommandAsync<CompanyV1>(
               "delete_company_by_id",
               correlationId,
               new
               {
                   company_id = id
               }
           );
        }

        public async Task<DataPage<CompanyV1>> GetCompaniesAsync(string correlationId, FilterParams filter, PagingParams paging, SortParams sort)
        {
            return await CallCommandAsync<DataPage<CompanyV1>>(
                "get_companies",
                correlationId,
                new
                {
                    filter = filter,
                    paging = paging,
                    sort = sort
                }
            );
        }

        public async Task<CompanyV1> GetCompanyByIdAsync(string correlationId, string id)
        {
            return await CallCommandAsync<CompanyV1>(
               "get_company_by_id",
               correlationId,
               new
               {
                   company_id = id
               }
            );
        }

        public async Task<CompanyV1> UpdateCompanyAsync(string correlationId, CompanyV1 company)
        {
            return await CallCommandAsync<CompanyV1>(
               "update_company",
               correlationId,
               new
               {
                   company = company
               }
            );
        }
    }
}
