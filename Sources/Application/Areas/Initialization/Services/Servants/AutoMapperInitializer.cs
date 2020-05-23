using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Lamar;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services.Servants
{
    internal static class AutoMapperInitializer
    {
        internal static void InitializeAutoMapper(ServiceRegistry registry, IEnumerable<Assembly> assemblies)
        {
            var profileTypes = assemblies.SelectMany(f => f.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t))).ToList();

            var mapperConfiguration = new MapperConfiguration(
                cfg =>
                {
                    foreach (var profileType in profileTypes)
                    {
                        cfg.AddProfile(profileType);
                    }
                });

            var mapper = mapperConfiguration.CreateMapper();
            registry.For<IMapper>().Use(mapper);
        }
    }
}