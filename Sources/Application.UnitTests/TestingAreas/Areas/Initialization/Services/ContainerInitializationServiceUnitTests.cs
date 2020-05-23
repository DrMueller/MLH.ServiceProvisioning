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
                "MMu");

            // Act
            ContainerInitializationService.CreateInitializedContainer(containerConfig);

            // Assert
            Assert.That(MockRegistry.WasCalled, Is.True);
        }

        [Test]
        public void InitializingContainer_CreatesContainer()
        {
            // Arrange
            var containerConfig = new ContainerConfiguration(
                typeof(ContainerInitializationServiceUnitTests).Assembly,
                "MMu");

            // Act
            var actualContainer = ContainerInitializationService.CreateInitializedContainer(containerConfig);

            // Assert
            Assert.That(actualContainer, Is.Not.Null);
        }
    }
}