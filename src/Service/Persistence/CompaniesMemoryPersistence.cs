using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PipServices3.Commons.Data;
using PipServices3.Data.Persistence;
using MongoDB.Driver;
using Companies.Data.Version1;

namespace Companies.Persistence
{
    public class CompaniesMemoryPersistence: IdentifiableMemoryPersistence<CompanyV1, string>, ICompaniesPersistence
    {
        public CompaniesMemoryPersistence()
        {
            _maxPageSize = 1000;
        }

        public Task<CompanyV1> CreateCompanyAsync(string correlationId, CompanyV1 company)
        {
            return base.CreateAsync(correlationId, company);
        }

        public Task<CompanyV1> DeleteCompanyByIdAsync(string correlationId, string id)
        {
            return base.DeleteByIdAsync(correlationId, id);
        }

        public Task<DataPage<CompanyV1>> GetCompaniesAsync(string correlationId, FilterParams filter, PagingParams paging, SortParams sort)
        {
            return base.GetPageByFilterAsync(correlationId, ComposeFilter(filter), paging);
        }

        public Task<CompanyV1> GetCompanyByIdAsync(string correlationId, string id)
        {
            return base.GetOneByIdAsync(correlationId, id);
        }

        public Task<CompanyV1> UpdateCompanyAsync(string correlationId, CompanyV1 company)
        {
            return base.UpdateAsync(correlationId, company);
        }

        private List<Func<CompanyV1, bool>> ComposeFilter(FilterParams filterParams)
        {
            filterParams = filterParams ?? new FilterParams();

            var builder = Builders<CompanyV1>.Filter;
            var filter = builder.Empty;

            var id = filterParams.GetAsNullableString("id");
            var bank_code = filterParams.GetAsNullableString("bank_code");
            var acc_code = filterParams.GetAsNullableString("acc_code");
            var state_code = filterParams.GetAsNullableString("state_code");
            var iban = filterParams.GetAsNullableString("iban");
            var name = filterParams.GetAsNullableString("name");
            var contract_date = filterParams.GetAsNullableString("contract_date");
            var contract_no = filterParams.GetAsNullableString("contract_no");
            var employee_id = filterParams.GetAsNullableInteger("employee_id");

            return new List<Func<CompanyV1, bool>>
            {
                (item) =>
                {
                    if (!string.IsNullOrWhiteSpace(id) && item.Id != id) return false;
                    if (!string.IsNullOrWhiteSpace(bank_code) && item.BankCode != bank_code) return false;
                    if (!string.IsNullOrWhiteSpace(acc_code) && item.AccCode != acc_code) return false;
                    if (!string.IsNullOrWhiteSpace(state_code) && item.StateCode != state_code) return false;
                    if (!string.IsNullOrWhiteSpace(iban) && item.IBAN != iban) return false;
                    if (!string.IsNullOrWhiteSpace(name) && item.Name != name) return false;
                    if (!string.IsNullOrWhiteSpace(contract_no) && item.ContractNo != contract_no) return false;
                    if (employee_id.HasValue && item.EmployeeId != employee_id) return false;
                    if (!string.IsNullOrWhiteSpace(contract_date) && DateTime.TryParse(contract_date, out DateTime dt_contract_date) && item.ContractDate != dt_contract_date) return false;
                    return true;
                }
            };
                        
        }

    }
}
