using System.Collections.Generic;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;
using Mmu.Mlh.ServiceProvisioning.UnitTests.TestingInfrastructure.Services;
using Mmu.Mlh.ServiceProvisioning.UnitTests.TestingInfrastructure.TestDoubles;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlh.ServiceProvisioning.UnitTests.TestingAreas.Areas.Initialization.Services
{
    [TestFixture]
    public class ServiceProvisioningInitializerUnitTests
    {
        [Test]
        public void InitializingContainer_FromContainerConfig_CallsRegistries()
        {
            // Arrange
            var containerConfig = new ContainerConfiguration(
                typeof(ServiceProvisioningInitializerUnitTests).Assembly,
                "Mmu");

            // Act
            ServiceProvisioningInitializer.CreateContainer(containerConfig);

            // Assert
            Assert.That(MockRegistry.WasCalled, Is.True);
        }

        [Test]
        public void InitializingContainer_FromContainerConfig_CreatesContainer()
        {
            // Arrange
            var containerConfig = new ContainerConfiguration(
                typeof(ServiceProvisioningInitializerUnitTests).Assembly,
                "Mmu");

            // Act
            var actualContainer = ServiceProvisioningInitializer.CreateContainer(containerConfig);

            // Assert
            Assert.That(actualContainer, Is.Not.Null);
        }

        [Test]
        public void InitializingContainer_WithOverridenServiceDescriptors_UsesOverridenServiceDescriptors()
        {
            // Arrange
            var containerConfig = new ContainerConfiguration(
                typeof(ServiceProvisioningInitializerUnitTests).Assembly,
                "Mmu");

            var mockIndividualService = Mock.Of<IIndividualService>();
            var serviceDescriptors = new List<ServiceDescriptor>
            {
                new(typeof(IIndividualService), mockIndividualService)
            };

            // Act
            var actualContainer = ServiceProvisioningInitializer.CreateContainer(containerConfig, serviceDescriptors);
            var actualIndividualService = actualContainer.GetInstance<IIndividualService>();

            // Assert
            Assert.That(actualIndividualService, Is.Not.Null);
            Assert.That(actualIndividualService, Is.EqualTo(mockIndividualService));
        }

        [Test]
        public void PopulateRegistry_PopulatesRegistry()
        {
            // Arrange
            var containerConfig = new ContainerConfiguration(
                typeof(ServiceProvisioningInitializerUnitTests).Assembly,
                "Mmu");

            var registry = new ServiceRegistry();

            // Act
            ServiceProvisioningInitializer.PopulateRegistry(containerConfig, registry);

            // Assert
            Assert.That(registry, Is.Not.Empty);
        }
    }
}