using System.Collections.Generic;
using Lamar;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes.Implementation;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services.Implementation;
using Mmu.Mlh.ServiceProvisioning.UnitTests.TestingAreas.Areas.Provisioning.Services.TestingServices;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlh.ServiceProvisioning.UnitTests.TestingAreas.Areas.Provisioning.Services
{
    [TestFixture]
    public class ServiceLocatorUnitTests
    {
        private Mock<IContainer> _containerMock;
        private ServiceLocator _sut;

        [SetUp]
        public void Align()
        {
            _containerMock = new Mock<IContainer>();
            _sut = new ServiceLocator(_containerMock.Object);
        }

        [Test]
        public void GettingAllServices_GetsAllServices()
        {
            // Arrange
            var testServices = new List<ITestService>
            {
                new TestService(),
                new TestService()
            };

            _containerMock.Setup(f => f.GetAllInstances<ITestService>()).Returns(testServices);

            // Act
            var actualServices = _sut.GetAllServices<ITestService>();

            // Assert
            CollectionAssert.AreEqual(testServices, actualServices);
        }

        [Test]
        public void GettingService_GetsService()
        {
            // Arrange
            var testService = new TestService();

            _containerMock.Setup(f => f.GetInstance<ITestService>()).Returns(testService);

            // Act
            var actualService = _sut.GetService<ITestService>();

            // Assert
            Assert.AreEqual(testService, actualService);
        }

        [Test]
        public void SearchingService_FindingService_ReturnsService()
        {
            // Arrange
            var testService = new TestService();

            _containerMock.Setup(f => f.TryGetInstance<ITestService>()).Returns(testService);

            // Act
            var actualServiceMaybe = _sut.SearchService<ITestService>();

            // Assert
            Assert.IsInstanceOf<Some<ITestService>>(actualServiceMaybe);

            actualServiceMaybe.Evaluate(
                actualService =>
                {
                    Assert.AreEqual(testService, actualService);
                });
        }

        [Test]
        public void SearchingService_NotFindingService_ReturnsNone()
        {
            // Arrange
            _containerMock.Setup(f => f.TryGetInstance<ITestService>()).Returns(() => null);

            // Act
            var actualServiceMaybe = _sut.SearchService<ITestService>();

            // Assert
            Assert.IsInstanceOf<None<ITestService>>(actualServiceMaybe);
        }
    }
}