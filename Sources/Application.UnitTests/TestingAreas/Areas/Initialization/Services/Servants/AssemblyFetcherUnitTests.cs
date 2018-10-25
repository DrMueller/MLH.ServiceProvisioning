using System.Linq;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services.Servants;
using NUnit.Framework;

namespace Mmu.Mlh.ServiceProvisioning.UnitTests.TestingAreas.Areas.Initialization.Services.Servants
{
    [TestFixture]
    public class AssemblyFetcherUnitTests
    {
        [Test]
        public void FetchingAssemblies_FetchesDirectReferences()
        {
            // Arrange
            var testAssembly = typeof(AssemblyFetcherUnitTests).Assembly;
            var referencedAssembly = typeof(AssemblyFetcher).Assembly;
            var containerConfig = new ContainerConfiguration(testAssembly, "Mmu.Mlh");

            // Act
            var actualReferenceAssemblies = AssemblyFetcher.GetApplicationRelevantAssemblies(containerConfig);

            // Assert
            var actualAppExtensionsAssembly = actualReferenceAssemblies.FirstOrDefault(f => f == referencedAssembly);

            Assert.That(actualAppExtensionsAssembly, Is.Not.Null);
        }

        [Test]
        public void FetchingAssemblies_FetchesNugetPackageReferences()
        {
            // Arrange
            var testAssembly = typeof(AssemblyFetcherUnitTests).Assembly;
            var nugetPackageAssembly = typeof(Maybe<>).Assembly;
            var containerConfig = new ContainerConfiguration(testAssembly, "Mmu.Mlh");

            // Act
            var actualReferenceAssemblies = AssemblyFetcher.GetApplicationRelevantAssemblies(containerConfig);

            // Assert
            var actualExtensionsAssembly = actualReferenceAssemblies.FirstOrDefault(f => f == nugetPackageAssembly);

            Assert.That(actualExtensionsAssembly, Is.Not.Null);
        }
    }
}