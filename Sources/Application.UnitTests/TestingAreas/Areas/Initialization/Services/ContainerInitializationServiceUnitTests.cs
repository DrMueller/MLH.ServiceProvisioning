using Lamar;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;
using Mmu.Mlh.ServiceProvisioning.UnitTests.TestingInfrastructure.TestDoubles;
using NUnit.Framework;

namespace Mmu.Mlh.ServiceProvisioning.UnitTests.TestingAreas.Areas.Initialization.Services
{
    [TestFixture]
    public class ContainerInitializationServiceUnitTests
    {
        [Test]
        public void InitializingContainer_CallsRegistries()
        {
            // Arrange
            var containerConfig = new ContainerConfiguration(
                typeof(ContainerInitializationServiceUnitTests).Assembly,
                "Mmu");

            // Act
            ProvisioningInitializer.CreateContainer(containerConfig);

            // Assert
            Assert.That(MockRegistry.WasCalled, Is.True);
        }

        [Test]
        public void InitializingContainer_CreatesContainer()
        {
            // Arrange
            var containerConfig = new ContainerConfiguration(
                typeof(ContainerInitializationServiceUnitTests).Assembly,
                "Mmu");

            // Act
            var actualContainer = ProvisioningInitializer.CreateContainer(containerConfig);

            // Assert
            Assert.That(actualContainer, Is.Not.Null);
        }

        [Test]
        public void PopulateRegistry_PopulatesRegistry()
        {
            // Arrange
            var containerConfig = new ContainerConfiguration(
                typeof(ContainerInitializationServiceUnitTests).Assembly,
                "Mmu");

            var registry = new ServiceRegistry();

            // Act
            ProvisioningInitializer.PopulateRegistry(containerConfig, registry);

            // Assert
            Assert.That(registry, Is.Not.Empty);
        }
    }
}