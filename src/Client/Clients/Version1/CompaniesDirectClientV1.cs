using System.Threading.Tasks;
using Companies.Data.Version1;
using Companies.Logic;
using PipServices3.Commons.Data;
using PipServices3.Commons.Refer;
using PipServices3.Rpc.Clients;

namespace Client.Clients.Version1
{
    public class CompaniesDirectClientV1: DirectClient<ICompaniesController>, ICompaniesClientV1
    {
        public CompaniesDirectClientV1() : base()
        {
            _dependencyResolver.Put("controller", new Descriptor("CompaniesService", "controller", "*", "*", "1.0"));
        }

        public async Task<DataPage<CompanyV1>> GetCompaniesAsync(
            string correlationId, FilterParams filter, PagingParams paging, SortParams sort)
        {
            using (Instrument(correlationId, "companies.get_companies"))
            {
                return await _controller.GetCompaniesAsync(correlationId, filter, paging, sort);
            }
        }

        public async Task<CompanyV1> GetCompanyByIdAsync(string correlationId, string id)
        {
            using (Instrument(correlationId, "companies.get_company_by_id"))
            {
                return await _controller.GetCompanyByIdAsync(correlationId, id);
            }
        }

        public async Task<CompanyV1> CreateCompanyAsync(string correlationId, CompanyV1 company)
        {
            using (Instrument(correlationId, "companies.create_company"))
            {
                return await _controller.CreateCompanyAsync(correlationId, company);
            }
        }

        public async Task<CompanyV1> UpdateCompanyAsync(string correlationId, CompanyV1 company)
        {
            using (Instrument(correlationId, "companies.update_company"))
            {
                return await _controller.UpdateCompanyAsync(correlationId, company);
            }
        }

        public async Task<CompanyV1> DeleteCompanyByIdAsync(string correlationId, string id)
        {
            using (Instrument(correlationId, "companies.delete_company_by_id"))
            {
                return await _controller.DeleteCompanyByIdAsync(correlationId, id);
            }
        }
    }
}
