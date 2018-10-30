using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services.Servants
{
    internal static class AssemblyFetcher
    {
        internal static IReadOnlyCollection<Assembly> GetApplicationRelevantAssemblies(ContainerConfiguration containerConfig)
        {
            var assembliesByReferences = new List<Assembly>();

            AppendAssembliesByAssemblyReferences(
                containerConfig,
                containerConfig.RootAssembly,
                assembliesByReferences);
            LogAssemblies(containerConfig, "Assemblies by References", assembliesByReferences);

            var assembliesInRootPath = new List<Assembly>();
            AppendAssembliesInBaseDirectory(
                containerConfig,
                assembliesInRootPath);
            LogAssemblies(containerConfig, "Assemblies in Root Path", assembliesInRootPath);

            var result = assembliesByReferences.Union(assembliesInRootPath).ToList();
            return result;
        }

        private static void AppendAssembliesByAssemblyReferences(ContainerConfiguration containerConfig, Assembly currentAssembly, ICollection<Assembly> assemblies)
        {
            var relevantAssemblies =
                currentAssembly
                    .GetReferencedAssemblies()
                    .Where(assemblyName => containerConfig.CheckIfIsRelevant(assemblyName.FullName));

            foreach (var assemblyName in relevantAssemblies)
            {
                var loadedAssembly = Assembly.Load(assemblyName);
                assemblies.Add(loadedAssembly);
                AppendAssembliesByAssemblyReferences(containerConfig, loadedAssembly, assemblies);
            }
        }

        private static void AppendAssembliesInBaseDirectory(ContainerConfiguration containerConfig, List<Assembly> assemblies)
        {
            var assemblyExtensions = new[]
            {
                ".EXE",
                ".DLL"
            };

            var codeDirectory = Path.GetDirectoryName(containerConfig.RootAssembly.Location);
            var relevantAssemblyFileInfos = Directory.GetFiles(codeDirectory)
                .Select(str => new FileInfo(str))
                .Where(fi => assemblyExtensions.Contains(fi.Extension.ToUpperInvariant()))
                .Where(fi => containerConfig.CheckIfIsRelevant(fi.Name));

            foreach (var fi in relevantAssemblyFileInfos)
            {
                try
                {
                    assemblies.Add(Assembly.LoadFrom(fi.FullName));
                }
                catch (Exception)
                {
                    // Best effort, do nothing here
                }
            }
        }

        private static void LogAssemblies(ContainerConfiguration containerConfig, string source, IEnumerable<Assembly> assemblies)
        {
            if (!containerConfig.LogInitialization)
            {
                return;
            }

            var assemblyNames = assemblies.Select(assembly => assembly.FullName);
            var assemblyList = string.Join(Environment.NewLine, assemblyNames);
            Debug.WriteLine(source + ": " + Environment.NewLine + assemblyList);
        }
    }
}