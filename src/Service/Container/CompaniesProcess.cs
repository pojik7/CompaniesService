using Companies.Build;
using PipServices3.Container;
using PipServices3.Rpc.Build;

namespace Companies.Container
{
    public class CompaniesProcess : ProcessContainer
    {
		public CompaniesProcess()
			: base("companies", "Companies microservice")
		{
			_factories.Add(new DefaultRpcFactory());
			_factories.Add(new CompaniesServiceFactory());
		}
	}
}
