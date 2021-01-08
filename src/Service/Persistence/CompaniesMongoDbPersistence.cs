using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using PipServices3.Commons.Data;
using PipServices3.Commons.Data.Mapper;
using PipServices3.MongoDb.Persistence;
using System.Threading.Tasks;
using Companies.Persistence.Mongo;
using Companies.Data.Version1;

namespace Companies.Persistence
{
    public class CompaniesMongoDbPersistence: IdentifiableMongoDbPersistence<CompanyMongoDbSchema, string>, ICompaniesPersistence
    {
        public CompaniesMongoDbPersistence()
            : base("companies")
        {
        }
        private new FilterDefinition<CompanyMongoDbSchema> ComposeFilter(FilterParams filterParams)
        {
            filterParams = filterParams ?? new FilterParams();

            var builder = Builders<CompanyMongoDbSchema>.Filter;
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

            if (!string.IsNullOrWhiteSpace(id)) filter &= builder.Eq(b => b.Id, id);
            if (!string.IsNullOrWhiteSpace(bank_code)) filter &= builder.Eq(b => b.BankCode, bank_code);
            if (!string.IsNullOrWhiteSpace(acc_code)) filter &= builder.Eq(b => b.AccCode, acc_code);
            if (!string.IsNullOrWhiteSpace(state_code)) filter &= builder.Eq(b => b.StateCode, state_code);
            if (!string.IsNullOrWhiteSpace(iban)) filter &= builder.Eq(b => b.IBAN, iban);
            if (!string.IsNullOrWhiteSpace(name)) filter &= builder.Eq(b => b.Name, name);
            if (!string.IsNullOrWhiteSpace(contract_no)) filter &= builder.Eq(b => b.ContractNo, contract_no);
            if (employee_id.HasValue) filter &= builder.Eq(b => b.EmployeeId, employee_id);

            if (!string.IsNullOrWhiteSpace(contract_date) && DateTime.TryParse(contract_date, out DateTime dt_contract_date))
                filter &= builder.Eq(b => b.ContractDate, dt_contract_date);

            return filter;
        }

        public async Task<CompanyV1> CreateCompanyAsync(string correlationId, CompanyV1 company)
        {
            var result = await CreateAsync(correlationId, FromPublic(company));

            return ToPublic(result);
        }

        public async Task<CompanyV1> DeleteCompanyByIdAsync(string correlationId, string id)
        {
            var result = await base.DeleteByIdAsync(correlationId, id);

            return ToPublic(result);
        }

        public async Task<CompanyV1> GetCompanyByIdAsync(string correlationId, string id)
        {
            var result = await GetOneByIdAsync(correlationId, id);

            return ToPublic(result);
        }

        public async Task<DataPage<CompanyV1>> GetCompanyAsync(string correlationId, FilterParams filter, PagingParams paging, SortParams sort)
        {
            var result = await base.GetPageByFilterAsync(correlationId, ComposeFilter(filter), paging);
            var data = result.Data.ConvertAll(ToPublic);

            return new DataPage<CompanyV1>
            {
                Data = data,
                Total = result.Total
            };
        }

        public async Task<CompanyV1> UpdateCompanyAsync(string correlationId, CompanyV1 company)
        {
            var result = await UpdateAsync(correlationId, FromPublic(company));

            return ToPublic(result);
        }

        private static CompanyV1 ToPublic(CompanyMongoDbSchema value)
        {
            return value == null ? null : ObjectMapper.MapTo<CompanyV1>(value);
        }

        private static CompanyMongoDbSchema FromPublic(CompanyV1 company)
        {
            return ObjectMapper.MapTo<CompanyMongoDbSchema>(company);
        }

        

        public async Task<DataPage<CompanyV1>> GetCompaniesAsync(string correlationId, FilterParams filter, PagingParams paging, SortParams sort)
        {
            var result = await base.GetPageByFilterAsync(correlationId, ComposeFilter(filter), paging);
            var data = result.Data.ConvertAll(ToPublic);

            return new DataPage<CompanyV1>
            {
                Data = data,
                Total = result.Total
            };
        }
    }
}
