using Lamar;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services.Implementation;

namespace Mmu.Mlh.ServiceProvisioning.Infrastructure.DependencyInjection
{
    public class ServiceProvisioningRegistry : ServiceRegistry
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