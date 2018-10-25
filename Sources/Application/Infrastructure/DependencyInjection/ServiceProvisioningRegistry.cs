using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services.Implementation;
using StructureMap;

namespace Mmu.Mlh.ServiceProvisioning.Infrastructure.DependencyInjection
{
    public class ServiceProvisioningRegistry : Registry
    {
        public ServiceProvisioningRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<ServiceProvisioningRegistry>();
                    scanner.WithDefaultConventions();
                });

            For<IServiceLocator>().Use<ServiceLocator>().Singleton();
        }
    }
}