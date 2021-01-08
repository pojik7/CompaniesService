using System.Threading.Tasks;
using Companies.Data.Version1;
using Companies.Persistence;
using PipServices3.Commons.Commands;
using PipServices3.Commons.Config;
using PipServices3.Commons.Data;
using PipServices3.Commons.Refer;

namespace Companies.Logic
{
    public class CompaniesController: ICompaniesController, IConfigurable, ICommandable
    {
        private ICompaniesPersistence _persistence;
        private CompaniesCommandSet _commandSet;

        public void Configure(ConfigParams config)
        {
        }
        public CommandSet GetCommandSet()
        {
            if (_commandSet == null)
                _commandSet = new CompaniesCommandSet(this);

            return _commandSet;
        }

        public void SetReferences(IReferences references)
        {
            _persistence = references.GetOneRequired<ICompaniesPersistence>(
                new Descriptor("CompaniesService", "persistence", "*", "*", "1.0")
            );
        }



        public async Task<CompanyV1> CreateCompanyAsync(string correlationId, CompanyV1 company)
        {
            company.Id = company.Id ?? IdGenerator.NextLong();

            return await _persistence.CreateCompanyAsync(correlationId, company);
        }

        public async Task<CompanyV1> DeleteCompanyByIdAsync(string correlationId, string id)
        {
            return await _persistence.DeleteCompanyByIdAsync(correlationId, id);
        }

        public async Task<CompanyV1> GetCompanyByIdAsync(string correlationId, string id)
        {
            return await _persistence.GetCompanyByIdAsync(correlationId, id);
        }

        public async Task<DataPage<CompanyV1>> GetCompaniesAsync(string correlationId, FilterParams filter, PagingParams paging, SortParams sort)
        {
            return await _persistence.GetCompaniesAsync(correlationId, filter, paging, sort);
        }

        public async Task<CompanyV1> UpdateCompanyAsync(string correlationId, CompanyV1 company)
        {
            return await _persistence.UpdateCompanyAsync(correlationId, company);
        }
    }
}
