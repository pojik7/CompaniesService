using Client.Clients.Version1;
using Companies.Data.Version1;
using PipServices3.Commons.Data;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace Companies.Clients
{
    public class CompaniesClientV1Fixture
    {
        private CompanyV1 Company1 = new CompanyV1()
        {
            AccCode = "2600000001",
            BankCode = "777777",
            ContractDate = DateTime.ParseExact("01/05/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            ContractNo = "1",
            EmployeeId = 1,
            IBAN = "IBAN",
            Id="1",
            Name = "Company1",
            StateCode="132456789"
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

        private ICompaniesClientV1 _company;

        public CompaniesClientV1Fixture(ICompaniesClientV1 company)
        {
            _company = company;
        }

        public async Task TestCrudOperationsAsync()
        {
            // Create the first customer
            var company = await _company.CreateCompanyAsync(null, Company1);

            AssertCompanies(Company1, company);

            // Create the second customer
            company = await _company.CreateCompanyAsync(null, Company2);

            AssertCompanies(Company2, company);

            // Get all customers
            var page = await _company.GetCompaniesAsync(
                null,
                new FilterParams(),
                new PagingParams(),
                new SortParams()
            );

            Assert.NotNull(page);
            Assert.Equal(2, page.Data.Count);

            var company1 = page.Data[0];

            // Update the customer
            company1.Name = "ABC";

            company = await _company.UpdateCompanyAsync(null, company1);

            Assert.NotNull(company1);
            Assert.Equal(company1.Id, company.Id);
            Assert.Equal("ABC", company.Name);

            // Delete the customer
            company = await _company.DeleteCompanyByIdAsync(null, company1.Id);

            Assert.NotNull(company);
            Assert.Equal(company1.Id, company.Id);

            // Try to get deleted customer
            company = await _company.GetCompanyByIdAsync(null, company1.Id);

            Assert.Null(company);

            // Clean up for the second test
            await _company.DeleteCompanyByIdAsync(null, Company2.Id);
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
