using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities
{
    public static class Reflection
    {
        public static IEnumerable<MethodInfo> GetMethodsWithAttribute<TAttribute>() where TAttribute : Attribute
        {
            return Assembly.GetExecutingAssembly().GetTypes().SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes().OfType<TAttribute>().Any());
        }

        public static IEnumerable<MethodInfo> GetMethodsWithAttribute<TAssembly, TAttribute>()
        {
            return GetMethodsWithAttribute<TAttribute>(typeof(TAssembly));
        }

        public static IEnumerable<MethodInfo> GetMethodsWithAttribute<TAttribute>(Type assembly)
        {
            return assembly.GetMethods().Where(m => m.GetCustomAttributes().OfType<TAttribute>().Any());
        }
    }
}