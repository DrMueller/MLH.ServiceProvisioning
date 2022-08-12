using Lamar;

namespace Mmu.Mlh.ServiceProvisioning.TestConsole.Infrastructure.DependencyInjection
{
    public class TestConsoleServiceRegistry : ServiceRegistry
    {
        public TestConsoleServiceRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<TestConsoleServiceRegistry>();
                    scanner.WithDefaultConventions();
                });
        }
    }
}