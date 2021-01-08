using Companies.Data.Version1;
using Companies.Persistence;
using PipServices3.Commons.Config;
using PipServices3.Commons.Data;
using PipServices3.Commons.Refer;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Companies.Logic
{
    class CompaniesControllerTest: IDisposable
    {
        private CompanyV1 Company1 = new CompanyV1()
        {
            AccCode = "2600000001",
            BankCode = "777777",
            ContractDate = DateTime.Now,
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
            ContractDate = DateTime.Now,
            ContractNo = "2",
            EmployeeId = 2,
            IBAN = "IBAN",
            Id = "2",
            Name = "Company2",
            StateCode = "132456780"
        };

        private CompaniesController _controller;
        private CompaniesMemoryPersistence _persistence;

        public CompaniesControllerTest()
        {
            _persistence = new CompaniesMemoryPersistence();
            _persistence.Configure(new ConfigParams());

            _controller = new CompaniesController();

            var references = References.FromTuples(
                new Descriptor("CompaniesService", "persistence", "memory", "*", "1.0"), _persistence,
                new Descriptor("CompaniesService", "controller", "default", "*", "1.0"), _controller
            );

            _controller.SetReferences(references);

            _persistence.OpenAsync(null).Wait();
        }

        public void Dispose()
        {
            _persistence.CloseAsync(null).Wait();
        }

        public async Task TestCrudOperationsAsync()
        {
            // Create the first customer
            var company = await _controller.CreateCompanyAsync(null, Company1);

            AssertCompanies(Company1, company);

            // Create the second customer
            company = await _controller.CreateCompanyAsync(null, Company2);

            AssertCompanies(Company2, company);

            // Get all customers
            var page = await _controller.GetCompaniesAsync(
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

            company = await _controller.UpdateCompanyAsync(null, Company1);

            Assert.NotNull(company1);
            Assert.Equal(company1.Id, company.Id);
            Assert.Equal("ABC", company.Name);

            // Delete the customer
            company = await _controller.DeleteCompanyByIdAsync(null, company1.Id);

            Assert.NotNull(company);
            Assert.Equal(company1.Id, company.Id);

            // Try to get deleted customer
            company = await _controller.GetCompanyByIdAsync(null, company1.Id);

            Assert.Null(company);

            // Clean up for the second test
            await _controller.DeleteCompanyByIdAsync(null, Company2.Id);
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
