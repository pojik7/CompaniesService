using Companies.Data.Version1;
using Companies.Logic;
using Companies.Persistence;
using PipServices3.Commons.Config;
using PipServices3.Commons.Convert;
using PipServices3.Commons.Data;
using PipServices3.Commons.Refer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Companies.Services.Version1
{
    public class CompaniesHttpServiceV1Test
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

        private static readonly ConfigParams HttpConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "localhost",
            "connection.port", "3000"
        );

        private CompaniesMemoryPersistence _persistence;
        private CompaniesController _controller;
        private CompaniesHttpServiceV1 _service;

        public CompaniesHttpServiceV1Test()
        {
            _persistence = new CompaniesMemoryPersistence();
            _controller = new CompaniesController();
            _service = new CompaniesHttpServiceV1();

            IReferences references = References.FromTuples(
                new Descriptor("CompaniesService", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("CompaniesService", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("CompaniesService", "service", "http", "default", "1.0"), _service
            );

            _controller.SetReferences(references);

            _service.Configure(HttpConfig);
            _service.SetReferences(references);
            
            Task.Run(() => _service.OpenAsync(null));
            Thread.Sleep(5000);
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            // Create the first customer
            var company = await Invoke<CompanyV1>("create_company", new { company = Company1 });

            AssertCompanies(Company1, company);

            // Create the second customer
            company = await Invoke<CompanyV1>("create_company", new { company = Company2 });

            AssertCompanies(Company2, company);

            // Get all customers
            var page = await Invoke<DataPage<CompanyV1>>(
                "get_companies",
                new
                {
                    filter = new FilterParams(),
                    paging = new PagingParams()
                }
            );

            Assert.NotNull(page);
            Assert.Equal(2, page.Data.Count);

            var company1 = page.Data[0];

            // Update the customer
            company1.Name = "ABC";

            company = await Invoke<CompanyV1>("update_company", new { company = company1 });

            Assert.NotNull(company);
            Assert.Equal(company1.Id, company.Id);
            Assert.Equal("ABC", company.Name);

            // Delete the customer
            company = await Invoke<CompanyV1>("delete_company_by_id", new { company_id = company1.Id });

            Assert.NotNull(company);
            Assert.Equal(company1.Id, company.Id);

            // Try to get deleted customer
            company = await Invoke<CompanyV1>("get_company_by_id", new { company_id = company1.Id });

            Assert.Null(company);
        }

        private static async Task<T> Invoke<T>(string route, dynamic request)
        {
            using (var httpClient = new HttpClient())
            {
                var requestValue = JsonConverter.ToJson(request);
                using (var content = new StringContent(requestValue, Encoding.UTF8, "application/json"))
                {
                    var response = await httpClient.PostAsync("http://localhost:3000/v1/companies/" + route, content);
                    var responseValue = response.Content.ReadAsStringAsync().Result;
                    return JsonConverter.FromJson<T>(responseValue);
                }
            }
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
