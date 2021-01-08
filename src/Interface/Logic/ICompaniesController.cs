using System.Threading.Tasks;
using Companies.Data.Version1;
using PipServices3.Commons.Data;


namespace Companies.Logic
{
    public interface ICompaniesController
    {
        Task<DataPage<CompanyV1>> GetCompaniesAsync(string correlationId, FilterParams filter, PagingParams paging, SortParams sort);
        Task<CompanyV1> GetCompanyByIdAsync(string correlationId, string id);
        Task<CompanyV1> CreateCompanyAsync(string correlationId, CompanyV1 enterprise);
        Task<CompanyV1> UpdateCompanyAsync(string correlationId, CompanyV1 enterprise);
        Task<CompanyV1> DeleteCompanyByIdAsync(string correlationId, string id);
    }
}
