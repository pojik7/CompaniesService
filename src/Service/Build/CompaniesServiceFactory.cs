using Companies.Logic;
using Companies.Persistence;
using Companies.Services.Version1;
using PipServices3.Commons.Refer;
using PipServices3.Components.Build;

namespace Companies.Build
{
    public class CompaniesServiceFactory : Factory
    {        
        public static Descriptor MongoDbPersistenceDescriptor = new Descriptor("CompaniesService", "persistence", "mongodb", "*", "1.0");
        public static Descriptor ControllerDescriptor = new Descriptor("CompaniesService", "controller", "default", "*", "1.0");
        public static Descriptor HttpServiceDescriptor = new Descriptor("CompaniesService", "service", "http", "*", "1.0");
        public static Descriptor MemoryPersistenceDescriptor = new Descriptor("CompaniesService", "persistence", "memory", "*", "1.0");

        public CompaniesServiceFactory()
        {
            RegisterAsType(MemoryPersistenceDescriptor, typeof(CompaniesMemoryPersistence));
            RegisterAsType(MongoDbPersistenceDescriptor, typeof(CompaniesMongoDbPersistence));
            RegisterAsType(ControllerDescriptor, typeof(CompaniesController));
            RegisterAsType(HttpServiceDescriptor, typeof(CompaniesHttpServiceV1));
        }
    }
}
