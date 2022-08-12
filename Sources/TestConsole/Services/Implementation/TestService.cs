using System;

namespace Mmu.Mlh.ServiceProvisioning.TestConsole.Services.Implementation
{
    public class TestService : ITestService
    {
        public void DoSomething()
        {
            Console.WriteLine("Did something");
        }
    }
}