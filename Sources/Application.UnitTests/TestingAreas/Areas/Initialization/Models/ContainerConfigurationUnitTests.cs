using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlh.ServiceProvisioning.UnitTests.TestingAreas.Areas.Initialization.Models
{
    [TestFixture]
    public class ContainerConfigurationUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            var rootAssembly = typeof(ContainerConfigurationUnitTests).Assembly;
            const string AssemblyPrefix = "Mmu.Mlh";
            const bool InitializeAutoMapper = true;

            ConstructorTestBuilderFactory.Constructing<ContainerConfiguration>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null, null).Fails()
                .WithArgumentValues(rootAssembly, null).Fails()
                .WithArgumentValues(null, AssemblyPrefix).Fails()
                .WithArgumentValues(rootAssembly, AssemblyPrefix, InitializeAutoMapper)
                .Maps()
                .ToProperty(f => f.AssemblyPrefix).WithValue(AssemblyPrefix)
                .ToProperty(f => f.InitializeAutoMapper).WithValue(InitializeAutoMapper)
                .ToProperty(f => f.RootAssembly).WithValue(rootAssembly)
                .BuildMaps()
                .Assert();
        }

        [Test]
        public void CreatingFromAssembly_WithThreePrefixParts_TakesThreePrefixParts()
        {
            // Arrange
            var assembly = typeof(ContainerConfigurationUnitTests).Assembly;

            // Act
            var actualAssemblyParameters = ContainerConfiguration.CreateFromAssembly(assembly, 3);

            // Assert
            Assert.AreEqual(actualAssemblyParameters.AssemblyPrefix, "Mmu.Mlh.ServiceProvisioning");
        }
    }
}