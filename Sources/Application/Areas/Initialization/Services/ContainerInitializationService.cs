using System.Diagnostics;
using Lamar;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services.Servants;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services
{
    public static class ContainerInitializationService
    {
        public static IContainer CreateInitializedContainer(ContainerConfiguration containerConfig)
        {
            var assemblies = AssemblyFetcher.GetApplicationRelevantAssemblies(containerConfig);

            var result = new Container(
                registry =>
                {
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
                });

            if (containerConfig.LogInitialization)
            {
                Debug.WriteLine(result.WhatDidIScan());
                Debug.WriteLine(result.WhatDoIHave());
            }

            var serviceLocator = result.GetInstance<IServiceLocator>();
            ServiceLocatorSingleton.Initialize(serviceLocator);
            return result;
        }
    }
}