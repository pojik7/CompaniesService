using Companies.Data.Version1;
using PipServices3.Commons.Data;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace Companies.Persistence
{
    public class CompaniesPersistenceFixture
    {
        private CompanyV1 Company1 = new CompanyV1()
        {
            AccCode = "2600000001",
            BankCode = "777777",
            ContractDate = DateTime.ParseExact("01/05/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            ContractNo = "1",
            EmployeeId = 1,
            IBAN = "IBAN",
            Id = "1",
            Name = "Company1",
            StateCode = "132456789"
        };

        private CompanyV1 Company2 = new CompanyV1()
        {
            AccCode = "2600000002",
            BankCode = "888888",
            ContractDate = DateTime.ParseExact("02/05/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            ContractNo = "2",
            EmployeeId = 2,
            IBAN = "IBAN",
            Id = "2",
            Name = "Company2",
            StateCode = "132456780"
        };

        private CompanyV1 Company3 = new CompanyV1()
        {
            AccCode = "2600000003",
            BankCode = "999999",
            ContractDate = DateTime.ParseExact("03/05/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            ContractNo = "3",
            EmployeeId = 3,
            IBAN = "IBAN2",
            Id = "3",
            Name = "Company3",
            StateCode = "132456700"
        };

        private ICompaniesPersistence _persistence;

        public CompaniesPersistenceFixture(ICompaniesPersistence persistence)
        {
            _persistence = persistence;
        }

        private async Task TestCreateCompaniesAsync()
        {
            // Create the first customer
            var customer = await _persistence.CreateCompanyAsync(null, Company1);

            AssertCompanies(Company1, customer);

            // Create the second customer
            customer = await _persistence.CreateCompanyAsync(null, Company2);

            AssertCompanies(Company2, customer);

            // Create the third customer
            customer = await _persistence.CreateCompanyAsync(null, Company3);

            AssertCompanies(Company3, customer);
        }

        public async Task TestCrudOperationsAsync()
        {
            // Create items
            await TestCreateCompaniesAsync();

            // Get all customers
            var page = await _persistence.GetCompaniesAsync(
                null,
                new FilterParams(),
                new PagingParams(),
                new SortParams()
            );

            Assert.NotNull(page);
            Assert.Equal(3, page.Data.Count);

            var company1 = page.Data[0];

            // Update the customer
            company1.Name = "ABC";

            var customer = await _persistence.UpdateCompanyAsync(null, company1);

            Assert.NotNull(customer);
            Assert.Equal(company1.Id, customer.Id);
            Assert.Equal("ABC", customer.Name);

            // Delete the customer
            customer = await _persistence.DeleteCompanyByIdAsync(null, company1.Id);

            Assert.NotNull(customer);
            Assert.Equal(company1.Id, customer.Id);

            // Try to get deleted customer
            customer = await _persistence.GetCompanyByIdAsync(null, company1.Id);

            Assert.Null(customer);
        }

        public async Task TestGetWithFiltersAsync()
        {
            // Create items
            await TestCreateCompaniesAsync();

            // Filter by id
            var page = await _persistence.GetCompaniesAsync(
                null,
                FilterParams.FromTuples(
                    "id", "1"
                ),
                new PagingParams(),
                new SortParams()
            );

            Assert.Single(page.Data);

            // Filter by state_code
            page = await _persistence.GetCompaniesAsync(
                null,
                FilterParams.FromTuples(
                    "state_code", "132456700"
                ),
                new PagingParams(),
                new SortParams()
            );

            Assert.Single(page.Data);

            // Filter by iban
            page = await _persistence.GetCompaniesAsync(
                null,
                FilterParams.FromTuples(
                    "iban", "IBAN"
                ),
                new PagingParams(),
                new SortParams()
            );

            Assert.Equal(2, page.Data.Count);

            
        }

        private static void AssertCompanies(CompanyV1 etalon, CompanyV1 company)
        {
            Assert.NotNull(company);
            Assert.Equal(etalon.Name, company.Name);
            Assert.Equal(etalon.AccCode, company.AccCode);
            Assert.Equal(etalon.BankCode, company.BankCode);
            Assert.Equal(etalon.ContractDate, company.ContractDate);
            Assert.Equal(etalon.ContractNo, company.ContractNo);
            Assert.Equal(etalon.IBAN, company.IBAN);
            Assert.Equal(etalon.Id, company.Id);
            Assert.Equal(etalon.StateCode, company.StateCode);
            Assert.Equal(etalon.EmployeeId, company.EmployeeId);
        }
    }
}
