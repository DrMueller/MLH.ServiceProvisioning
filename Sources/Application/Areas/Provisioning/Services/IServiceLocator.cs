using System;
using System.Collections.Generic;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services
{
    public interface IServiceLocator
    {
        IReadOnlyCollection<T> GetAllServices<T>();

        T GetService<T>();

        object GetService(Type pluginType);
    }
}
