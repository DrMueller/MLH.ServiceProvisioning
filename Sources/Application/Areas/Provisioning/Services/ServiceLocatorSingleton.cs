﻿namespace Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services
{
    public static class ServiceLocatorSingleton
    {
        public static IServiceLocator Instance { get; private set; }

        public static void Initialize(IServiceLocator instance)
        {
            Instance = instance;
        }
    }
}