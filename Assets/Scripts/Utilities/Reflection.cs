using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities
{
    public static class Reflection
    {
        public static IEnumerable<MethodInfo> GetMethodsWithAttribute<TAttribute>()
            where TAttribute : Attribute =>
            Assembly.GetExecutingAssembly().GetTypes().SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes().OfType<TAttribute>().Any());

        public static IEnumerable<MethodInfo> GetMethodsWithAttribute<TAssembly, TAttribute>() =>
            GetMethodsWithAttribute<TAttribute>(typeof(TAssembly));


        public static IEnumerable<MethodInfo> GetMethodsWithAttribute<TAttribute>(Type assembly) =>
            assembly.GetMethods().Where(m => m.GetCustomAttributes().OfType<TAttribute>().Any());


        public static string GetTypeName(Type t) => t.ToString().Split('.').Last();
    }
}