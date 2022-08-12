using System;
using JetBrains.Annotations;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;
using Mmu.Mlh.ServiceProvisioning.TestConsole.Services;

namespace Mmu.Mlh.ServiceProvisioning.TestConsole
{
    [PublicAPI]
    internal class Program
    {
        public static void Main()
        {
            var containerConfig = new ContainerConfiguration(
                typeof(Program).Assembly,
                "Mmu.Mlh",
                true);

            var container = ServiceProvisioningInitializer.CreateContainer(containerConfig);
            var testservice = container.GetInstance<ITestService>();
            testservice.DoSomething();

            Console.ReadKey();
        }
    }
}
