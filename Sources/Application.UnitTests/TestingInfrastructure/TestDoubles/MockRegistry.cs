using StructureMap;

namespace Mmu.Mlh.ServiceProvisioning.UnitTests.TestingInfrastructure.TestDoubles
{
    public class MockRegistry : Registry
    {
        public static bool WasCalled { get; private set; }

        public MockRegistry()
        {
            WasCalled = true;
        }
    }
}