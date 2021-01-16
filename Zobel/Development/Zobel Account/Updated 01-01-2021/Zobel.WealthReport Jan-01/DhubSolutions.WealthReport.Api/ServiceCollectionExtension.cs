using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DhubSolutions.WealthReport.Api
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
       ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies
                .SelectMany(assembly => assembly.DefinedTypes
                            .Where(typeInfo => typeInfo.GetInterfaces().Contains(typeof(T)) && !typeInfo.IsAbstract /*&&*/
                                              //typeInfo.IsAssignableFrom(typeof(T)) &&
                                              /*!typeInfo.IsAbstract && !typeInfo.IsInterface*/));

            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }
    }
}
