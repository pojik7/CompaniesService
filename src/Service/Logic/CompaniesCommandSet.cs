using Companies.Data.Version1;
using PipServices3.Commons.Commands;
using PipServices3.Commons.Convert;
using PipServices3.Commons.Data;
using PipServices3.Commons.Validate; 


namespace Companies.Logic
{
    public class CompaniesCommandSet : CommandSet
    {
        private ICompaniesController _controller;

        public CompaniesCommandSet(ICompaniesController controller)
        {
            _controller = controller;

            AddCommand(MakeGetCompaniesCommand());
            AddCommand(MakeGetCompanyByIdCommand());
            AddCommand(MakeCreateCompanyCommand());
            AddCommand(MakeUpdateCompanyCommand());
            AddCommand(MakeDeleteCompanyByIdCommand());
        }

        private ICommand MakeGetCompaniesCommand()
        {
            return new Command(
                "get_companies",
                new ObjectSchema()
                    .WithOptionalProperty("filter", new FilterParamsSchema())
                    .WithOptionalProperty("paging", new PagingParamsSchema())
                    .WithOptionalProperty("sort", new SortParamsSchema()),
                async (correlationId, parameters) =>
                {
                    var filter = FilterParams.FromValue(parameters.Get("filter"));
                    var paging = PagingParams.FromValue(parameters.Get("paging"));
                    var sort = SortParams.FromValue(parameters.Get("sort"));
                    return await _controller.GetCompaniesAsync(correlationId, filter, paging, sort);
                });
        }

        private ICommand MakeGetCompanyByIdCommand()
        {
            return new Command(
                "get_company_by_id",
                new ObjectSchema()
                    .WithRequiredProperty("company_id", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var id = parameters.GetAsString("company_id");
                    return await _controller.GetCompanyByIdAsync(correlationId, id);
                });
        }

        private ICommand MakeCreateCompanyCommand()
        {
            return new Command(
                "create_company",
                new ObjectSchema()
                    .WithRequiredProperty("company", new CompanyV1Schema()),
                async (correlationId, parameters) =>
                {
                    var company = ConvertToCompany(parameters.GetAsObject("company"));
                    return await _controller.CreateCompanyAsync(correlationId, company);
                });
        }

        private ICommand MakeUpdateCompanyCommand()
        {
            return new Command(
               "update_company",
               new ObjectSchema()
                    .WithRequiredProperty("company", new CompanyV1Schema()),
               async (correlationId, parameters) =>
               {
                   var enterprice = ConvertToCompany(parameters.GetAsObject("company"));
                   return await _controller.UpdateCompanyAsync(correlationId, enterprice);
               });
        }

        private ICommand MakeDeleteCompanyByIdCommand()
        {
            return new Command(
               "delete_company_by_id",
               new ObjectSchema()
                   .WithRequiredProperty("company_id", TypeCode.String),
               async (correlationId, parameters) =>
               {
                   var id = parameters.GetAsString("company_id");
                   return await _controller.DeleteCompanyByIdAsync(correlationId, id);
               });
        }

        private CompanyV1 ConvertToCompany(object value)
        {
            return JsonConverter.FromJson<CompanyV1>(JsonConverter.ToJson(value));
        }
    }
}
