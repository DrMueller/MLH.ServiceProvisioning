using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services.Implementation
{
    internal class ServiceLocator : IServiceLocator
    {
        private readonly IContainer _container;

        public ServiceLocator(IContainer container)
        {
            _container = container;
        }

        public IReadOnlyCollection<T> GetAllServices<T>()
        {
            var result = _container.GetAllInstances<T>().ToList();
            return result;
        }

        public T GetService<T>()
        {
            var result = _container.GetInstance<T>();
            return result;
        }

        public object GetService(Type pluginType)
        {
            var result = _container.GetInstance(pluginType);
            return result;
        }
    }
}