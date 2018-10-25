using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models
{
    public class ContainerConfiguration
    {
        private readonly bool _logInitialization;
        public string AssemblyPrefix { get; }
        public bool InitializeAutoMapper { get; }
        public Assembly RootAssembly { get; }

        public ContainerConfiguration(
            Assembly rootAssembly,
            string assemblyPrefix,
            bool initializeAutoMapper = false,
            bool logInitialization = false)
        {
            _logInitialization = logInitialization;
            Guard.ObjectNotNull(() => rootAssembly);
            Guard.ObjectNotNull(() => assemblyPrefix);

            RootAssembly = rootAssembly;
            AssemblyPrefix = assemblyPrefix;
            InitializeAutoMapper = initializeAutoMapper;
        }

        public static ContainerConfiguration CreateFromAssembly(
            Assembly assembly,
            int namespaceParts = 2,
            bool initializeAutoMapper = false,
            bool logInitialization = false)
        {
            var prefixParts = assembly
                .FullName
                .Split('.')
                .Take(namespaceParts);

            var assemblyPrefix = string.Join(".", prefixParts);

            var result = new ContainerConfiguration(assembly, assemblyPrefix, initializeAutoMapper, logInitialization);
            return result;
        }

        internal bool CheckIfIsRelevant(string assemblyNameSpace)
        {
            if (string.IsNullOrEmpty(assemblyNameSpace))
            {
                return false;
            }

            var relevantPrefixes = new[]
            {
                AssemblyPrefix,
                "Mmu"
            };

            return relevantPrefixes.Any(relevantPrefix => assemblyNameSpace.StartsWith(relevantPrefix, StringComparison.OrdinalIgnoreCase));
        }

        internal void WriteDebug(string message)
        {
            if (_logInitialization)
            {
                Debug.WriteLine(message);
            }
        }
    }
}