using System;
using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services
{
    public interface IServiceLocator
    {
        IReadOnlyCollection<T> GetAllServices<T>();

        T GetService<T>();

        object GetService(Type pluginType);

        FunctionResult<T> TryToGetService<T>();
    }
}
