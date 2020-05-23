using System.Diagnostics;
using Lamar;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services.Servants;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services
{
    public static class ProvisioningInitializer
    {
        public static IContainer CreateContainer(ContainerConfiguration containerConfig)
        {
            var result = new Container(registry => PopulateRegistry(containerConfig, registry));

            if (containerConfig.LogInitialization)
            {
                Debug.WriteLine(result.WhatDidIScan());
                Debug.WriteLine(result.WhatDoIHave());
            }

            var serviceLocator = result.GetInstance<IServiceLocator>();
            ServiceLocatorSingleton.Initialize(serviceLocator);
            return result;
        }

        public static void PopulateRegistry(ContainerConfiguration containerConfig, ServiceRegistry registry)
        {
            var assemblies = AssemblyFetcher.GetApplicationRelevantAssemblies(containerConfig);

            registry.Scan(
                scanner =>
                {
                    assemblies.ForEach(scanner.Assembly);
                    scanner.LookForRegistries();
                });

            if (containerConfig.InitializeAutoMapper)
            {
                AutoMapperInitializer.InitializeAutoMapper(registry, assemblies);
            }
        }
    }
}