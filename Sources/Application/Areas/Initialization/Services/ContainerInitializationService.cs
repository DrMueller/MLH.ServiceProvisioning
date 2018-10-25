using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services.Servants;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;
using StructureMap;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services
{
    public static class ContainerInitializationService
    {
        public static Container CreateInitializedContainer(ContainerConfiguration containerConfig)
        {
            var result = new Container();
            var assemblies = AssemblyFetcher.GetApplicationRelevantAssemblies(containerConfig);

            result.Configure(
                cfgExpression =>
                {
                    cfgExpression.Scan(
                        scanner =>
                        {
                            assemblies.ForEach(scanner.Assembly);
                            scanner.LookForRegistries();
                        });

                    if (containerConfig.InitializeAutoMapper)
                    {
                        AutoMapperInitializer.InitializeAutoMapper(cfgExpression, assemblies);
                    }
                });

            var serviceLocator = result.GetInstance<IServiceLocator>();
            ServiceLocatorSingleton.Initialize(serviceLocator);
            return result;
        }
    }
}