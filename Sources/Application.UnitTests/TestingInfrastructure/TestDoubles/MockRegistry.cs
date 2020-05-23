using Lamar;

namespace Mmu.Mlh.ServiceProvisioning.UnitTests.TestingInfrastructure.TestDoubles
{
    public class MockRegistry : ServiceRegistry
    {
        public static bool WasCalled { get; private set; }

        public MockRegistry()
        {
            WasCalled = true;
        }
    }
}