using System.Collections.Generic;
using System.Diagnostics;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services.Servants;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services
{
    public static class ServiceProvisioningInitializer
    {
        // Integration Testing scenario
        public static IContainer CreateContainer(
            ContainerConfiguration containerConfig,
            IReadOnlyCollection<ServiceDescriptor> overrideDescriptors)
        {
            var registry = new ServiceRegistry();
            PopulateRegistry(containerConfig, registry);
            registry.AddRange(overrideDescriptors);
            var container = new Container(registry);

            LogAndInitializeSingleton(container);
            return container;
        }

        // Default scenario
        public static IContainer CreateContainer(ContainerConfiguration containerConfig)
        {
            var container = new Container(
                registry =>
                {
                    PopulateRegistry(containerConfig, registry);
                });

            LogAndInitializeSingleton(container);
            return container;
        }

        // ASP.Net Core DI scenario
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

        private static void LogAndInitializeSingleton(IContainer container)
        {
            Debug.WriteLine(container.WhatDidIScan());
            Debug.WriteLine(container.WhatDoIHave());

            var serviceLocator = container.GetInstance<IServiceLocator>();
            ServiceLocatorSingleton.Initialize(serviceLocator);
        }
    }
}