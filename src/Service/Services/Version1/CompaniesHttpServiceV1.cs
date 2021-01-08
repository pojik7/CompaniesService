using PipServices3.Commons.Refer;
using PipServices3.Rpc.Services;

namespace Companies.Services.Version1
{
    public class CompaniesHttpServiceV1: CommandableHttpService
    {
        public CompaniesHttpServiceV1()
            : base("v1/companies")
        {
            _dependencyResolver.Put("controller", new Descriptor("CompaniesService", "controller", "default", "*", "1.0"));
        }
    }
}
