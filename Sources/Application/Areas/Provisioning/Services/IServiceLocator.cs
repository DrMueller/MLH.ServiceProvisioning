using System;
using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services
{
    public interface IServiceLocator
    {
        IReadOnlyCollection<T> GetAllServices<T>();

        T GetService<T>();

        object GetService(Type pluginType);

        Maybe<T> SearchService<T>();
    }
}
